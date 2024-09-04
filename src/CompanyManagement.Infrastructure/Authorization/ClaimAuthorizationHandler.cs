using CompanyManagement.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CompanyManagement.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        var permissionResult = context.User.GetPermissions();

        if (permissionResult.IsError)
        {
            return Task.CompletedTask;
        }

        if (permissionResult.Value.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
