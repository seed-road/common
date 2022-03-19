using System.Collections.Immutable;
using MediatR;

namespace SeedRoad.Common.Core.Application.Pagination;

public record PaginationQueryBase<TResponse> : IRequest<TResponse>, IPagination
{
    public int Page { get; set; } = IPagination.UnsetPaginationValue;
    public int Size { get; set; } = IPagination.UnsetPaginationValue;
}