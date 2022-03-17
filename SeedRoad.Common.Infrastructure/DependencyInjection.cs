using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Core.Domain.Definitions;
using SeedRoad.Common.Core.Domain.Time;
using SeedRoad.Common.Infrastructure.Services;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection serviceCollection,
        params Assembly[] assemblies)
    {
        var allAssemblies = assemblies.WithCallingAssembly();
        return serviceCollection.AddAggregateRepositories(allAssemblies).AddSingleton<ITimeGenerator,TimeGenerator>();
    }

    private static IServiceCollection AddAggregateRepositories(this IServiceCollection serviceCollection,
        IEnumerable<Assembly> assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IAggregateRepository<,,>).IsAssignableFrom(type))
                {
                    serviceCollection.AddScoped(typeof(IAggregateRepository<,,>), type);
                }
            }
        }

        return serviceCollection;
    }
}