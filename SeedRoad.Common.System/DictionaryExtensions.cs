namespace SeedRoad.Common.System;

public static class DictionaryExtensions
{
    public static IDictionary<TKey, TValue> Copy<TKey, TValue>
        (this IDictionary<TKey, TValue> original) where TKey : notnull
    {
        var ret = new Dictionary<TKey, TValue>(original.Count);
        foreach (var (key, value) in original)
        {
            ret.Add(key, value);
        }

        return ret;
    }
}