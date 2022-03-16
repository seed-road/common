using SeedRoad.Common.Core.Domain.Contracts;

namespace SeedRoad.Common.Core.Application.Contracts;

public interface IAggregateRepository<TId, TAggregate> where TAggregate : Aggregate<TId> where TId : IEntityId
{
    public Task<TId> SetAsync(TAggregate aggregate);
    public Task<TAggregate?> FindByIdAsync(TId id);
    public Task RemoveAsync(TId id);
    public Task<TId> NextIdAsync();
}