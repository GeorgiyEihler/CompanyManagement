using CompanyManagement.Application.Tests.Inegration.Infrastructure;
using CompanyManagement.Application.Users.ChangePassword;
using FluentAssertions;

namespace CompanyManagement.Application.Tests.Inegration.Users;

public class ChangePasswordTests : IntegrationTestBase
{
    private ChangePasswordHandler _sut;

    private const string OldPassword = "qewQWE123!@#";
    private const string NewStrongPassword = "ertert!@#!@#234SSdf";
    public ChangePasswordTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _sut = new ChangePasswordHandler(
            _passwordHasher,
            _userRepository, 
            _unitOfWork);
    }

    [Fact]
    public async Task HandeAsync_WhenUserChangeToStrongPassword_ShouldChangePasswordSuccesfully()
    {
        var user = _dbContxet.Users.First();

        var changePasswordCommand = new ChangePasswordCommand(user.Id.Id, OldPassword, NewStrongPassword);

        var changePasswordResult = await _sut.HandleAsync(changePasswordCommand);

        var chenkPasswordResult = user.CheckPassword(NewStrongPassword, _passwordHasher);

        changePasswordResult.IsError.Should().BeFalse();
        chenkPasswordResult.Should().BeTrue();
    }
}
