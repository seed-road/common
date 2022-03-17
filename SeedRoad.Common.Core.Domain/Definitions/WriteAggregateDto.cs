using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Domain.Definitions;

public abstract class WriteAggregateDto : IAggregateDto
{
    public IReadOnlyList<IDomainEvent>? Events => _events.AsReadOnly();
    protected List<IDomainEvent>? _events;

    public void ClearEvents()
    {
        _events = new List<IDomainEvent>();
    }
}