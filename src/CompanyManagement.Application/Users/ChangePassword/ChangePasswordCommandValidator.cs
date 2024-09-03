using CompanyManagement.Application.CustomValidators;
using FluentValidation;

namespace CompanyManagement.Application.Users.ChangePassword;

public class ChangePasswordCommandValidator: AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.NewPassword).MustBeStrongPassword();
    }
}