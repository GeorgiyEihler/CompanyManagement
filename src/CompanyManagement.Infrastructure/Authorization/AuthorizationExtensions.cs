﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagement.Infrastructure.Authorization;

internal static class AuthorizationExtensions
{
    internal static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}