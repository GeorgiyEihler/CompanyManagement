using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Shared.Ids;

public class OwnerId : BaseId
{
    private OwnerId(Guid id) : base(id)
    {
    }

    public static OwnerId Empty => new(Guid.Empty);

    public static OwnerId NewId => new(Guid.NewGuid());

    public static OwnerId Create(Guid id) => new(id);
}