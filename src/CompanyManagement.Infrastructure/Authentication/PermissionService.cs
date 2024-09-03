using CompanyManagement.Application.Abstractions.Authentication;
using CompanyManagement.Application.Users.GetPermission;
using ErrorOr;
using System.Threading;

namespace CompanyManagement.Infrastructure.Authentication;

internal sealed class PermissionService : IPermissionService
{
    private readonly GetPermissionQueryHandler _queryHandler;

    public PermissionService(GetPermissionQueryHandler queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public async Task<ErrorOr<PermissionsResponse>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var permissionResult = await _queryHandler.HandleAsync(new GetPermissionQuery(userId), cancellationToken);

        return permissionResult;
    }
}
