namespace SeedRoad.Common.Core.Application.Pagination;

public class PagedList<TItem> : List<TItem>, IPagedList<TItem>
{
    public PagedList(IEnumerable<TItem> items, long count, long pageNumber, long pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (long) Math.Ceiling(count / (double) pageSize);
        AddRange(items);
    }

    public long CurrentPage { get; }
    public long TotalPages { get; }
    public long PageSize { get; }
    public long TotalCount { get; }
    public bool HasPrevious => CurrentPage > 0;
    public bool HasNext => CurrentPage < TotalPages - 1;

    public static PagedList<TItem> Empty(long pageNumber, long pageSize)
    {
        return new PagedList<TItem>(new List<TItem>(), 0, pageNumber, pageSize);
    }

    public static PagedList<TItem> Empty(IPagination pagination)
    {
        return new PagedList<TItem>(new List<TItem>(), 0, pagination.Page, pagination.Size);
    }


    public static PagedList<TItem> From<TOther>(IEnumerable<TItem> items, PagedList<TOther> page)
    {
        return new PagedList<TItem>(items, page.Count, page.CurrentPage, page.PageSize);
    }
}