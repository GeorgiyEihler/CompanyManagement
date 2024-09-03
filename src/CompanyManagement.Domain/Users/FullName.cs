using ErrorOr;

namespace CompanyManagement.Domain.Users;

public record FullName
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Patronymic { get; init; }

    private FullName(
        string firstName,
        string lastName,
        string patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public static ErrorOr<FullName> Create(
        string firstName,
        string lastName,
        string patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Error.Validation(code: "FullName.FistName.Validation", description: "The First name is invalid");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Error.Validation(code: "FullName.LastName.Validation", description: "The Last name is invalid");
        }

        if (string.IsNullOrWhiteSpace(patronymic))
        {
            return Error.Validation(code: "FullName.Patronymic.Validation", description: "The Patronymic is invalid");
        }

        return new FullName(firstName, lastName, patronymic);
    }
}
