using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Shared.Ids;

namespace CompanyManagement.Domain.Companies.Employees;

public sealed class Employee : Entity<EmployeeId>, ISoftDeletable
{
    public FullName FullName { get; private set; }

    public Email Email { get; private set; }

    public CompanyId CompanyId { get; private set; }

    public ProjectCollection Projects { get; private set; }

    public EmployeeStatus EmployeeStatus { get; private set; }

    private Employee()
    {
    }

    public Employee(
        EmployeeId id,
        DateTime created,
        Email email,
        FullName fullName,
        CompanyId companyId,
        EmployeeStatus employeeStatus,
        ProjectCollection projects) : base(id, created)
    {
        FullName = fullName;
        Email = email;
        CompanyId = companyId;
        Projects = projects;
        EmployeeStatus = employeeStatus;
    }

    private bool _isDeleted;

    public void Delete() => _isDeleted = true;

    public void Restor() => _isDeleted = false;
}
