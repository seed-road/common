using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class Error<TReason, TTarget>
{
    private const string UnexpectedReason = "An unexcepted error occured";

    public Error()
    {
    }

    public Error(string? message = null, string? code = null, TReason? reason = default, TTarget? target = default,
        string? stackTrace = null)
    {
        Message = message;
        Reason = reason;
        Target = target;
        StackTrace = stackTrace;
        Code = code;
    }

    public string? Message { get; private set; }
    public TReason? Reason { get; private set; }
    public TTarget? Target { get; private set; }
    public string? StackTrace { get; set; }
    public string? Code { get; set; }


    public static Error<object, object> FromException(ExceptionWrapper exception, bool withTrace)
    {
        var error = withTrace
            ? new Error<object, object>(exception.Exception.Message, exception.Code,null, null, exception.Exception.StackTrace)
            : new Error<object, object>(exception.Exception.Message, exception.Code,null);
        switch (exception.Exception)
        {
            case ISubstantiateException substantiateException:
                error.Reason = substantiateException.ReasonObject;
                break;
            case ITargetException targetException:
                error.Target = targetException.TargetObject;
                break;
        }
        return error;
    }
}