using System.Collections.Immutable;
using MediatR;

namespace SeedRoad.Common.Core.Application.Pagination;

public record PaginationQueryBase<TResponse> : IRequest<TResponse>, IPagination
{
    public bool Paginate { get; set; } = true;
    public int Page { get; set; } = IPagination.UnsetPaginationValue;
    public int Size { get; set; } = IPagination.UnsetPaginationValue;
}