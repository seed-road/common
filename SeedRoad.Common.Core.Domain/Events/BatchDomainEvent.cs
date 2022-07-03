namespace SeedRoad.Common.Core.Domain.Events;

public class BatchDomainEvent<TEvent> : List<TEvent>, IDomainEvent where TEvent : IDomainEvent
{
    
}