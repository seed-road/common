using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using SeedRoad.Common.Core.Domain.Exceptions;
using SeedRoad.Common.Presentation.WebApi.Contracts;
using SeedRoad.Common.Presentation.WebApi.ErrorHandling;
using SeedRoad.Common.Presentation.WebApi.Services;
using ValidationException = SeedRoad.Common.Core.Application.Exceptions.ValidationException;

namespace SeedRoad.Common.Presentation.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly HashSet<ApiVersion> ApiVersions = new();

    public static IServiceCollection AddResponseBuilder(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IHateoasResponseBuilder>(_ => HateoasResponseBuilder.Default());
    }

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

    public static IServiceCollection AddCommonVersioning(this IServiceCollection serviceCollection,
        string grpNameFormat = "'v'VV",
        int majorVersion = 0, int minorVersion = 0)
    {
        var apiVersion = new ApiVersion(majorVersion, minorVersion);
        ApiVersions.Add(apiVersion);
        return serviceCollection
            .AddVersionedApiExplorer(setupAction =>
            {
                setupAction.GroupNameFormat = grpNameFormat;
                setupAction.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = apiVersion;
                setupAction.ReportApiVersions = true;
            });
    }

    public static void AddCommonSwagger(this IServiceCollection services, string apiTitle, string apiDescription)
    {
        foreach (ApiVersion apiVersion in ApiVersions)
        {
            services.AddSwaggerDocument((settings, serviceProvider) =>
            {
                settings.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "TheCodeBuzz OAuth2 Service",
                    Flows = new OpenApiOAuthFlows()
                    {
                        AuthorizationCode = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                {"api1", "API1"},
                            },
                            AuthorizationUrl = "https://localhost:5001/connect/authorize",
                            TokenUrl = "https://localhost:5001/connect/token"
                        },
                    }
                });
                settings.PostProcess = document =>
                {
                    document.Info.Version = apiVersion.ToString();
                    document.Info.Title = apiTitle;
                    document.Info.Description = apiDescription;
                };
            });
        }
    }

    public static IServiceCollection AddCommonHttpErrorService(this IServiceCollection serviceCollection,
        bool enableTraces, IDictionary<Type, string> codeMapping, params ExceptionPriority[] exceptionPriorities)
    {
        return serviceCollection.AddScoped<IHttpErrorService, HttpErrorService>(_ =>
            new HttpErrorService(new HttpErrorServiceConfiguration()
            {
                ExceptionPriorities = exceptionPriorities,
                WithTrace = enableTraces,
                CodeMapping = codeMapping
            }));
    }

    public static IServiceCollection AddCommonHttpErrorService(this IServiceCollection serviceCollection,
        bool enableTraces)
    {
        var exceptionPriorities = new[]
        {
            ExceptionPriority.New<ValidationException>(0, (int) HttpStatusCode.UnprocessableEntity),
            ExceptionPriority.New<NotFoundException>(1, (int) HttpStatusCode.NotFound),
        };
        return serviceCollection.AddCommonHttpErrorService(enableTraces, new Dictionary<Type, string>(),
            exceptionPriorities);
    }
}