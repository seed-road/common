using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.WebApi.ErrorHandling;

namespace SeedRoad.Common.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonCors(this IServiceCollection serviceCollection, string policyName,
        string clientUrl)
    {
        return serviceCollection.AddCors(options =>
        {
            options.AddPolicy(policyName, builder =>
            {
                builder.WithOrigins(clientUrl)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Location");
            });
        });
    }

    public static IApiVersionDescriptionProvider AddCommonVersioning(this IServiceCollection serviceCollection,
        string grpNameFormat = "'v'VV",
        int majorVersion = 0, int minorVersion = 0)
    {
        serviceCollection.AddVersionedApiExplorer(setupAction => { setupAction.GroupNameFormat = grpNameFormat; });
        serviceCollection.AddApiVersioning(setupAction =>
        {
            setupAction.AssumeDefaultVersionWhenUnspecified = true;
            setupAction.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            setupAction.ReportApiVersions = true;
        });
        return serviceCollection.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
    }

    public static void AddCommonSwagger(this IServiceCollection services,
        IApiVersionDescriptionProvider apiVersionDescriptionProvider, string apiTitle, string apiDescription)
    {
        foreach (ApiVersionDescription apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            services.AddSwaggerDocument((settings, serviceProvider) =>
            {
                // var fluentValidationSchemaProcessor = serviceProvider.GetService<FluentValidationSchemaProcessor>();
                // Add the fluent validations schema processor
                // settings.SchemaProcessors.Add(fluentValidationSchemaProcessor);
                settings.PostProcess = document =>
                {
                    document.Info.Version = apiVersionDescription.ApiVersion.ToString();
                    document.Info.Title = apiTitle;
                    document.Info.Description = apiDescription;
                };
            });
        }
    }

    public static IServiceCollection AddCommonHttpErrorService(this IServiceCollection serviceCollection,
        bool enableTraces, params ExceptionPriority[] exceptionPriorities)
    {
        return serviceCollection.AddScoped<IHttpErrorService, HttpErrorService>(_ =>
            HttpErrorService.DefaultErrorService(exceptionPriorities, enableTraces));
    }
}