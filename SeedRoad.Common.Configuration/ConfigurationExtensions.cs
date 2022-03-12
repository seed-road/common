using Microsoft.Extensions.Configuration;

namespace SeedRoad.Common.Configuration;

public static class ConfigurationExtensions
{
    public static TImplementation GetRequiredConfiguration<TImplementation>(
        this IConfiguration configuration) where TImplementation : class
    {
        var configurationName = typeof(TImplementation).Name;
        return configuration.GetSection(configurationName).Get<TImplementation>() ??
               throw new ArgumentException($"Configuration not found : {configurationName}");
    }

    public static TImplementation GetRequiredConfiguration<TImplementation>(
        this IConfiguration configuration, string configurationName) where TImplementation : class
    {
        return configuration.GetSection(configurationName).Get<TImplementation>() ??
               throw new ArgumentException($"Configuration not found : {configurationName}");
    }
}