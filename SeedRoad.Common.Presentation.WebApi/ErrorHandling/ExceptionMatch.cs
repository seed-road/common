using System.Net;
using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class ExceptionMatch
{
    private ExceptionMatch(ExceptionPriority? priority, ExceptionCode? code)
    {
        Priority = priority;
        Code = code;
    }


    public static ExceptionMatch New<TException>(int weight, int httpCode, string code)
        where TException : IDomainException
    {
        return new ExceptionMatch(ExceptionPriority.New<TException>(weight, httpCode),
            ExceptionCode.New<TException>(code));
    }
    
    public static ExceptionMatch New<TException>(int weight, HttpStatusCode httpCode, string code)
        where TException : IDomainException
    {
        return new ExceptionMatch(ExceptionPriority.New<TException>(weight, (int) httpCode),
            ExceptionCode.New<TException>(code));
    }

    public static ExceptionMatch New<TException>(int weight, int httpCode)
        where TException : IDomainException
    {
        return new ExceptionMatch(ExceptionPriority.New<TException>(weight, httpCode), null);
    }

    public static ExceptionMatch New<TException>(string code)
        where TException : IDomainException
    {
        return new ExceptionMatch(null, ExceptionCode.New<TException>(code));
    }

    public ExceptionPriority? Priority { get; }
    public ExceptionCode? Code { get; }
}