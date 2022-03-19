using Microsoft.AspNetCore.Mvc;
using SeedRoad.Common.Core.Application.Pagination;
using SeedRoad.Common.Presentation.WebApi.DTOs;

namespace SeedRoad.Common.Presentation.WebApi.Contracts;

public interface IHateoasResponseBuilder
{


    HateoasResponse<T> ToEntityResponse<T>(T entity, IEnumerable<LinkDto> entityLinks);

    HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, IPagedList<T> page,
        IEnumerable<T> values,
        string routeName, object? routeValues = null);

    HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, IPagedList<T> page,
        string routeName, IDictionary<string, object>? routeValues = null);
    

    HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, PagedListResume resume,
        IEnumerable<T> values,
        string routeName, object? routeValues = null);


}