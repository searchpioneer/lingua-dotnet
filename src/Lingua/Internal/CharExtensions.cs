namespace Lingua.Internal;

internal static class CharExtensions
{
	private static readonly UnicodeScript[] ScriptsWithLogograms = LanguageInfo.LanguagesSupportingLogograms
		.SelectMany(l => l.Alphabets())
		.Select(a => a.Script())
		.OfType<UnicodeScript>()
		.Distinct()
		.ToArray();

	public static bool IsLogogram(this char ch)
	{
		if (char.IsWhiteSpace(ch))
			return false;

		var charScript = ch.GetScript();
		foreach (var script in ScriptsWithLogograms)
		{
			if (script == charScript)
				return true;
		}

		return false;
	}
}
