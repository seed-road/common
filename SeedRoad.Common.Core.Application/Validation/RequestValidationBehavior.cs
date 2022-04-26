using FluentValidation;
using MediatR;

namespace SeedRoad.Common.Core.Application.Validation;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IRequestValidator> _validators;

    public RequestValidationBehavior(IEnumerable<IRequestValidator> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var reqType = typeof(TRequest);
        foreach (var validator in _validators)
        {
            var validatorType = validator.GetType();
            var isValidatorForRequest = validatorType.GetInterfaces()
                .Any(validatorInterface => validatorInterface.GenericTypeArguments
                    .Any(interfaceGeneric => reqType.IsAssignableTo(interfaceGeneric)));
            if (isValidatorForRequest)
            {
                await validator.Validate(request);
            }
        }
        return await next();
    }
}