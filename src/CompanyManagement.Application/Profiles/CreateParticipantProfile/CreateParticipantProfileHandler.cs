using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Profiles.CreateParticipantProfile;

public sealed class CreateParticipantProfileHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateParticipantProfileHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateParticipantProfileResponse>> HandleAsync(
    CreateParticipantProfileCommand command, 
        CancellationToken cancellationToken = default)
    {
        var userResult = await _userRepository.GetUserByIdAsync(UserId.Create(command.UserId), cancellationToken);

        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var createParticipantProfileResult = userResult.Value.CreatePrticipantProfile();

        if (createParticipantProfileResult.IsError)
        {
            return createParticipantProfileResult.Errors;
        }

        _userRepository.AddUserRole(userResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateParticipantProfileResponse(createParticipantProfileResult.Value.Id);
    }
}
