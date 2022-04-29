using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;
    private readonly IExceptionsAggregate _exceptionsAggregate;

    public UnhandledExceptionBehavior(IServiceScopeFactory scopeFactory,
        ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger, IExceptionsAggregate exceptionsAggregate)
    {
        _scopeFactory = scopeFactory;
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
                _scopeFactory.FireNotificationAndForget(exceptionNotification, _logger);
            }
            throw _exceptionsAggregate.AggregatedException;
        }
    }
}