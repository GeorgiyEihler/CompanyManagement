using ErrorOr;
using System.Security.Claims;

namespace CompanyManagement.Infrastructure.Authentication;

internal static class ClaimPrincipalExtension
{
    internal static ErrorOr<Guid> GetUserId(this ClaimsPrincipal? principal)
    {
        var claimUserId = principal?.Claims.FirstOrDefault(c => c.Type == SpecialClaims.UserId)?.Value;

        if (claimUserId is null)
        {
            return Error.Forbidden();
        }

        if (!Guid.TryParse(claimUserId, out var userId))
        {
            return Error.Forbidden(); 
        }

        return userId;
    }

    internal static ErrorOr<HashSet<string>> GetPermissions(this ClaimsPrincipal? principal)
    {
        var permissions = principal?.Claims.Where(c => c.Type == SpecialClaims.Permission);

        if (permissions == null)
        {
            return Error.Forbidden();
        }

        return permissions.Select(c => c.Value).ToHashSet();

    }
}
