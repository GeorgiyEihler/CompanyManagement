namespace CompanyManagement.Domain.Common;

public abstract class BaseId
{
    public Guid Id { get; init; }

    protected BaseId(Guid id) => Id = id;

    public override int GetHashCode() => (Id + GetType().FullName).GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var oterId = obj as BaseId;


        return oterId!.Id == Id;
    }

    public static bool operator ==(BaseId left, BaseId right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(BaseId left, BaseId right) => !(left == right);

    public static implicit operator Guid (BaseId id) => id.Id;
}