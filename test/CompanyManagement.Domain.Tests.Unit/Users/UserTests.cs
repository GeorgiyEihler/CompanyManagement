using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Tests.Unit.Common.Factories;
using NSubstitute;

namespace CompanyManagement.Domain.Tests.Unit.Users;

public class UserTests
{
    public void ChangePassword_WhenChangePassword_ShouldChangePasswordSuccesfully()
    {
        var oldPassword = "123QWeqwe$#!";
        var newPassword = "321QWeqwe$#!";

        var hasher = Substitute.For<IPasswordHasher>();

        hasher.HashPassword("123QWeqwe$#!").Returns(oldPassword);
        hasher.HashPassword("321QWeqwe$#!").Returns(newPassword);

    }
}
