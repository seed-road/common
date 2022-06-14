using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Proxy.Configuration;

namespace SeedRoad.Common.Proxy;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddProxy(this IServiceCollection serviceCollection,
        ProxyConfiguration proxyConfiguration)
    {
        serviceCollection.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            if (proxyConfiguration.ProxyIp is not null)
            {
                options.KnownProxies.Add(IPAddress.Parse(proxyConfiguration.ProxyIp));
            }

            if (proxyConfiguration.ProxyNetwork is not null)
            {
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse(proxyConfiguration.ProxyNetwork),
                    proxyConfiguration.ProxyNetworkMask));
            }
        });

        if (proxyConfiguration.LoggedHeaders is not null)
        {
            serviceCollection.AddHttpLogging(options =>
            {
                foreach (var proxyConfigurationLoggedHeader in proxyConfiguration.LoggedHeaders)
                {
                    options.ResponseHeaders.Add(proxyConfigurationLoggedHeader);
                    options.RequestHeaders.Add(proxyConfigurationLoggedHeader);
                }
            });
        }

        return serviceCollection;
    }
}