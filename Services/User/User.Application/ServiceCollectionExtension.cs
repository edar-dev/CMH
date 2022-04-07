using Microsoft.Extensions.DependencyInjection;

namespace User.Application;

public static class ServiceCollectionExtension
{
    public static void RegisterApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
    }
}