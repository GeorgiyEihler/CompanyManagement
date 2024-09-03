using CompanyManagement.Domain.Participants;

namespace CompanyManagement.Application.Abstractions.Repositories;

public interface IParticipantRepository
{
    Task AddParticipantAsync(Participant participant, CancellationToken cancellationToken = default);
}
