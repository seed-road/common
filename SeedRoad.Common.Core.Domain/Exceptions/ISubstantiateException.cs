namespace SeedRoad.Common.Core.Domain.Exceptions;

public interface ISubstantiateException : IDomainException
{
    public object Reason { get; }
}
