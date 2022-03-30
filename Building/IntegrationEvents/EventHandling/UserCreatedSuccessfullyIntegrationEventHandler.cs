using Building.Dtos;
using Building.IntegrationEvents.Events;
using Building.Services;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Options;

namespace Building.IntegrationEvents.EventHandling;

public class
    UserCreatedSuccessfullyIntegrationEventHandler : IIntegrationEventHandler<UserCreatedSuccessfullyIntegrationEvent>
{
    private readonly ILogger<UserCreatedSuccessfullyIntegrationEventHandler> _logger;
    private readonly IHouseService _houseService;

    public UserCreatedSuccessfullyIntegrationEventHandler(
        ILogger<UserCreatedSuccessfullyIntegrationEventHandler> logger,
        IHouseService houseService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _houseService = houseService ?? throw new ArgumentNullException(nameof(houseService));

        _logger.LogTrace("UserCreatedSuccessfullyIntegrationEventHandler ctor");
    }

    public async Task Handle(UserCreatedSuccessfullyIntegrationEvent @event)
    {
        _logger.LogInformation(
            "----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id,
            "Building", @event);

        await _houseService.CreateHouse(new HouseDto(Guid.NewGuid(), "IntegrationEventHouse", "test"),
            Enumerable.Empty<RoomDto>());
        
        await Task.CompletedTask;
    }
}