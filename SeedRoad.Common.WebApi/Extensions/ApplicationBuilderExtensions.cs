using Microsoft.AspNetCore.Builder;
using SeedRoad.Common.WebApi.ErrorHandling;

namespace SeedRoad.Common.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCommonExceptionHandler(this IApplicationBuilder appBuilder)
    {
        var exceptionHandler = ExceptionHandler.JsonExceptionHandler();
        return appBuilder.UseExceptionHandler(new ExceptionHandlerOptions
            { ExceptionHandler = context => exceptionHandler.Invoke(context) });
    }
}