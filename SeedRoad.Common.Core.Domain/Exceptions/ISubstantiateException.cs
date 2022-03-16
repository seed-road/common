namespace SeedRoad.Common.Core.Domain.Exceptions;

public interface ISubstantiateException : IApplicationException
{
    public object Reason { get; }
}
