using SeedRoad.Common.Core.Domain.Events;

namespace SeedRoad.Common.Core.Domain.Definitions;

public interface IAggregateDto
{
    IList<IDomainEvent>? Events { get; }
    void ClearEvents();
}