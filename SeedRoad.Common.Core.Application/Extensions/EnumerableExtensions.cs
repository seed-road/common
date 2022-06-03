using SeedRoad.Common.Core.Application.Pagination;

namespace SeedRoad.Common.Core.Application.Extensions;

public static class EnumerableExtensions
{
    public static PagedList<TItem> FromPagination<TItem>(this IEnumerable<TItem> items, IPagination pagination)
    {
        var itemsList = items.ToList();
        var count = itemsList.Count;
        var skip = pagination.Page * pagination.Size;
        var paginatedItems = itemsList.Skip(skip).Take(pagination.Size).ToList();
        return new PagedList<TItem>(paginatedItems, count, pagination.Page, pagination.Size);
    }

    public static PagedList<TItem> ToPagedList<TItem>(this IEnumerable<TItem> items)
    {
        var itemsList = items.ToList();
        var count = itemsList.Count;
        return new PagedList<TItem>(itemsList, count, 0, count);
    }
}