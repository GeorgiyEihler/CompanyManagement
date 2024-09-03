using ErrorOr;
using System.Security.Claims;

namespace CompanyManagement.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static ErrorOr<Guid> GetUserId(this ClaimsPrincipal? principal)
    {
        var claimValue = principal?.FindFirst(SpecialClaims.UserId)?.Value;

        if (claimValue == null || !Guid.TryParse(claimValue, out var userId))
        {
            return Error.Forbidden();
        }

        return userId;
    }

    public static ErrorOr<HashSet<string>> GetPermissions(this ClaimsPrincipal? principal)
    {
        var permissions = principal?.FindAll(SpecialClaims.Permission);

        if (permissions == null)
        {
            return Error.Forbidden();
        }

        return permissions.Select(c => c.Value).ToHashSet();
    }
}
