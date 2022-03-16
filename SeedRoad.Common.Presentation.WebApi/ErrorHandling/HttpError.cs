using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class HttpError
{
    public HttpError()
    {
    }

    public HttpError(string url, IEnumerable<Error> errors)
    {
        Url = url;
        Errors = errors.ToList();
    }

    public string Url { get; set; }
    public List<Error> Errors { get; set; }

    public static HttpError FromSubstantiateException(ISubstantiateException applicationException, string instance,
        bool withTrace)
    {
        return new HttpError(instance, new[] { Error.FromApplicationException(applicationException, withTrace) });
    }

    public static HttpError FromException(Exception exception, string instance,
        bool withTrace)
    {
        return new HttpError(instance, new[] { Error.FromException(exception, withTrace) });
    }

    public static HttpError FromApplicationExceptionsAggregate(ExceptionsAggregate exception,
        string instance, bool withTrace)
    {
        return new HttpError(instance, exception.Select(e => Error.FromApplicationException(e, withTrace)));
    }
}