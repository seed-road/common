using MediatR;
using SeedRoad.Common.Application.Exceptions;
using SeedRoad.Common.Application.Notifications;
using SeedRoad.Common.Domain.Exceptions;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    private readonly IPublisher _publisher;

    public UnhandledExceptionBehavior(IPublisher publisher)
    {
        _publisher = publisher;
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
            if (exception is not IApplicationException)
            {
                throw await PublishUnhandledException(exception, cancellationToken);
            }

            var exceptionNotification = exception.ToGenericType(typeof(ExceptionNotification<>));
            await _publisher.Publish(exceptionNotification, cancellationToken);
            throw;
        }
    }

    private async Task<UnhandledException> PublishUnhandledException(Exception e,
        CancellationToken cancellationToken)
    {
        var unhandledException = new UnhandledException(e);
        var applicationException = new ExceptionNotification<UnhandledException>(unhandledException);
        await _publisher.Publish(applicationException, cancellationToken);
        return unhandledException;
    }
}