namespace Lingua.Internal;

/// <summary>
/// Tokenizes text in the same way as the <c>TOKENS_WITHOUT_WHITESPACE</c> and
/// <c>TOKENS_WITH_OPTIONAL_WHITESPACE</c> regular expressions in the Rust implementation of Lingua.
/// .NET regular expressions do not support Unicode script character classes,
/// so tokens are matched against <see cref="UnicodeScript"/> data instead.
/// </summary>
internal static class Tokenizer
{
	/// <summary>
	/// Finds all tokens in the text that match the pattern
	/// <c>\p{Bengali}+|\p{Devanagari}+|\p{Gujarati}+|\p{Gurmukhi}+|\p{Han}|\p{Hangul}+|\p{Hiragana}|\p{Katakana}|\p{Tamil}+|\p{Telugu}+|\p{Thai}+|\p{L}+</c>
	/// </summary>
	/// <param name="text">The text to tokenize.</param>
	/// <returns>The matched tokens.</returns>
	public static List<string> FindTokensWithoutWhitespace(string text)
	{
		var tokens = new List<string>();
		var pos = 0;
		while (pos < text.Length)
		{
			var end = MatchToken(text, pos, includeApostropheAndHyphen: false);
			if (end == -1)
			{
				pos++;
				continue;
			}

			tokens.Add(text[pos..end]);
			pos = end;
		}

		return tokens;
	}

	/// <summary>
	/// Finds all tokens in the text that match the pattern
	/// <c>\s*(?:\p{Bengali}+|\p{Devanagari}+|\p{Gujarati}+|\p{Gurmukhi}+|\p{Han}|\p{Hangul}+|\p{Hiragana}|\p{Katakana}|\p{Tamil}+|\p{Telugu}+|\p{Thai}+|[\p{L}'-]+)[\p{N}\p{P}]*\s*</c>
	/// </summary>
	/// <param name="text">The text to tokenize.</param>
	/// <returns>The ranges of the matched tokens.</returns>
	public static List<Range> FindTokensWithOptionalWhitespace(string text)
	{
		var tokens = new List<Range>();
		var pos = 0;
		while (pos < text.Length)
		{
			var start = pos;
			var tokenStart = pos;
			while (tokenStart < text.Length && char.IsWhiteSpace(text[tokenStart]))
				tokenStart++;

			var end = tokenStart < text.Length ? MatchToken(text, tokenStart, includeApostropheAndHyphen: true) : -1;
			if (end == -1)
			{
				// no token can start within the whitespace run or at the character that follows it,
				// so continue searching after that character
				pos = tokenStart + 1;
				continue;
			}

			while (end < text.Length && (char.IsNumber(text[end]) || char.IsPunctuation(text[end])))
				end++;
			while (end < text.Length && char.IsWhiteSpace(text[end]))
				end++;

			tokens.Add(new Range(start, end));
			pos = end;
		}

		return tokens;
	}

	/// <summary>
	/// Matches a single token alternative at <paramref name="start"/>, using the same leftmost-first
	/// semantics as the regular expression alternation: characters of scripts written without
	/// spaces between words match either alone or as a run of the same script, and any other
	/// letters match as a run of letters of any script.
	/// </summary>
	/// <returns>The exclusive end index of the token, or <c>-1</c> if no alternative matches.</returns>
	private static int MatchToken(string text, int start, bool includeApostropheAndHyphen)
	{
		var ch = text[start];
		var script = ch.GetScript();

		if (script is UnicodeScript.Han or UnicodeScript.Hiragana or UnicodeScript.Katakana)
			return start + 1;

		if (script is UnicodeScript.Bengali
			or UnicodeScript.Devanagari
			or UnicodeScript.Gujarati
			or UnicodeScript.Gurmukhi
			or UnicodeScript.Hangul
			or UnicodeScript.Tamil
			or UnicodeScript.Telugu
			or UnicodeScript.Thai)
		{
			var end = start + 1;
			while (end < text.Length && text[end].GetScript() == script)
				end++;

			return end;
		}

		if (!IsLetterClass(text[start], includeApostropheAndHyphen))
			return -1;

		var letterEnd = start + 1;
		while (letterEnd < text.Length && IsLetterClass(text[letterEnd], includeApostropheAndHyphen))
			letterEnd++;

		return letterEnd;
	}

	private static bool IsLetterClass(char ch, bool includeApostropheAndHyphen) =>
		char.IsLetter(ch) || (includeApostropheAndHyphen && ch is '\'' or '-');
}
