using SeedRoad.Common.Domain.Exceptions;

namespace SeedRoad.Common.WebApi.ErrorHandling;

public class Error
{
    private const string UnexpectedReason = "An unexcepted error occured";

    public Error()
    {
    }

    public Error(string message, string reason, string? stackTrace = null)
    {
        Message = message;
        Reason = reason;
        StackTrace = stackTrace;
    }

    public string Message { get; set; }
    public string Reason { get; set; }
    public string? StackTrace { get; set; }

    public static Error FromApplicationException(ISubstantiateException applicationException, bool withTrace)
    {
        if (withTrace)
        {
            return new Error(applicationException.Message, applicationException.Reason,
                applicationException.StackTrace);
        }

        return new Error(applicationException.Message, applicationException.Reason);
    }

    public static Error FromException(Exception exception, bool withTrace)
    {
        return withTrace
            ? new Error(exception.Message, UnexpectedReason, exception.StackTrace)
            : new Error(exception.Message, UnexpectedReason);
    }
}