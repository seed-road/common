namespace SeedRoad.Common.Infrastructure.Messaging.Exceptions;

public class InvalidMessageException: Exception
{
    public InvalidMessageException(string? message) : base(message)
    {
    }
}