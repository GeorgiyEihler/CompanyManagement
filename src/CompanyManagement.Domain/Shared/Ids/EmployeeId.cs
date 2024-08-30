
using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Shared.Ids;

public sealed class EmployeeId : BaseId
{
    private EmployeeId(Guid id) : base(id)
    {
    }

    public static EmployeeId Empty => new(Guid.Empty);

    public static EmployeeId NewId => new(Guid.NewGuid());

    public static EmployeeId Create(Guid id) => new(id);
}
