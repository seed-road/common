using System.Reflection;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Configuration;
using SeedRoad.Common.Core.Application.Events;
using SeedRoad.Common.Core.Application.ExceptionsHandling;
using SeedRoad.Common.Core.Application.Pagination;
using SeedRoad.Common.Core.Application.Validation;
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

    public static IServiceCollection AddProxyScoped<TInterface, TImplementation>(
        this IServiceCollection serviceCollection, Type interceptorType)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var factory = ProxyFactory<TInterface, TImplementation>(interceptorType);
        return serviceCollection.AddScoped(typeof(TInterface), factory);
    }


    private static ProxyGenerator PrepareProxyFactory<TImplementation>(Type[] interceptorsType,
        IServiceProvider serviceProvider, out TImplementation actual, out IAsyncInterceptor[] interceptors)
        where TImplementation : class
    {
        var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
        actual = ActivatorUtilities.CreateInstance<TImplementation>(serviceProvider);
        interceptors = GetInterceptors(interceptorsType, serviceProvider);
        return proxyGenerator;
    }

    private static IAsyncInterceptor[] GetInterceptors(Type[] interceptorsType, IServiceProvider serviceProvider)
    {
        return serviceProvider
            .GetServices<IAsyncInterceptor>()
            .Where(interceptor =>
                interceptorsType.IsNullOrEmpty() || interceptorsType.Contains(interceptor.GetType()))
            .ToArray();
    }

    public static IServiceCollection AddCommonApplication(this IServiceCollection serviceCollection,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var allAssemblies = assemblies.WithCallingAssembly();
        return serviceCollection
            .AddValidatorsFromAssemblies(allAssemblies)
            .AddScoped<IAsyncInterceptor, EventPublisherInterceptor>()
            .AddMediatR(allAssemblies)
            .AddPagination(configuration)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(PaginationBehavior<,>))
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

    private static IServiceCollection AddPagination(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        PaginationConfiguration paginationConfiguration =
            configuration.GetConfiguration<PaginationConfiguration>() ?? new PaginationConfiguration();
        return serviceCollection.AddSingleton(paginationConfiguration);
    }
}