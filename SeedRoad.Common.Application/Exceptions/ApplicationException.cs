using SeedRoad.Common.Domain.Exceptions;

namespace SeedRoad.Common.Application.Exceptions;

public class ApplicationException : Exception, IApplicationException
{
    public ApplicationException(string? message) : base(message)
    {
    }
}