using SeedRoad.Common.Core.Domain.Exceptions;

namespace SeedRoad.Common.Presentation.WebApi.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Action need authenticated user")
    {
    }
}

public class UnauthorizedException<TTarget> : UnauthorizedException, ITargetException<TTarget>
{
    public UnauthorizedException(TTarget target) : base()
    {
        Target = target;
    }

    public TTarget Target { get; }
}