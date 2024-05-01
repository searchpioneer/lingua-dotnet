using System.Runtime.CompilerServices;

namespace Lingua.Api;

internal static class DictionaryExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IncrementCounter<TKey>(this Dictionary<TKey, int> dictionary, TKey key) where TKey : notnull
	{
		dictionary.TryGetValue(key, out var count);
		dictionary[key] = count + 1;
	}
}
