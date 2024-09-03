using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared.Ids;

namespace CompanyManagement.Domain.Administrators;

public sealed class Administrator : AggregateRoot<AdministratorId>
{
    private Administrator()
    {
    }

    public Administrator(
        AdministratorId id,
        DateTime created) 
        : base(id, created)
    {
    }
}
