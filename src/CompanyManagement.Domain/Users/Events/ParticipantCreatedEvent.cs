using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Users.Events;

public record ParticipantCreatedEvent(Guid ParticipantId) : IDomainEvent;