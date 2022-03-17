using MediatR;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Core.Application.Exceptions;
using SeedRoad.Common.Core.Application.Extensions;
using SeedRoad.Common.Core.Application.Notifications;
using SeedRoad.Common.Core.Domain.Exceptions;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Core.Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehavior(IServiceProvider provider,
        ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
    {
        _provider = provider;
        _logger = logger;
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
            if (exception is not IDomainException)
            {
                throw await PublishUnhandledException(exception);
            }

            var exceptionNotification = exception.ToGenericType(typeof(ExceptionNotification<>));
            _provider.FireNotificationAndForget(exceptionNotification, _logger);
            throw;
        }
    }

    private async Task<UnhandledException> PublishUnhandledException(Exception e)
    {
        var unhandledException = new UnhandledException(e);
        var applicationException = new ExceptionNotification<UnhandledException>(unhandledException);
        _provider.FireNotificationAndForget(applicationException, _logger);
        return unhandledException;
    }
}