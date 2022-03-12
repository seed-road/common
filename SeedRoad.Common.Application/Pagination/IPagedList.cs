namespace SeedRoad.Common.Application.Pagination;

public interface IPagedList<T> : IList<T>
{
    public long CurrentPage { get; }
    public long TotalPages { get; }
    public long PageSize { get; }
    public long TotalCount { get; }
    public bool HasPrevious { get; }
    public bool HasNext { get; }
}