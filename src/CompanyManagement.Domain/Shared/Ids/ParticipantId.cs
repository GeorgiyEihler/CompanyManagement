using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Shared.Ids;

public sealed class ParticipantId : BaseId
{
    public ParticipantId(Guid id) : base(id)
    {
    }

    public static ParticipantId Empty => new(Guid.Empty);

    public static ParticipantId NewId => new(Guid.NewGuid());

    public static ParticipantId Create(Guid id) => new(id);
}
