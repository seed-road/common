namespace SeedRoad.Common.WebApi.ErrorHandling;

public interface IHttpErrorService
{
    public HttpErrorWrapper ToHttpErrorWrapper(Exception exception, string instance);
}