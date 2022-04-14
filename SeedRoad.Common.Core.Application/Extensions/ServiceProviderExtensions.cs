using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SeedRoad.Common.Core.Application.Extensions;

public static class ServiceProviderExtensions
{
    public static void FireNotificationAndForget<T>(this IServiceProvider serviceProvider, T notification,
        ILogger? logger = null)
    {
        if (notification is null) throw new ArgumentNullException(nameof(notification));
        Task.Run(async () =>
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                await publisher.Publish(notification, default);
            }
            catch (Exception exception)
            {
                logger?.LogError("Notification {Notification} handling failed, with error {Error}",
                    notification.GetType(), exception.Message);
            }
        });
    }
}