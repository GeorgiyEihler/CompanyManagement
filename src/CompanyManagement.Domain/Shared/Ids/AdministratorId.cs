using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Shared.Ids;

public sealed class AdministratorId : BaseId
{
    public AdministratorId(Guid id) : base(id)
    {
    }

    public static AdministratorId NewId => new(Guid.NewGuid());

    public static AdministratorId Empty => new(Guid.Empty);

    public static AdministratorId Create(Guid id) => new(id);
}
