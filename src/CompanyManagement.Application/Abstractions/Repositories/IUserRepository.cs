using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users;
using ErrorOr;

namespace CompanyManagement.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<ErrorOr<User>> GetUserAsync(Login login, CancellationToken cancellationToken);

    Task<ErrorOr<User>> GetUserByIdAsync(UserId userId, CancellationToken cancellationToken);

    Task<bool> IsUserWithSameEmailAlreadyExistsAsync(Email email, CancellationToken cancellationToken);

    Task<bool> IsUserWithSameLoginAlreadyExistsAsync(Login login, CancellationToken cancellationToken);

    Task AddUserAsync(User user, CancellationToken cancellationToken);
}
