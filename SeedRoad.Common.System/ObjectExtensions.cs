using System.ComponentModel;

namespace SeedRoad.Common.System;

public static class ObjectExtensions
{
    public static object? ToGenericTypeInstance(this object obj, Type parentType)
    {
        Type genericDispatcherType = parentType.MakeGenericType(obj.GetType());
        return Activator.CreateInstance(genericDispatcherType, obj);
    }

    public static Task<T> ToTask<T>(this T obj)
    {
        return Task.FromResult<T>(obj);
    }

    public static IDictionary<string, T> ToDictionary<T>(this object? source)
    {
        var dictionary = new Dictionary<string, T>();
        if (source is null) return dictionary;
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
        {
            AddPropertyToDictionary(property, source, dictionary);
        }

        return dictionary;
    }

    private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object? source,
        Dictionary<string, T> dictionary)
    {
        var value = property.GetValue(source) ??
                    throw new InvalidOperationException($"Cannot get value for property {property.Name}");
        if (value is T value1)
        {
            dictionary.Add(property.Name, value1);
        }
    }
}