namespace SeedRoad.Common.Core.Domain.Exceptions;

public interface ITargetException: IDomainException
{
    public object TargetObject { get; }
}
public interface ITargetException<T> : ITargetException
{
    object ITargetException.TargetObject => Target;
    public T Target { get; }
}