namespace SeedRoad.Common.Core.Domain.Exceptions;

public interface IApplicationException
{
    public string Message { get; }
    public string? StackTrace { get; }
}