using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Domain.Definitions;

public abstract class WriteAggregateDto : IAggregateDto
{
    public WriteAggregateDto(IEnumerable<IDomainEvent> events)
    {
        _events = events.ToList();
    }


    public Guid Id { get; set; }
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