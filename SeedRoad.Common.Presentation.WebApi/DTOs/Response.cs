namespace SeedRoad.Common.Presentation.WebApi.DTOs;

public class Response<TResult>
{
    public Response(TResult result)
    {
        Result = result;
    }

    public TResult Result { get; set; }
}