using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Users.ChangePassword;

public sealed class ChangePasswordHandler
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordHandler(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> HandleAsync(ChangePasswordCommand command, CancellationToken cancellationToken = default)
    {
        var userId = UserId.Create(command.UserId);

        var userResult = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (userResult.IsError)
        {
            return userResult.FirstError;
        }

        var changePasswordResult = userResult.Value.ChangePassword(command.OldPassword, command.NewPassword, _passwordHasher);

        if (changePasswordResult.IsError)
        {
            return changePasswordResult.FirstError;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
