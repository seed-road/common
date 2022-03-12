namespace SeedRoad.Common.System;

public static class DictionaryExtensions
{
    public static IDictionary<TKey, TValue> Copy<TKey, TValue>
        (this IDictionary<TKey, TValue> original)
    {
        var ret = new Dictionary<TKey, TValue>(original.Count);
        foreach ((TKey key, TValue value) in original)
        {
            ret.Add(key, value);
        }

        return ret;
    }
}