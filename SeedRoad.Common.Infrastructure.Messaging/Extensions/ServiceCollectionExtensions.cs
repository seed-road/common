using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Core.Domain.Events;
using SeedRoad.Common.Infrastructure.Messaging.Services;
using SeedRoad.Common.Messaging.Configurations;

namespace SeedRoad.Common.Infrastructure.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonDispatchService(this IServiceCollection serviceCollection,
        IRabbitMqConfiguration configuration)
    {
        return serviceCollection.AddSingleton(configuration)
            .AddScoped<IDispatcherService, DispatcherService>();
    }

    public static IServiceCollection AddCommonDispatcher<TMessage>(this IServiceCollection serviceCollection, RoutingConfiguration routingConfiguration)
    {
        return serviceCollection.AddScoped<IEventDispatcher<TMessage>>(provider =>
        {
            var dispatcherService = provider.GetRequiredService<IDispatcherService>();
            return new EventDispatcher<TMessage>(dispatcherService, routingConfiguration);
        });
    }
}