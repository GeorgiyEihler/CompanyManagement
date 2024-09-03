using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Profiles.CreateAdminProfile;

public sealed class CreateAdminProfileHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdminProfileHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateAdminProfileResponse>> HandleAsync(CreateAdminProfileCommand command, CancellationToken cancellationToken = default)
    {
        var adminUserResult = await _userRepository.GetUserByIdAsync(UserId.Create(command.AdminUserId), cancellationToken);

        if (adminUserResult.IsError)
        {
            return adminUserResult.Errors;
        }

        if (adminUserResult.Value.AdministratorId is null)
        {
            return Error.Forbidden();
        }

        var userResult = await _userRepository.GetUserByIdAsync(UserId.Create(command.UserId), cancellationToken);

        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var createAdminResult = userResult.Value.CreateAdminProfile();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Error.Validation();
    }
}
