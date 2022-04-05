using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class ExceptionCode
{
    public Type HandledType { get; }
    public string Code { get; }

    private ExceptionCode(Type handledType, string code)
    {
        HandledType = handledType;
        Code = code;
    }

    public static ExceptionCode New<TException>(string code) where TException : IDomainException
    {
        return new ExceptionCode(typeof(TException), code);
    }

    protected bool Equals(ExceptionCode other)
    {
        return HandledType.Equals(other.HandledType);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ExceptionCode) obj);
    }

    public override int GetHashCode()
    {
        return HandledType.GetHashCode();
    }
}