using System.Reflection;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Core.Application.Behaviors;
using SeedRoad.Common.Core.Application.Interceptors;
using SeedRoad.Common.System;

namespace SeedRoad.Common.Core.Application;

public static class DependencyInjection
{
    public static Func<IServiceProvider, object> ProxyFactory<TInterface, TImplementation>(
        params Type[] interceptorsType)
        where TInterface : class where TImplementation : class, TInterface
    {
        return serviceProvider =>
        {
            ProxyGenerator proxyGenerator = PrepareProxyFactory(interceptorsType, serviceProvider,
                out TImplementation actual, out var interceptors);
            return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
        };
    }

    public static Func<IServiceProvider, object> ProxyFactory<TImplementation>(params Type[] interceptorsType)
        where TImplementation : class
    {
        return serviceProvider =>
        {
            ProxyGenerator proxyGenerator = PrepareProxyFactory(interceptorsType, serviceProvider,
                out TImplementation actual, out var interceptors);
            return proxyGenerator.CreateClassProxyWithTarget(typeof(TImplementation), actual, interceptors);
        };
    }

    public static IServiceCollection AddProxyScoped<TInterface, TImplementation>(this IServiceCollection serviceCollection, Type interceptorType)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var factory = ProxyFactory<TInterface, TImplementation>(interceptorType);
        return serviceCollection.AddScoped(typeof(TInterface), factory);
    }
    

    private static ProxyGenerator PrepareProxyFactory<TImplementation>(Type[] interceptorsType,
        IServiceProvider serviceProvider, out TImplementation actual, out IInterceptor[] interceptors)
        where TImplementation : class
    {
        var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
        actual = ActivatorUtilities.CreateInstance<TImplementation>(serviceProvider);
        interceptors = GetInterceptors(interceptorsType, serviceProvider);
        return proxyGenerator;
    }

    private static IInterceptor[] GetInterceptors(Type[] interceptorsType, IServiceProvider serviceProvider)
    {
        return serviceProvider
            .GetServices<IInterceptor>()
            .Where(interceptor =>
                interceptorsType.IsNullOrEmpty() || interceptorsType.Contains(interceptor.GetType()))
            .ToArray();
    }

    public static IServiceCollection AddCommonApplication(this IServiceCollection serviceCollection,
        params Assembly[] assemblies)
    {
        var allAssemblies = assemblies.WithCallingAssembly();
        return serviceCollection
            .AddValidatorsFromAssemblies(allAssemblies)
            .AddScoped<IInterceptor, EventPublisherInterceptor>()
            .AddMediatR(allAssemblies)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddSingleton(new ProxyGenerator());
    }


    private static IServiceCollection AddValidatorsFromAssemblies(this IServiceCollection serviceCollection,
        IEnumerable<Assembly> assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            serviceCollection.AddValidatorsFromAssembly(assembly);
        }

        return serviceCollection;
    }
}