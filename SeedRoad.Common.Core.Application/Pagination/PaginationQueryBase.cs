using System.Collections.Immutable;

namespace SeedRoad.Common.Core.Application.Pagination;

public record PaginationQueryBase
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public IReadOnlyCollection<string> Test => _test.ToImmutableHashSet();
    private HashSet<string> _test = new HashSet<string>();
}