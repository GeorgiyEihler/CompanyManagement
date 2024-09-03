using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Profiles.CreateOwnerProfile;

public sealed class CreateOwnerProfileHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOwnerProfileHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateOwnerProfileResponse>> HandleAsync(CreateOwnerProfileCommand command, CancellationToken cancellationToken = default)
    {
        var userResult = await _userRepository.GetUserByIdAsync(UserId.Create(command.UserId), cancellationToken);

        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var createOwnerProfileResult = userResult.Value.CreateOwnerProfile();

        if (createOwnerProfileResult.IsError)
        {
            return createOwnerProfileResult.Errors;
        }

        _userRepository.AddUserRole(userResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateOwnerProfileResponse(createOwnerProfileResult.Value.Id);
    }
}
