using Microsoft.AspNetCore.Mvc;
using SeedRoad.Common.Core.Application.Pagination;
using SeedRoad.Common.Presentation.WebApi.Contracts;
using SeedRoad.Common.Presentation.WebApi.DTOs;
using SeedRoad.Common.Presentation.WebApi.Extensions;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Presentation.WebApi.Services;

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

    public HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, IPagedList<T> page,
        IEnumerable<T> values,
        string routeName, object? routeValues = null)
    {
        var valuesDic = routeValues == null ? new Dictionary<string, object>() : routeValues.ToDictionary<object>();

        return GeneratePageResponse(urlHelper, page.ToPagedListResume(), values, routeName, valuesDic);
    }


    public HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, IPagedList<T> page,
        string routeName, IDictionary<string, object>? routeValues = null)
    {
        var valuesDic = routeValues == null ? new Dictionary<string, object>() : routeValues.ToDictionary<object>();

        return GeneratePageResponse(urlHelper, page.ToPagedListResume(), page, routeName, valuesDic);
    }

    public HateoasPageResponse<T> FromPagedList<T>(IUrlHelper urlHelper, PagedListResume resume,
        IEnumerable<T> values,
        string routeName, object? routeValues = null)
    {
        var valuesDic = routeValues == null ? new Dictionary<string, object>() : routeValues.ToDictionary<object>();

        return GeneratePageResponse(urlHelper, resume, values, routeName, valuesDic);
    }


  


    public HateoasResponse<T> ToEntityResponse<T>(T entity, IEnumerable<LinkDto> entityLinks)
    {
        return new HateoasResponse<T>(entity, entityLinks.ToList());
    }


    public static HateoasResponseBuilder Default(string pageParam = DefaultPageQueryParam,
        string sizeParam = DefaultSizeQueryParam)
    {
        return new HateoasResponseBuilder(pageParam, sizeParam);
    }

    private HateoasPageResponse<T> GeneratePageResponse<T>(IUrlHelper urlHelper, PagedListResume page,
        IEnumerable<T> values,
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

        return new HateoasPageResponse<T>(values.ToList(), links, page.TotalCount);
    }
}