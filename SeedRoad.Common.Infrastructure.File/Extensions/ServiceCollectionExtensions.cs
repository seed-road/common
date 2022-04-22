using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Core.Application.Files;
using SeedRoad.Common.Infrastructure.File.Services;

namespace SeedRoad.Common.Infrastructure.File.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileService<T>(this IServiceCollection serviceCollection, string path)
    {
        return serviceCollection.AddScoped<IFileService<T>>(provider => new FileService<T>(
            provider.GetRequiredService<ILogger<FileService<T>>>(),
            path
        ));
    }


    
    public static IServiceCollection AddFileService(this IServiceCollection serviceCollection, string path)
    {
        return serviceCollection.AddScoped<IFileService>(provider => new FileService(
            provider.GetRequiredService<ILogger<FileService>>(),
            path
        ));
    }
}