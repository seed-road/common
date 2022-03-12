namespace SeedRoad.Common.Domain.Exceptions;

public interface ISubstantiateException : IApplicationException
{
    public string Reason { get; }
}