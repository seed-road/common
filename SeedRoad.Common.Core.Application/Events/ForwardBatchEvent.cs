using MediatR;
using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Application.Events;

public class ForwardBatchEvent<TDomainEvent> : INotificationHandler<DomainEventNotification<BatchDomainEvent<TDomainEvent>>>
    where TDomainEvent : IDomainEvent
{
    private readonly IEventDispatcher<TDomainEvent> _eventDispatcher;

    public ForwardBatchEvent(IEventDispatcher<TDomainEvent> eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public Task Handle(DomainEventNotification<BatchDomainEvent<TDomainEvent>> notification, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in notification.DomainEvent)
        {
            _eventDispatcher.Dispatch(domainEvent);
        }

        return Task.CompletedTask;
    }
}