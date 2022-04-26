namespace SeedRoad.Common.Core.Application.Validation;

public interface IRequestValidator
{
    Task Validate(object command);
}

public interface IRequestValidator<TRequest> : IRequestValidator where TRequest : class
{
    Task Validate(TRequest command);
}

public abstract class RequestValidator<TRequest> : IRequestValidator<TRequest> where TRequest : class
{
    public abstract Task Validate(TRequest command);
    Task IRequestValidator.Validate(object command)
    {
        return Validate((TRequest) command);
    }
}