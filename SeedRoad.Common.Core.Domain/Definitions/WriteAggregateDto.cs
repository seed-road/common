using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Domain.Definitions;

public abstract class WriteAggregateDto : IAggregateDto
{
    public WriteAggregateDto(IAggregate aggregate)
    {
        _events = aggregate.Events.ToList();
    }

    public IReadOnlyList<IDomainEvent>? Events => _events.AsReadOnly();
    protected List<IDomainEvent>? _events;

    protected WriteAggregateDto()
    {
        
    }

    public void ClearEvents()
    {
        _events = new List<IDomainEvent>();
    }
}