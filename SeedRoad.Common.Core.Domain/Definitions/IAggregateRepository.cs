namespace SeedRoad.Common.Core.Domain.Definitions;

public interface IAggregateRepository<TId, in TWriteAggregateDto, TReadAggregateDto>
    where TWriteAggregateDto : IAggregateDto
{
    public Task<TId> SetAsync(TWriteAggregateDto dto);
    public Task<TReadAggregateDto?> FindByIdAsync(TId id);
    public Task RemoveAsync(TWriteAggregateDto dto);
    public Task<TId> NextIdAsync();
}