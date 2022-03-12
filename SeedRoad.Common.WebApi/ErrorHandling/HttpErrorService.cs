using System.Collections.Immutable;
using SeedRoad.Common.Domain.Exceptions;

namespace SeedRoad.Common.WebApi.ErrorHandling;

public class HttpErrorService : IHttpErrorService
{
    public const int DefaultServerErrorCode = 500;
    public const int DefaultMultipleExceptionCode = 400;
    private readonly int _defaultMultipleExceptionCode;
    private readonly int _defaultServerErrorCode;
    private readonly ImmutableSortedSet<ExceptionPriority> _exceptionPriorities;
    private readonly bool _withTrace;

    public HttpErrorService(ImmutableSortedSet<ExceptionPriority> exceptionPriorities, bool withTrace,
        int defaultServerErrorCode, int defaultMultipleExceptionCode)
    {
        _withTrace = withTrace;
        _defaultServerErrorCode = defaultServerErrorCode;
        _defaultMultipleExceptionCode = defaultMultipleExceptionCode;
        _exceptionPriorities = exceptionPriorities;
    }

    public HttpErrorWrapper ToHttpErrorWrapper(Exception exception, string instance)
    {
        ExceptionPriority? priority =
            _exceptionPriorities.FirstOrDefault(priority => priority.HandledType == exception.GetType());
        if (priority is not null && exception is ISubstantiateException substantiateException)
        {
            return new HttpErrorWrapper(priority.HttpCode,
                HttpError.FromApplicationException(substantiateException, instance, _withTrace));
        }

        if (exception is ExceptionsAggregate exceptionsAggregate)
        {
            return HandleApplicationExceptionsAggregate(exceptionsAggregate, instance);
        }

        return new HttpErrorWrapper(_defaultServerErrorCode, HttpError.FromException(exception, instance, _withTrace));
    }

    public static HttpErrorService DefaultErrorService(IEnumerable<ExceptionPriority> priorities, bool withTrace)
    {
        return new HttpErrorService(priorities.ToImmutableSortedSet(), withTrace, DefaultServerErrorCode,
            DefaultMultipleExceptionCode);
    }

    private HttpErrorWrapper HandleApplicationExceptionsAggregate(ExceptionsAggregate exceptionsAggregate,
        string instance)
    {
        var prioritizedExceptionCode = _exceptionPriorities
            .LastOrDefault(priority => exceptionsAggregate.Exceptions
                .Any(exception => exception.GetType() == priority.HandledType))
            ?.HttpCode;
        HttpError httpError = HttpError.FromApplicationExceptionsAggregate(exceptionsAggregate, instance, _withTrace);
        prioritizedExceptionCode ??= _defaultMultipleExceptionCode;
        return new HttpErrorWrapper(prioritizedExceptionCode.Value, httpError);
    }
}