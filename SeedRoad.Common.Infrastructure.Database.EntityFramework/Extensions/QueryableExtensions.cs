using Microsoft.EntityFrameworkCore;
using SeedRoad.Common.Core.Application.Pagination;

namespace SeedRoad.Common.Infrastructure.Database.EntityFramework.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> FromPaginationQueryAsync<T>(this IQueryable<T> query,
        IPagination pagination)
    {
        var count = await query.CountAsync();
        var skip = pagination.Page  * pagination.Size;
        var items = await query.Skip(skip).Take(pagination.Size).ToListAsync();
        return new PagedList<T>(items, count, pagination.Page, pagination.Size);
    }
}