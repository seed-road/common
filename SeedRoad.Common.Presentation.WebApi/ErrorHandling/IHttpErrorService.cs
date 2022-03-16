namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public interface IHttpErrorService
{
    public HttpErrorWrapper ToHttpErrorWrapper(Exception exception, string instance);
}