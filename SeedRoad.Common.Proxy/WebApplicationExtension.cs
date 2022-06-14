using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Proxy.Configuration;

namespace SeedRoad.Common.Proxy;

public static class WebApplicationExtension
{

    public static WebApplication UseProxy(this WebApplication app, ProxyConfiguration proxyConfiguration)
    {
        if (proxyConfiguration.LoggedHeaders is not null)
        {
            foreach (var proxyConfigurationLoggedHeader in proxyConfiguration.LoggedHeaders)
            {
                app.Logger.LogInformation("HEADER : {Header}", proxyConfigurationLoggedHeader);
            }
        }
        app.UseForwardedHeaders();
        app.UseHttpLogging();
        app.UseHttpsRedirection();
        if (proxyConfiguration.LoggedHeaders is not null)
        {
            app.Use((context, next) =>
            {
                app.Logger.LogInformation("Request RemoteIp: {RemoteIpAddress}",
                    context.Connection.RemoteIpAddress);
                return next();
            });
        }
        app.UseRewriter(
            new RewriteOptions().AddRedirectToHttps()
        );
        app.UseHsts();
        return app;
    }
}