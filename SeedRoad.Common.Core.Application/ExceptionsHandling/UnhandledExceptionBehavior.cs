using MediatR;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Core.Application.Exceptions;
using SeedRoad.Common.Core.Application.Extensions;
using SeedRoad.Common.Core.Application.Notifications;
using SeedRoad.Common.Core.Domain.Exceptions;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Core.Application.ExceptionsHandling;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;
    private readonly IExceptionsAggregate _exceptionsAggregate;

    public UnhandledExceptionBehavior(IServiceProvider provider,
        ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger, IExceptionsAggregate exceptionsAggregate)
    {
        _provider = provider;
        _logger = logger;
        _exceptionsAggregate = exceptionsAggregate;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            if (exception != _exceptionsAggregate)
            {
                _exceptionsAggregate.Add(exception);
            }
            foreach (var innerException in _exceptionsAggregate)
            {
                var exceptionNotification = innerException is IDomainException
                    ? innerException.ToGenericTypeInstance(typeof(ExceptionNotification<>))
                    : new ExceptionNotification<UnhandledException>(new UnhandledException(innerException));
                _provider.FireNotificationAndForget(exceptionNotification, _logger);
            }
            throw _exceptionsAggregate.AggregatedException;
        }
    }
}