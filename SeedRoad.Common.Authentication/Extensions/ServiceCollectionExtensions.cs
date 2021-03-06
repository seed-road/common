using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SeedRoad.Common.Authentication.Configuration;

namespace SeedRoad.Common.Authentication.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonAuthentication(this IServiceCollection serviceCollection, AuthConfiguration authConfiguration)
    {
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authConfiguration.Authority;
                options.RequireHttpsMetadata = authConfiguration.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authConfiguration.ValidIssuer,
                    ValidateAudience = false
                };
            });
        return serviceCollection;
    }

    public static IServiceCollection AddCommonAuthorization(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy(AuthenticationConstants.Profile, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", AuthenticationConstants.Scope);
            });
        });
    }
}