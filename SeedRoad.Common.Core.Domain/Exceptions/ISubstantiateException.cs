namespace SeedRoad.Common.Core.Domain.Exceptions;

public interface ISubstantiateException : IDomainException
{
    object ReasonObject { get; }
}

public interface ISubstantiateException<T> : ISubstantiateException where T : notnull
{
    object ISubstantiateException.ReasonObject => Reason;
    public T Reason { get; }
}

