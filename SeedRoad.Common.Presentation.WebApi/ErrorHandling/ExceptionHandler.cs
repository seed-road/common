using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Presentation.WebApi.Serialization;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class ExceptionHandler
{
    private readonly string _contentType;
    private readonly ISerializer _serializer;

    public ExceptionHandler(string contentType, ISerializer serializer)
    {
        _contentType = contentType;
        _serializer = serializer;
    }

    public static ExceptionHandler XmlExceptionHandler()
    {
        return new("application/xml", new XmlSerializer());
    }

    public static ExceptionHandler JsonExceptionHandler()
    {
        return new("application/json", new JsonSerializer());
    }

    public async Task Invoke(HttpContext context)
    {
        var httpErrorService = context.RequestServices.GetRequiredService<IHttpErrorService>();
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        (var httpCode, HttpError httpError) =
            httpErrorService.ToHttpErrorWrapper(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Path);
        context.Response.StatusCode = httpCode;
        context.Response.ContentType = _contentType;

        await context.Response.WriteAsync(_serializer.Serialize(httpError));
    }
}