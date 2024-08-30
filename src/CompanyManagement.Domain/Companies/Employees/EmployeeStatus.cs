using Ardalis.SmartEnum;

namespace CompanyManagement.Domain.Companies.Employees;

public class EmployeeStatus : SmartEnum<EmployeeStatus>
{
    public static readonly EmployeeStatus Work = new(nameof(Work), 0);
    public static readonly EmployeeStatus Vacation = new(nameof(Vacation), 1);
    public static readonly EmployeeStatus Fired = new(nameof(Fired), 2);

    public EmployeeStatus(string name, int value) : base(name, value)
    {
    }
}
