using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Domain.Contracts;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate where TId : IEntityId
{
    private readonly List<IDomainEvent> _events = new();

    public Aggregate(TId id) : base(id)
    {
    }

    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    public void ClearEvents()
    {
        _events.Clear();
    }

    protected Aggregate<TId> RegisterEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
        return this;
    }
}