using Lingua.Internal;

namespace Lingua.Api;

internal static class CharExtensions
{
	private static readonly HashSet<Alphabet> AlphabetsWithLogograms = LanguageInfo.LanguagesSupportingLogograms
		.SelectMany(l => l.Alphabets())
		.ToHashSet();

	public static bool IsLogogram(this char ch)
	{
		if (char.IsWhiteSpace(ch))
			return false;

		var charScript = ch.GetScript();
		return AlphabetsWithLogograms.Any(a => a.Matches(charScript));
	}
}
