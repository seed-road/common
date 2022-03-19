using SeedRoad.Common.Core.Application.Pagination;
using SeedRoad.Common.Presentation.WebApi.DTOs;

namespace SeedRoad.Common.Presentation.WebApi.Extensions;

public static class PagedListExtensions
{
    public static PagedListResume ToPagedListResume<T>(this IPagedList<T> page)
    {
        return new PagedListResume(page.CurrentPage, page.TotalPages, page.PageSize, page.TotalCount,
            page.HasPrevious, page.HasNext);
    }
}