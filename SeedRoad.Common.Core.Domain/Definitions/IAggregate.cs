using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Domain.Definitions;

public interface IAggregate
{
    public IReadOnlyList<IDomainEvent> Events { get; }
    public void ClearEvents();
}