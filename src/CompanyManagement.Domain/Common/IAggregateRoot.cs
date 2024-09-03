namespace CompanyManagement.Domain.Common;

public interface IAggregateRoot
{
    void ClearDomainEvents();

    IReadOnlyList<IDomainEvent> PopDomainEvents();
}
