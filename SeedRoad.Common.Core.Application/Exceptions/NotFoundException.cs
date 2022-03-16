using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Core.Application.Exceptions;

public class NotFoundException<TId, TEntity> : Exception, ISubstantiateException
{
    public TId Id { get; }

    public NotFoundException(TId id) : base("Entity not found")
    {
        Id = id;
    }

    public object Reason => $"{typeof(TEntity)} not found by id : {Id}";
}