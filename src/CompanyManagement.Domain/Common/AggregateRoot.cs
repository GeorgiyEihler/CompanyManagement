namespace CompanyManagement.Domain.Common;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : BaseId
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id, DateTime created) : base(id, created)
    {
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IReadOnlyList<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();

        _domainEvents.Clear();

        return copy;
    }
}
