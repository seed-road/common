using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SeedRoad.Common.Authentication.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonAuthentication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
        return serviceCollection;
    }

    public static IServiceCollection AddCommonAuthorization(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "api1");
            });
        });
    }
}