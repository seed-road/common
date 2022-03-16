using Microsoft.Extensions.DependencyInjection;

namespace SeedRoad.Common.Core.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProxiedScoped<TInterface, TImplementation>(this IServiceCollection services,
        params Type[] interceptorsType)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddScoped<TImplementation>();
        services.AddScoped(typeof(TInterface),
            DependencyInjection.ProxyFactory<TInterface, TImplementation>(interceptorsType));
    }
}