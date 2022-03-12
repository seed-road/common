using Microsoft.AspNetCore.Mvc;
using SeedRoad.Common.System;
using SeedRoad.Common.WebApi.Contracts;
using SeedRoad.Common.WebApi.DTOs;

namespace SeedRoad.Common.WebApi.Services;

public class HateoasResponseBuilder : IHateoasResponseBuilder
{
    public const string DefaultPageQueryParam = "page";
    public const string DefaultSizeQueryParam = "size";
    private readonly string _pageParam;
    private readonly string _sizeParam;

    private HateoasResponseBuilder(string pageParam, string sizeParam)
    {
        _pageParam = pageParam;
        _sizeParam = sizeParam;
    }

    public HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, PagedListResume page,
        IList<T> values,
        string routeName, object? routeValues = null)
    {
        var valuesDic = routeValues == null ? new Dictionary<string, object>() : routeValues.ToDictionary<object>();

        return GeneratePageResponse(urlHelper, page, values, routeName, valuesDic);
    }


    public HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, PagedListResume page,
        IList<T> values,
        string routeName, IDictionary<string, object>? routeValues = null)
    {
        var valuesDic = routeValues == null ? new Dictionary<string, object>() : routeValues.ToDictionary<object>();

        return GeneratePageResponse(urlHelper, page, values, routeName, valuesDic);
    }


    public static HateoasResponseBuilder Default(string pageParam = DefaultPageQueryParam,
        string sizeParam = DefaultSizeQueryParam)
    {
        return new(pageParam, sizeParam);
    }

    private HateoasPageResponse<T> GeneratePageResponse<T>(IUrlHelper urlHelper, PagedListResume page,
        IList<T> values,
        string routeName, IDictionary<string, object> valuesDic)
    {
        valuesDic[_pageParam] = page.CurrentPage;
        valuesDic[_sizeParam] = page.PageSize;

        var links = new List<LinkDto>
        {
            LinkDto.CurrentPage(urlHelper.Link(routeName, valuesDic))
        };
        if (page.HasPrevious)
        {
            var previousValues = valuesDic.Copy();
            previousValues[_pageParam] = page.CurrentPage - 1;
            links.Add(LinkDto.PreviousPage(urlHelper.Link(routeName, previousValues)));
        }

        if (page.HasNext)
        {
            var nextValues = valuesDic.Copy();
            nextValues[_pageParam] = page.CurrentPage + 1;
            links.Add(LinkDto.NextPage(urlHelper.Link(routeName, nextValues)));
        }

        return new HateoasPageResponse<T>(values, links, page.TotalCount);
    }
}