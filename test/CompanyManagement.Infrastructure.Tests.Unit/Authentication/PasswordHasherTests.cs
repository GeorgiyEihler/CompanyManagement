using CompanyManagement.Infrastructure.Authentication;
using FluentAssertions;
using Xunit.Abstractions;

namespace CompanyManagement.Infrastructure.Tests.Unit.Authentication;

public class PasswordHasherTests
{
    private readonly PasswordHanser _sut = new();
    private readonly ITestOutputHelper _output;

    public PasswordHasherTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void IsCorrectPassword_WhenComparingPasswordAndHans_SouldReturnTrue()
    {
        var password = "123!@#qweQWE";

        var hash = _sut.HashPassword(password).Value;

        _output.WriteLine($"Hash for password {password} is {hash}");

        var resut = _sut.IsCorrectPassword(password, hash);

        resut.Should().BeTrue();
    }
}
