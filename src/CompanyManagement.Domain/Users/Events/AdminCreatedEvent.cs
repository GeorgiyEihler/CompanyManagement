using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Users.Events;

public record AdminCreatedEvent(Guid AdminId) : IDomainEvent;