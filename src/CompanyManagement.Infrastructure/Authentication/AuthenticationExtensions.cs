using CompanyManagement.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagement.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Owner", builder =>
            {
                builder.RequireClaim("owner", "true");
            });

            options.AddPolicy("Administartor", builder =>
            {
                builder.RequireClaim("administartor", "true");
            });

            options.AddPolicy("Participant", builder =>
            {
                builder.RequireClaim("participant", "true");
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddHttpContextAccessor();

        return services;
    }
}
