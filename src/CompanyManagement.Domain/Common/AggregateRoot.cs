namespace CompanyManagement.Domain.Common;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : BaseId
{
    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id, DateTime created) : base(id, created)
    {
    }
}
