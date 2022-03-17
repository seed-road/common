namespace SeedRoad.Common.Core.Domain.Exceptions;

public interface IDomainException
{
    public string Message { get; }
    public string? StackTrace { get; }
}