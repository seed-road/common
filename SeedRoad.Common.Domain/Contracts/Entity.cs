namespace SeedRoad.Common.Domain.Contracts;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : IEntityId
{
    public Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; init; }

    public bool Equals(Entity<TId>? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }
}