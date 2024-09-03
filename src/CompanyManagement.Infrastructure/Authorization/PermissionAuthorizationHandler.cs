using CompanyManagement.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CompanyManagement.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var permissionsResult = context.User.GetPermissions();

        if (permissionsResult.IsError)
        {
            return Task.CompletedTask;
        }

        if (permissionsResult.Value.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}


