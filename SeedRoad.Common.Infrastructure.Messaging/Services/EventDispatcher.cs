using SeedRoad.Common.Core.Domain.Events;
using SeedRoad.Common.Infrastructure.Messaging.Exceptions;
using SeedRoad.Common.Messaging.Configurations;

namespace SeedRoad.Common.Infrastructure.Messaging.Services
{
    public class EventDispatcher<TMessage> : IEventDispatcher<TMessage>
    {
        private readonly IDispatcherService _dispatcherService;
        private readonly string _exchange;
        private readonly string _routingKey;
        
        public EventDispatcher(IDispatcherService dispatcherService, RoutingConfiguration routingConfiguration)
        {
            _dispatcherService = dispatcherService;
            _exchange = routingConfiguration.Exchange;
            _routingKey = routingConfiguration.RoutingKey;
        }
        
        public void Dispatch(TMessage message)
        {
            if (message is null)
            {
                throw new InvalidMessageException("Cannot send null message with rabbitmq");
            }
            _dispatcherService.PushMessage(message, _routingKey, _exchange);
        }
    }
}