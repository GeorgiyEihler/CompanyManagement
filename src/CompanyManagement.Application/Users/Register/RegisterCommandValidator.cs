using CompanyManagement.Application.CustomValidators;
using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Users;
using FluentValidation;

namespace CompanyManagement.Application.Users.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => new { FirstName = c.Name, c.LastName, c.Patronymic })
            .MustBeValueObject(fn => FullName.Create(fn.FirstName, fn.LastName, fn.Patronymic));

        RuleFor(c => c.Login)
            .MustBeValueObject(Domain.Users.Login.Create);

        RuleFor(c => c.Email)
            .MustBeValueObject(Email.Create);
    }
}
