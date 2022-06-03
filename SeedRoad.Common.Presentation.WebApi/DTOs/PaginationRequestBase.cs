using SeedRoad.Common.Core.Application.Pagination;

namespace SeedRoad.Common.Presentation.WebApi.DTOs;

public abstract record PaginationRequestBase
{
    public bool Paginate { get; set; } = true;
    public int Page { get; set; } = IPagination.UnsetPaginationValue;
    public int Size { get; set; } = IPagination.UnsetPaginationValue;
}