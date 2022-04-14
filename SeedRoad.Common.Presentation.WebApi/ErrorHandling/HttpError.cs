using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class HttpError
{
    public HttpError(string url, IEnumerable<Error<object, object>> errors)
    {
        Url = url;
        Errors = errors.ToList();
    }

    public string Url { get; set; }
    public List<Error<object,object>> Errors { get; set; }
    
    public static HttpError FromException(ExceptionWrapper exception, string instance,
        bool withTrace)
    {
        var error = Error<string, string>.FromException(exception, withTrace);
        return new HttpError(instance, new[] {error});
    }

    public static HttpError FromApplicationExceptionsAggregate(IEnumerable<ExceptionWrapper> exceptions, 
        string instance, bool withTrace)
    {
        return new HttpError(instance, exceptions.Select(e => Error<object, object>.FromException(e, withTrace)));
    }
}