using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Shared.Ids;

public sealed class UserId : BaseId
{
    public UserId(Guid id) : base(id)
    {
    }

    public static UserId NewId => new(Guid.NewGuid());

    public static UserId Empty => new(Guid.Empty);

    public static UserId Create(Guid id) => new(id);
}
