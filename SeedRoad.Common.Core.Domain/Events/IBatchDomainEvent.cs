namespace SeedRoad.Common.Core.Domain.Events;

public interface IBatchDomainEvent<TEvent>: IDomainEvent, ICollection<TEvent > where TEvent : IDomainEvent
{
    
}