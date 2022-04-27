namespace SeedRoad.Common.Core.Domain.Definitions;

public interface IEntityId
{
}

public interface IEntityId<T> : IEntityId
{
    public T Value { get;  }
    
}