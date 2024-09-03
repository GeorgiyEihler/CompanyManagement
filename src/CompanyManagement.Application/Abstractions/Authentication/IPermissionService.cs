using ErrorOr;

namespace CompanyManagement.Application.Abstractions.Authentication;

public interface IPermissionService
{
    Task<ErrorOr<PermissionsResponse>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken = default);
}
