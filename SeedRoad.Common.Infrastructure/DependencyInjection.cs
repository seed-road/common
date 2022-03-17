using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Core.Application.Contracts;
using SeedRoad.Common.Core.Domain.Contracts;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection serviceCollection,
        params Assembly[] assemblies)
    {
        var allAssemblies = assemblies.WithCallingAssembly();
        return serviceCollection.AddAggregateRepositories(allAssemblies);
    }

    private static IServiceCollection AddAggregateRepositories(this IServiceCollection serviceCollection,
        IEnumerable<Assembly> assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IAggregateRepository<,>).IsAssignableFrom(type))
                {
                    serviceCollection.AddScoped(typeof(IAggregateRepository<,>), type);
                }
            }
        }

        return serviceCollection;
    }
}