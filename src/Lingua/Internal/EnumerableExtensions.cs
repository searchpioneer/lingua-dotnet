namespace Lingua.Internal;

internal static class EnumerableExtensions
{
	/// <summary>
	/// Collects the key value pairs into a dictionary that preserves insertion order when enumerated.
	/// </summary>
	/// <remarks>
	/// On target frameworks that predate .NET 9 the <see cref="OrderedDictionary{TKey,TValue}"/>
	/// constructed here is the adapted copy of the runtime implementation in this namespace,
	/// rather than the built-in type.
	/// </remarks>
	public static IDictionary<TKey, TValue> ToOrderedDictionary<TKey, TValue>(
		this IEnumerable<KeyValuePair<TKey, TValue>> enumerable) where TKey : notnull
	{
		var dictionary = new OrderedDictionary<TKey, TValue>();
		foreach (var keyValuePair in enumerable)
			dictionary.Add(keyValuePair.Key, keyValuePair.Value);

		return dictionary;
	}
}
