using CompanyManagement.Domain.Common;

namespace CompanyManagement.Domain.Users.Events;

public record OwnerCreatedEvent(Guid OwnerId) : IDomainEvent;