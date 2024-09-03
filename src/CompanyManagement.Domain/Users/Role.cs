using CompanyManagement.Domain.DomainConstants;

namespace CompanyManagement.Domain.Users;

public sealed class Role
{
    public string Name { get; init; }

    private Role(string name) => Name = name;

    private Role()
    {
    }

    public static readonly Role Owner = new(Constants.Role.Owner);

    public static readonly Role Administrator = new(Constants.Role.Administrator);

    public static readonly Role Participatn = new(Constants.Role.Participant);
}
