using System.Reflection;
using Castle.DynamicProxy;
using MediatR;
using SeedRoad.Common.Application.Contracts;
using SeedRoad.Common.Application.Notifications;
using SeedRoad.Common.Domain.Contracts;
using SeedRoad.Common.Domain.Events;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Application.Interceptors;

public class EventPublisherInterceptor : IInterceptor
{
    private readonly IPublisher _publisher;


    public EventPublisherInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
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

        var aggregate = invocation.Arguments.First() as Aggregate<IEntityId>;
        foreach (IDomainEvent aggregateEvent in aggregate.Events.ToList())
        {
            _publisher.Publish(aggregateEvent.ToGenericType(typeof(DomainEventNotification<>)));
        }

        aggregate.ClearEvents();
    }

    private bool IsAggregateRepository(Type invocationType)
    {
        return invocationType.GetInterfaces().Any(interfaceType =>
            interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition()
                .IsAssignableFrom(typeof(IAggregateRepository<,>))
        );
    }

    private bool IsSetAsyncMethod(MethodInfo methodInfo)
    {
        return methodInfo.Name == nameof(IAggregateRepository<IEntityId, Aggregate<IEntityId>>.SetAsync);
    }
}