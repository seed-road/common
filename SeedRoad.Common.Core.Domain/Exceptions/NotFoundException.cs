using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Core.Domain.Exceptions;

public class NotFoundException : Exception, IDomainException
{
    public NotFoundException() : base("Entity not found")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}

public class NotFoundException<TId, TEntity> : NotFoundException, ITargetException<TId>
{
    public NotFoundException(TId id) : base($"{typeof(TEntity)} not found by : {id}")
    {
        Target = id;
    }

    public TId Target { get; }
}

public class NotFoundException<TId> : NotFoundException, ITargetException<TId>
{
    public NotFoundException(string entityName, TId id) : base($"{entityName} not found by : {id}")
    {
        Target = id;
    }

    public TId Target { get; }
}