namespace SeedRoad.Common.Core.Application.Pagination;

public class PaginationConfiguration
{
    public const int DefaultPageValue = 0;
    public const int DefaultSizeValue = 10;
    public const int DefaultMaxSizeValue = 10;
    public int DefaultPage { get; set; } = DefaultPageValue;
    public int DefaultSize { get; set; } = DefaultSizeValue;
    public int DefaultMaxSize { get; set; } = DefaultMaxSizeValue;
}