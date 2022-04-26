using MediatR;
using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Application.Events;

public class ForwardEvent<TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>>
    where TDomainEvent : IDomainEvent
{
    private readonly IEventDispatcher<TDomainEvent> _eventDispatcher;

    public ForwardEvent(IEventDispatcher<TDomainEvent> eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public Task Handle(DomainEventNotification<TDomainEvent> notification, CancellationToken cancellationToken)
    {
        _eventDispatcher.Dispatch(notification.DomainEvent);
        return Task.CompletedTask;
    }
}