using ErrorOr;

namespace CompanyManagement.Domain.Companies;

public static class CompanyError
{
    public static readonly Error EmailValidationError = Error.Validation(code: "Email.Validation", description: "Email is not valid");

    public static readonly Error NameValidationError = Error.Validation(code: "Name:Validation", description: "Name is not valid");
}
