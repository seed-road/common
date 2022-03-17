namespace SeedRoad.Common.Core.Domain.Definitions;

public interface IAggregateRepository<TId, in TWriteAggregateDto, TReadAggregateDto> where TWriteAggregateDto : IAggregateDto
{
    public Task<TId> SetAsync(TWriteAggregateDto aggregate);
    public Task<TReadAggregateDto?> FindByIdAsync(TId id);
    public Task RemoveAsync(TId id);
    public Task<TId> NextIdAsync();
}