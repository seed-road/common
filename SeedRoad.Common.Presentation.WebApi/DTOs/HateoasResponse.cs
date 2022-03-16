namespace SeedRoad.Common.Presentation.WebApi.DTOs;

public class HateoasResponse<TResult>
{
    public HateoasResponse(TResult result, IList<LinkDto> links)
    {
        Result = result;
        Links = links;
    }

    public TResult Result { get; set; }
    public IList<LinkDto> Links { get; set; }
}