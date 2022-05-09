namespace SeedRoad.Common.Presentation.WebApi.DTOs;

public class HateoasResponse<TResult>: Response<TResult>
{
    public HateoasResponse(TResult result, IList<LinkDto> links): base(result)
    {
        Result = result;
        Links = links;
    }

    public TResult Result { get; set; }
    public IList<LinkDto> Links { get; set; }
}