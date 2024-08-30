namespace CompanyManagement.Domain.Companies.Employees;

public record ProjectCollection
{
    public IReadOnlyList<Project> Projects { get; init; }

    private ProjectCollection()
    {
    }

    public static ProjectCollection Create(IEnumerable<Project> projects) => new() { Projects = projects.ToList() };
}
