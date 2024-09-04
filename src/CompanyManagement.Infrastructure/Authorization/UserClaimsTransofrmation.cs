using CompanyManagement.Application.Abstractions.Authentication;
using CompanyManagement.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CompanyManagement.Infrastructure.Authorization;

internal sealed class UserClaimsTransofrmation : IClaimsTransformation
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IPermissionService _permissionService;

    public UserClaimsTransofrmation(
        IServiceScopeFactory serviceScopeFactory, 
        IPermissionService permissionService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _permissionService = permissionService;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        var userIdResult = principal.GetUserId();

        if (userIdResult.IsError)
        {
            throw new ApplicationException();
        }

        var permissionsResult = await _permissionService.GetUserPermissionsAsync(userIdResult.Value);

        if (permissionsResult.IsError)
        {
            throw new ApplicationException();
        }

        var claimsIdentity = new ClaimsIdentity();

        foreach (var permission in permissionsResult.Value.Permissions)
        {
            claimsIdentity.AddClaim(new Claim(SpecialClaims.Permission, permission));
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
