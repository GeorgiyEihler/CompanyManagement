namespace CompanyManagement.Domain.Companies.Employees;

public record FullName
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Patronymic { get; init; } = default!;

    private FullName(
        string firstName,
        string lastName,
        string patronymic) =>
        (FirstName, LastName, Patronymic) = (firstName, lastName, patronymic);

    public static FullName Create(
        string firstName,
        string lastName,
        string patronymic)
    {


        return new FullName(firstName, lastName, patronymic);
    }
}