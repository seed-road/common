using System.Collections.Immutable;
using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class HttpErrorServiceConfiguration
{
    public const int DefaultServerErrorCodeValue = 500;
    public const int DefaultMultipleExceptionCodeValue = 400;
    public const string DefaultServerTextErrorCodeValue = "UNHANDLED_ERROR";

    public int DefaultMultipleExceptionCode { get; set; } = DefaultMultipleExceptionCodeValue;
    public int DefaultServerErrorCode { get; set; } = DefaultServerErrorCodeValue;
    public string DefaultServerTextErrorCode { get; set; } = DefaultServerTextErrorCodeValue;
    public IEnumerable<ExceptionPriority> ExceptionPriorities { get; set; } = Enumerable.Empty<ExceptionPriority>();
    public IDictionary<Type, string> CodeMapping { get; set; } = new Dictionary<Type, string>();
    public bool WithTrace { get; set; }
}

public class HttpErrorService : IHttpErrorService
{
    private readonly int _defaultMultipleExceptionCode;
    private readonly int _defaultServerErrorCode;
    private readonly string _defaultServerTextErrorCode;

    private readonly ImmutableSortedSet<ExceptionPriority> _exceptionPriorities;
    private readonly IDictionary<Type, string> _codeMapping;
    private readonly bool _withTrace;

    public HttpErrorService(HttpErrorServiceConfiguration configuration)
    {
        _withTrace = configuration.WithTrace;
        _defaultServerErrorCode = configuration.DefaultServerErrorCode;
        _defaultMultipleExceptionCode = configuration.DefaultMultipleExceptionCode;
        _defaultServerTextErrorCode = configuration.DefaultServerTextErrorCode;
        _codeMapping = configuration.CodeMapping;
        _exceptionPriorities = configuration.ExceptionPriorities.ToImmutableSortedSet();
    }

    public HttpErrorWrapper ToHttpErrorWrapper(Exception exception, string instance)
    {
        ExceptionPriority? priority =
            _exceptionPriorities.FirstOrDefault(priority => priority.HandledType == exception.GetType());
        if (priority is not null)
        {
            return new HttpErrorWrapper(priority.HttpCode,
                HttpError.FromException(GetWrapper(exception), instance, _withTrace));
        }

        if (exception is ExceptionsAggregate exceptionsAggregate)
        {
            return HandleApplicationExceptionsAggregate(exceptionsAggregate, instance);
        }

        return new HttpErrorWrapper(_defaultServerErrorCode,
            HttpError.FromException(GetWrapper(exception), instance, _withTrace));
    }

    private ExceptionWrapper GetWrapper(Exception exception)
    {
        if (_codeMapping.TryGetValue(exception.GetType(), out var code))
        {
            return new ExceptionWrapper(code, exception);
        }

        return new ExceptionWrapper(_defaultServerTextErrorCode, exception);
    }

    private HttpErrorWrapper HandleApplicationExceptionsAggregate(ExceptionsAggregate exceptionsAggregate,
        string instance)
    {
        var prioritizedExceptionCode = _exceptionPriorities
            .LastOrDefault(priority => exceptionsAggregate.Exceptions
                .Any(exception => exception.GetType() == priority.HandledType))
            ?.HttpCode;
        var exceptionWrappers = exceptionsAggregate.Select(GetWrapper);
        HttpError httpError = HttpError.FromApplicationExceptionsAggregate(exceptionWrappers, instance, _withTrace);
        prioritizedExceptionCode ??= _defaultMultipleExceptionCode;
        return new HttpErrorWrapper(prioritizedExceptionCode.Value, httpError);
    }
}