using Lingua.Internal;

namespace Lingua.Api;

public static class EnumerableExtensions
{
    public static IndexedDictionary<TKey, TValue> ToIndexedDictionary<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> enumerable) where TKey : notnull
    {
        var dictionary = new IndexedDictionary<TKey, TValue>();
        foreach (var keyValuePair in enumerable)
        {
            dictionary.Add(keyValuePair);
        }

        return dictionary;
    }
}