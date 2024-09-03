using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using ErrorOr;

namespace CompanyManagement.Application.Users.Login;

public class LoginHandler(
    IPasswordHasher hasher,
    IJwtGenerator jwtTokenGenerator,
    IUserRepository userRepository)
{
    private readonly IPasswordHasher _hasher = hasher;
    private readonly IJwtGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<LoginResponse>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var loginResult = Domain.Users.Login.Create(command.Login);

        if (loginResult.IsError)
        {
            return Error.Validation(code: "Login.Validation", description: "The login value is incorrect");
        }

        var userResult = await _userRepository.GetUserAsync(loginResult.Value, cancellationToken);

        if (userResult.IsError)
        {
            return Error.NotFound(code: "User.NotFound", description: "The user with login not found");
        }

        var user = userResult.Value;

        var isCorrectPassword = user.CheckPassword(command.Password, _hasher);

        if (!isCorrectPassword)
        {
            return Error.Forbidden(code: "User.Login", description: "The password is incorrect");
        }

        var jwtTokenResult = await _jwtTokenGenerator.GenerateToken(user);

        if (jwtTokenResult.IsError)
        {
            return jwtTokenResult.Errors;
        }

        return new LoginResponse(jwtTokenResult.Value);
    }
}
