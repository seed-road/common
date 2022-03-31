namespace SeedRoad.Common.System;

public static class EnumerableExtensions
{
    public static IEnumerable<T> ToNotNullEnumerable<T>(this IEnumerable<T?> enumerable)
    {
        return enumerable.Where(item => item is not null) as IEnumerable<T>;
    } 
}