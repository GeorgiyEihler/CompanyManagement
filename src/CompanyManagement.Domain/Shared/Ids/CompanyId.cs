
using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Shared.Ids;

public sealed class CompanyId : BaseId
{
    private CompanyId(Guid id) : base(id)
    {
    }

    public static CompanyId Empty => new(Guid.Empty);

    public static CompanyId NewId => new(Guid.NewGuid());

    public static CompanyId Create(Guid id) => new(id);

    public static implicit operator Guid(CompanyId id) => id.Id;
}
