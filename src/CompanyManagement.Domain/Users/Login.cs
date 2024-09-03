using ErrorOr;

namespace CompanyManagement.Domain.Users;

public record Login
{
    public string Value { get; init; }

    public Login(string value) => Value = value;

    public static ErrorOr<Login> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation(code: "Login.Validation", description: "The Login can not be empty");
        }

        return new Login(value);
    }
}
