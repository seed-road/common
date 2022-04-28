using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Core.Application.Events;
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

    public static IServiceCollection AddCommonDispatcher<TMessage>(this IServiceCollection serviceCollection,
        RoutingConfiguration routingConfiguration)
    {
        return serviceCollection.AddScoped<IEventDispatcher<TMessage>>(provider =>
        {
            var dispatcherService = provider.GetRequiredService<IDispatcherService>();
            return new EventDispatcher<TMessage>(dispatcherService, routingConfiguration);
        });
    }

    public static IServiceCollection AddEventForward<TMessage>(this IServiceCollection serviceCollection,
        RoutingConfiguration routingConfiguration) where TMessage : IDomainEvent
    {
        var eventForwardType = typeof(ForwardEvent<>).MakeGenericType(typeof(TMessage));
        return serviceCollection
            .AddCommonDispatcher<TMessage>(routingConfiguration)
            .AddScoped(typeof(INotificationHandler<DomainEventNotification<TMessage>>), eventForwardType);
    }
}