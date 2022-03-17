using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Core.Application.Extensions;
using SeedRoad.Common.Core.Application.Notifications;
using SeedRoad.Common.Core.Domain.Definitions;
using SeedRoad.Common.Core.Domain.Events;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Core.Application.Interceptors;

public class EventPublisherInterceptor : IInterceptor
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EventPublisherInterceptor> _logger;

    public EventPublisherInterceptor(IServiceProvider serviceProvider, ILogger<EventPublisherInterceptor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public void Intercept(IInvocation invocation)
    {
        if (!IsAggregateRepository(invocation.TargetType))
        {
            throw new ApplicationException(
                $"{invocation.GetType()} doesn't implement IAggregateRepository<,> interface");
        }

        invocation.Proceed();
        if (!IsSetAsyncMethod(invocation.Method))
        {
            return;
        }

        var dto = invocation.Arguments.First() as IAggregateDto;
        foreach (IDomainEvent aggregateEvent in dto.Events.ToList())
        {
            var notification = aggregateEvent.ToGenericType(typeof(DomainEventNotification<>));
            _serviceProvider.FireNotificationAndForget(notification, _logger);
        }

        dto.ClearEvents();
    }

    private bool IsAggregateRepository(Type invocationType)
    {
        return invocationType.GetInterfaces().Any(interfaceType =>
            interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition()
                .IsAssignableFrom(typeof(IAggregateRepository<,,>))
        );
    }

    private bool IsSetAsyncMethod(MethodInfo methodInfo)
    {
        return methodInfo.Name == nameof(IAggregateRepository<object, IAggregateDto, object>.SetAsync);
    }
}