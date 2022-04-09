using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Messaging.Configurations;
using SeedRoad.Common.Presentation.Messaging.Services;

namespace SeedRoad.Common.Presentation.Messaging.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddCommonListener<TEvent>(this IServiceCollection serviceCollection,
        RabbitMqConfiguration configuration,
        RoutingConfiguration routingConfiguration)
    {
        serviceCollection.AddTransient(typeof(MessageProcessor<>));
        return serviceCollection.AddHostedService(provider =>
        {
            var msgProcessor = provider.GetRequiredService<MessageProcessor<TEvent>>();
            var logger = provider.GetRequiredService<ILogger<RabbitMqListener<TEvent>>>();
            return new RabbitMqListener<TEvent>(configuration, routingConfiguration, msgProcessor, logger);
        });
    }
}