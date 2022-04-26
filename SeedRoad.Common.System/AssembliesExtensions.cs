using System.Diagnostics;
using System.Reflection;

namespace SeedRoad.Common.System;

public static class AssembliesExtensions
{
    public static Assembly[] WithExecutingAssembly(this Assembly[] assemblies)
    {
        var assembliesSet = assemblies.ToHashSet();
        assembliesSet.Add(Assembly.GetCallingAssembly());
        return assembliesSet.ToArray();
    }

    public static Assembly[] WithCallingAssembly(this Assembly[] assemblies)
    {
        var stackTrace = new StackTrace();
        var assembliesSet = assemblies.ToHashSet();
        Assembly? callingAssembly = GetAssembly(stackTrace.FrameCount - 2);
        if (callingAssembly is not null)
        {
            assembliesSet.Add(callingAssembly);
        }

        return assembliesSet.ToArray();
    }

    public static Type[] GetAllTypes(this IEnumerable<Assembly> assemblies)
    {
        return assemblies.SelectMany(assembly => assembly.GetTypes()).ToArray();
    }


    private static Assembly? GetAssembly(int stackTraceLevel)
    {
        var stackTrace = new StackTrace();
        Type? declaringType = stackTrace.GetFrame(stackTraceLevel)?.GetMethod()?.DeclaringType;
        return declaringType?.Assembly;
    }
}