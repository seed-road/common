using SeedRoad.Common.Domain.Events;

namespace SeedRoad.Common.Domain.Contracts;

public interface IAggregate
{
    public IReadOnlyList<IDomainEvent> Events { get; }
    public void ClearEvents();
}