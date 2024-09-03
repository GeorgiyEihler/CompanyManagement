using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Participants;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users.Events;
using MediatR;

namespace CompanyManagement.Application.Participants.CreateParticipant;

internal sealed class ParticipantCreatedDomainEventHander : INotificationHandler<ParticipantCreatedEvent>
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantCreatedDomainEventHander(
        IParticipantRepository participantRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _participantRepository = participantRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ParticipantCreatedEvent notification, CancellationToken cancellationToken = default)
    {
        var participant = new Participant(ParticipantId.Create(notification.ParticipantId), _dateTimeProvider.UtcNow);

        await _participantRepository.AddParticipantAsync(participant, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
