using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users;
using ErrorOr;

namespace CompanyManagement.Application.Users.Register;

public class RegisterHander
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateProvider;

    public RegisterHander(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IDateTimeProvider dateProvider)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _dateProvider = dateProvider;
    }

    public async Task<ErrorOr<RegisterResponse>> HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var login = Domain.Users.Login.Create(command.Login);
        var email = Email.Create(command.Email);

        var isLoginExists = await _userRepository.IsUserWithSameLoginAlreadyExistsAsync(login.Value, cancellationToken);

        if (isLoginExists)
        {
            return Error.Conflict(code: "UM.Conflict.1343", "A user with the same login already exists.");
        }

        var isEmailExists = await _userRepository.IsUserWithSameEmailAlreadyExistsAsync(email.Value, cancellationToken);

        if (isEmailExists)
        {
            return Error.Conflict(code: "UM.Conflict.1344", description: "A user with the same email address already exists.");
        }

        var passwordHashResult = _passwordHasher.HashPassword(command.Password);

        if (passwordHashResult.IsError)
        {
            return passwordHashResult.FirstError;
        }

        var fullName = FullName.Create(command.Name, command.LastName, command.Patronymic);

        var user = new User(
            UserId.NewId,
            _dateProvider.UtcNow,
            fullName.Value,
            login.Value,
            email.Value, 
            passwordHashResult.Value);

        await _userRepository.AddUserAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterResponse(user.Id.Id);
    }
}
