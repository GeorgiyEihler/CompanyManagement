using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Participants;
using CompanyManagement.Infrastructure.Persistence;

namespace CompanyManagement.Infrastructure.Participants.Persistence;

internal sealed class ParticipantRepository : IParticipantRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ParticipantRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task AddParticipantAsync(Participant participant, CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.Participants.AddAsync(participant, cancellationToken);
    }
}
