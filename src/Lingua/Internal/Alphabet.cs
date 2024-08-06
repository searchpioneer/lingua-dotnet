namespace Lingua.Internal;

internal enum Alphabet
{
	/// <summary>The 'Arabic' alphabet</summary>
	Arabic,
	/// <summary>The 'Armenian' alphabet</summary>
	Armenian,
	/// <summary>The 'Bengali' alphabet</summary>
	Bengali,
	/// <summary>The 'Cyrillic' alphabet</summary>
	Cyrillic,
	/// <summary>The 'Devanagari' alphabet</summary>
	Devanagari,
	/// <summary>The 'Ethiopic' alphabet</summary>
	Ethiopic,
	/// <summary>The 'Georgian' alphabet</summary>
	Georgian,
	/// <summary>The 'Greek' alphabet</summary>
	Greek,
	/// <summary>The 'Gujarati' alphabet</summary>
	Gujarati,
	/// <summary>The 'Gurmukhi' alphabet</summary>
	Gurmukhi,
	/// <summary>The 'Han' alphabet</summary>
	Han,
	/// <summary>The 'Hangul' alphabet</summary>
	Hangul,
	/// <summary>The 'Hebrew' alphabet</summary>
	Hebrew,
	/// <summary>The 'Hiragana' alphabet</summary>
	Hiragana,
	/// <summary>The 'Katakana' alphabet</summary>
	Katakana,
	/// <summary>The 'Latin' alphabet</summary>
	Latin,
	/// <summary>The 'Sinhala' alphabet</summary>
	Sinhala,
	/// <summary>The 'Tamil' alphabet</summary>
	Tamil,
	/// <summary>The 'Telugu' alphabet</summary>
	Telugu,
	/// <summary>The 'Thai' alphabet</summary>
	Thai,
	/// <summary>The imaginary 'None' alphabet</summary>
	None,
}

internal static class AlphabetExtensions
{
	internal static readonly Alphabet[] Values = Enum.GetValues<Alphabet>();

	/// <summary>
	/// Gets the Unicode script for the alphabet
	/// </summary>
	/// <param name="alphabet">The alphabet</param>
	/// <returns>The <see cref="UnicodeScript"/> if there is one, null otherwise</returns>
	/// <exception cref="InvalidOperationException">If the value is not a valid alphabet</exception>
	public static UnicodeScript? Script(this Alphabet alphabet) =>
		alphabet switch
		{
			Alphabet.Arabic => UnicodeScript.Arabic,
			Alphabet.Armenian => UnicodeScript.Armenian,
			Alphabet.Bengali => UnicodeScript.Bengali,
			Alphabet.Cyrillic => UnicodeScript.Cyrillic,
			Alphabet.Devanagari => UnicodeScript.Devanagari,
			Alphabet.Ethiopic => UnicodeScript.Ethiopic,
			Alphabet.Georgian => UnicodeScript.Georgian,
			Alphabet.Greek => UnicodeScript.Greek,
			Alphabet.Gujarati => UnicodeScript.Gujarati,
			Alphabet.Gurmukhi => UnicodeScript.Gurmukhi,
			Alphabet.Han => UnicodeScript.Han,
			Alphabet.Hangul => UnicodeScript.Hangul,
			Alphabet.Hebrew => UnicodeScript.Hebrew,
			Alphabet.Hiragana => UnicodeScript.Hiragana,
			Alphabet.Katakana => UnicodeScript.Katakana,
			Alphabet.Latin => UnicodeScript.Latin,
			Alphabet.Sinhala => UnicodeScript.Sinhala,
			Alphabet.Tamil => UnicodeScript.Tamil,
			Alphabet.Telugu => UnicodeScript.Telugu,
			Alphabet.Thai => UnicodeScript.Thai,
			Alphabet.None => null,
			_ => throw new InvalidOperationException($"{alphabet} is not a valid value")
		};

	/// <summary>
	/// Whether the alphabet script matches character script.
	/// </summary>
	/// <param name="alphabet">The alphabet to check</param>
	/// <param name="ch">The character to check</param>
	/// <returns><c>true</c> if the scripts match, <c>false</c> otherwise</returns>
	public static bool Matches(this Alphabet alphabet, char ch) => ch.GetScript() == alphabet.Script();

	/// <summary>
	/// Whether the alphabet script matches the given script.
	/// </summary>
	/// <param name="alphabet">The alphabet to check</param>
	/// <param name="script">The script to check</param>
	/// <returns><c>true</c> if the scripts match, <c>false</c> otherwise</returns>
	public static bool Matches(this Alphabet alphabet, UnicodeScript script) => script == alphabet.Script();

	/// <summary>
	/// Whether the alphabet script matches script of all characters in the text.
	/// </summary>
	/// <param name="alphabet">The alphabet to check</param>
	/// <param name="text">The text to check</param>
	/// <returns><c>true</c> if the scripts match for all characters, <c>false</c> otherwise</returns>
	public static bool Matches(this Alphabet alphabet, string text)
	{
		var unicodeScript = alphabet.Script();
		return text.All(ch => ch.GetScript() == unicodeScript);
	}

	/// <summary>
	/// Gets the alphabets that support exactly one language.
	/// </summary>
	/// <returns>
	/// A new instance of a <see cref="IReadOnlyDictionary{TKey,TValue}"/> keyed by <see cref="Alphabet"/>,
	/// with <see cref="Language"/> values.
	/// </returns>
	public static IReadOnlyDictionary<Alphabet, Language> AllSupportingExactlyOneLanguage()
	{
		var alphabets = new Dictionary<Alphabet, Language>();
		foreach (var alphabet in Values)
		{
			if (alphabet == Alphabet.None)
				continue;

			var supportedLanguages = alphabet.SupportedLanguages();
			if (supportedLanguages.Count == 1)
				alphabets[alphabet] = supportedLanguages.First();
		}

		return alphabets;
	}

	private static HashSet<Language> SupportedLanguages(this Alphabet alphabet)
	{
		var languages = LanguageInfo.All();
		languages.RemoveWhere(language => !language.Alphabets().Contains(alphabet));
		return languages;
	}
}
