namespace SeedRoad.Common.WebApi.DTOs;

public class PagedListResume
{
    public PagedListResume(long currentPage, long totalPages, long pageSize, long totalCount, bool hasPrevious,
        bool hasNext)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
        HasPrevious = hasPrevious;
        HasNext = hasNext;
    }

    public long CurrentPage { get; }
    public long TotalPages { get; }
    public long PageSize { get; }
    public long TotalCount { get; }
    public bool HasPrevious { get; }
    public bool HasNext { get; }
}