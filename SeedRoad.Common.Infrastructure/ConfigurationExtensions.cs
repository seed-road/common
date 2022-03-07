using Microsoft.Extensions.Configuration;

namespace SeedRoad.Common.Infrastructure;

public static class ConfigurationExtensions
{
    /// <summary>
    ///     Instancie une classe à partir d'une section de configuration d'un projet.
    /// </summary>
    /// <remarks>Le type de classe de configuration doit correspondre à l'entrée visée dans la configuration</remarks>
    /// <param name="configuration">Configuration du projet appelant</param>
    /// <typeparam name="TImplementation">Type concret de  la classe de configuration</typeparam>
    /// <returns></returns>
    public static TImplementation GetRequiredSetting<TImplementation>(
        this IConfiguration configuration) where TImplementation : class
    {
        var settingsName = typeof(TImplementation).Name;
        return configuration.GetSection(settingsName).Get<TImplementation>() ??
               throw new ArgumentException($"Configuration not found : {settingsName}");
    }
}