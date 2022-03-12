namespace SeedRoad.Common.Domain.Exceptions;

public interface IApplicationException
{
    public string Message { get; }
    public string? StackTrace { get; }
}