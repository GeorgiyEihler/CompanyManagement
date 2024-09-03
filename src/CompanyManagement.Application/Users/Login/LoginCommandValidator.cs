using CompanyManagement.Application.CustomValidators;
using FluentValidation;

namespace CompanyManagement.Application.Users.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Password).NotEmpty();

        RuleFor(c => c.Login).MustBeValueObject(l => Domain.Users.Login.Create(l));

    }
}
