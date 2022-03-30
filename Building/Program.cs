using Autofac;
using Building.IntegrationEvents.EventHandling;
using Building.IntegrationEvents.Events;
using Building.Repositories;
using Building.Services;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
AddEventBusRegistration(builder.Services);
builder.Services.AddScoped<IHouseService, HouseService>();
builder.Services.AddSingleton<IHouseRepository, InMemoryHouseRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ConfigureEventBus(app);

app.UseAuthorization();

app.MapControllers();

app.Run();


void AddEventBusRegistration(IServiceCollection services)
{
    builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
    {
        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
        var factory = new ConnectionFactory()
        {
            HostName = builder.Configuration["ExventBusConnection"],
            DispatchConsumersAsync = true
        };

        if (!string.IsNullOrEmpty(builder.Configuration["EventBusUserName"]))
        {
            factory.UserName = builder.Configuration["EventBusUserName"];
        }

        if (!string.IsNullOrEmpty(builder.Configuration["EventBusPassword"]))
        {
            factory.Password = builder.Configuration["EventBusPassword"];
        }

        var retryCount = 5;
        if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
        {
            retryCount = int.Parse(builder.Configuration["EventBusRetryCount"]);
        }

        return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
    });
    RegisterEventBus(services);
}

void RegisterEventBus(IServiceCollection services)
{
    services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
    {
        var subscriptionClientName = builder.Configuration["SubscriptionClientName"];
        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
        var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        var retryCount = 5;
        if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
        {
            retryCount = int.Parse(builder.Configuration["EventBusRetryCount"]);
        }

        return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope,
            eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    });

    builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
}

void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus
        .Subscribe<UserCreatedSuccessfullyIntegrationEvent,
            UserCreatedSuccessfullyIntegrationEventHandler>();
}