using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class Error
{
    private const string UnexpectedReason = "An unexcepted error occured";

    public Error()
    {
    }

    public Error(string message, object reason, string? stackTrace = null)
    {
        Message = message;
        Reason = reason;
        StackTrace = stackTrace;
    }

    public string Message { get; set; }
    public object Reason { get; set; }
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