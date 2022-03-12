using Microsoft.AspNetCore.Mvc;
using SeedRoad.Common.WebApi.DTOs;

namespace SeedRoad.Common.WebApi.Contracts;

public interface IHateoasResponseBuilder
{
    HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, PagedListResume page,
        IList<T> values,
        string routeName, object? routeValues = null);

    HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, PagedListResume page,
        IList<T> values,
        string routeName, IDictionary<string, object>? routeValues = null);
}