using MediatR;
using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Application.Notifications;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}