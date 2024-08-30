using CompanyManagement.Application.CustomValidators;
using CompanyManagement.Domain.Companies;
using FluentValidation;

namespace CompanyManagement.Application.Comanies.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.Name).MustBeValueObject(Name.Create);

        RuleFor(c => c.Images)
            .Must(i => i.Count > 0)
            .ForEach(imageRule =>
            {
                imageRule.SetValidator(new CreateImageCommandValidator());
            });
    }
}

public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
{
    public CreateImageCommandValidator()
    {
        RuleFor(i => new { i.Name, i.Alt, i.Url }).MustBeValueObject(i => Image.Create(i.Name, i.Alt, i.Url));
    }
}