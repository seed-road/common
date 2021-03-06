using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.ErrorHandling;

public class ExceptionPriority : IEquatable<ExceptionPriority>
{
    private ExceptionPriority(int weight, int httpCode, Type handledType)
    {
        Weight = weight;
        HttpCode = httpCode;
        HandledType = handledType;
    }

    public int Weight { get; }
    public int HttpCode { get; }
    public Type HandledType { get; }


    public bool Equals(ExceptionPriority? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return HandledType == other.HandledType;
    }

    public static ExceptionPriority New<TException>(int weight, int httpCode) where TException : IDomainException
    {
        return new ExceptionPriority(weight, httpCode, typeof(TException));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((ExceptionPriority) obj);
    }

    public override int GetHashCode()
    {
        return HandledType.GetHashCode();
    }
}