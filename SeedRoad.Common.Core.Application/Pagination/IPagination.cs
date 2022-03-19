namespace SeedRoad.Common.Core.Application.Pagination;

public interface IPagination
{
    public const int UnsetPaginationValue = -1;
    public int Page { get; }
    public int Size { get; }
}