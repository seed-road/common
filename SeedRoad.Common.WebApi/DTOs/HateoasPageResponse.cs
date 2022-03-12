namespace SeedRoad.Common.WebApi.DTOs;

public class HateoasPageResponse<T> : HateoasResponse<IList<T>>
{
    public HateoasPageResponse(IList<T> result, IList<LinkDto> links, long total) : base(result, links)
    {
        Total = total;
    }

    public long Total { get; set; }
}