using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Domain.Definitions;

public abstract class WriteAggregateDto : IAggregateDto
{
    public WriteAggregateDto(IEnumerable<IDomainEvent> events)
    {
        Events = events.ToList();
    }


    public IList<IDomainEvent>? Events { get;  set; }

    protected WriteAggregateDto()
    {
    }

    public void ClearEvents()
    {
        Events = new List<IDomainEvent>();
    }
}