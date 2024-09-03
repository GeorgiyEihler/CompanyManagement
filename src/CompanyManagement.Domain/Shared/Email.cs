using CompanyManagement.Domain.Companies;
using ErrorOr;
using System.Net.Mail;

namespace CompanyManagement.Domain.Shared;

public record Email
{
    public string Value { get; init; } = default!;

    public Email(string value) => Value = value;

    public static ErrorOr<Email> Create(string value)
    {

        if (!MailAddress.TryCreate(value, out _))
        {
            return CompanyError.EmailValidationError;
        }

        return new Email(value);
    }
}