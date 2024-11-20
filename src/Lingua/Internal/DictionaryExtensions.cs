using System.Runtime.CompilerServices;

namespace Lingua.Internal;

internal static class DictionaryExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IncrementCounter<TKey>(this Dictionary<TKey, int> dictionary, TKey key, int increment = 1) where TKey : notnull =>
		dictionary[key] = dictionary.TryGetValue(key, out var count) ? count + increment : increment;
}
