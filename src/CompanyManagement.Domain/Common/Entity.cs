namespace CompanyManagement.Domain.Common;

public abstract class Entity<TId>
    where TId : BaseId
{
    public TId Id { get; init; }

    public DateTime CreatedOn { get; init; }

    public DateTime ModifiedOn { get; private set; }

    protected Entity()
    { 
    }

    protected Entity (
        TId id, 
        DateTime createdOn)
    {
        Id = id;
        CreatedOn = createdOn;
        ModifiedOn = createdOn;
    }
}
