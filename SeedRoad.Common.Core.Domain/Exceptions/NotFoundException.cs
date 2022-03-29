using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Core.Domain.Exceptions;

public class NotFoundException : Exception, IDomainException
{
    public NotFoundException() : base("Entity not found")
    {
    }
}

public class NotFoundException<TId, TEntity> : NotFoundException, ISubstantiateException<string>
{
    public TId Id { get; }

    public NotFoundException(TId id)
    {
        Id = id;
    }

    public virtual string Reason => $"{typeof(TEntity)} not found by id : {Id}";
}