namespace SeedRoad.Common.Infrastructure.Messaging.Services;

public interface IDispatcherService
{
    void PushMessage(object message, string exchange, string routingKey);
}