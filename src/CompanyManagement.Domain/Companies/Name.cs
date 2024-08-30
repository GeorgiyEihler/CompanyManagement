using ErrorOr;

namespace CompanyManagement.Domain.Companies;

public record Name
{
    public string Value { get; init; }

    private Name(string value) => Value = value;

    public static ErrorOr<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CompanyError.NameValidationError;
        }

        return new Name(value);
    }
}