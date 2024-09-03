using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users;
using CompanyManagement.Infrastructure.Persistence;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Infrastructure.Users.Persistanse;

internal sealed class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<ErrorOr<User>> GetUserAsync(Login login, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);

        if (user is null)
        {
            return Error.NotFound(code: "User.NotFound", description: $"The user with login: {login.Value} not found");
        }

        return user;
    }

    public async Task<ErrorOr<User>> GetUserByIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user is null)
        {
            return Error.NotFound();
        }

        return user;
    }

    public async Task<bool> IsUserWithSameEmailAlreadyExistsAsync(Email email, CancellationToken cancellationToken)
    {
        var isExistst = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        return isExistst is not null;
    }

    public async Task<bool> IsUserWithSameLoginAlreadyExistsAsync(Login login, CancellationToken cancellationToken)
    {
        var isExistst = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);

        return isExistst is not null;
    }
}
