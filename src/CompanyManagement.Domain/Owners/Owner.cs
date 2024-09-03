using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared.Ids;

namespace CompanyManagement.Domain.Owners;

public sealed class Owner : AggregateRoot<OwnerId>
{
    private Owner()
    {
    }

    public Owner(OwnerId id, DateTime created) : base(id, created)
    {
    }
}
