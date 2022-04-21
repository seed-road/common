using SeedRoad.Common.Core.Application.Pagination;

namespace SeedRoad.Common.Presentation.WebApi.DTOs;

public abstract record PaginationRequestBase
{
    public int Page { get; set; } = IPagination.UnsetPaginationValue;
    public int Size { get; set; } = IPagination.UnsetPaginationValue;
}