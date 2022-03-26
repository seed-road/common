using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Core.Application.Extensions;
using SeedRoad.Common.Core.Domain.Definitions;
using SeedRoad.Common.Core.Domain.Events;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Core.Application.Events;

public class EventPublisherInterceptor : AsyncInterceptorBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EventPublisherInterceptor> _logger;

    public EventPublisherInterceptor(IServiceProvider serviceProvider, ILogger<EventPublisherInterceptor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    private void PublishEvents(IInvocation invocation)
    {
        if (!IsSetAsyncMethod(invocation.Method))
        {
            return;
        }
        var dto = invocation.Arguments.First() as IAggregateDto;
        foreach (IDomainEvent aggregateEvent in dto.Events.ToList())
        {
            _logger.LogInformation("Send aggregate event  {AggregateEvent}", aggregateEvent.ToString());
            var notification = aggregateEvent.ToGenericType(typeof(DomainEventNotification<>));
            _serviceProvider.FireNotificationAndForget(notification, _logger);
        }

        dto.ClearEvents();
    }

    private void ValidateAggregateRepository(IInvocation invocation)
    {
        if (!IsAggregateRepository(invocation.TargetType))
        {
            throw new ApplicationException(
                $"{invocation.GetType()} doesn't implement {nameof(IAggregateRepository<object, WriteAggregateDto, object>)} interface");
        }
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

    protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
    {
        ValidateAggregateRepository(invocation);
        await proceed(invocation, proceedInfo);
        PublishEvents(invocation);
    }

    protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
    {
        ValidateAggregateRepository(invocation);
        TResult result = await proceed(invocation, proceedInfo);
        PublishEvents(invocation);
        return result;
    }
}