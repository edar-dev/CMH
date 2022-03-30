using Microsoft.Extensions.DependencyInjection;

namespace User.Persistence;

public static class ServiceCollectionExtension
{
    public static void RegisterPersistence(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUserRepository, UserRepository>();
    }
}