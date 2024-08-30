using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Companies.Employees;
using CompanyManagement.Domain.Shared.Ids;

namespace CompanyManagement.Domain.Companies;

public sealed class Company : AggregateRoot<CompanyId>, ISoftDeletable
{
    public Name Name { get; private set; }

    public Number Number { get; private set; }

    private readonly List<Employee> _employees = [];

    public IReadOnlyList<Employee> Employees => _employees; 

    public ImagesCollection Images { get; private set; }

    private Company()
    {
    }

    public Company(
        CompanyId id,
        DateTime created,
        Name name, 
        Number number,
        ImagesCollection imagesCollection) 
        : base(id, created)
    {
        Name = name;
        Number = number;
        Images = imagesCollection;
    }

    private bool _isDeleted;

    public void Delete()
    {
        _isDeleted = true;

        foreach (var employee in _employees)
        {
            employee.Delete();
        }
    }

    public void Restor() => _isDeleted = false;

    public void SetEmployee(List<Employee> employeeList) => _employees.AddRange(employeeList);
}
