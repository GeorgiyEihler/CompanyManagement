using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared.Ids;

namespace CompanyManagement.Domain.Participants;

public sealed class Participant : AggregateRoot<ParticipantId>
{
    private Participant()
    {
    }

    public Participant(
        ParticipantId id, 
        DateTime created)
        : base(id, created) 
    { 
    }
}
