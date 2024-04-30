using Lingua.Api;

namespace Lingua;

public enum Alphabet
{
    Arabic,
    Armenian,
    Bengali,
    Cyrillic,
    Devanagari,
    Ethiopic,
    Georgian,
    Greek,
    Gujarati,
    Gurmukhi,
    Han,
    Hangul,
    Hebrew,
    Hiragana,
    Katakana,
    Latin,
    Sinhala,
    Tamil,
    Telugu,
    Thai,
    None,
}

public static class AlphabetExtensions
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
    /// Whether the the alphabet script matches character script.
    /// </summary>
    /// <param name="alphabet">The alphabet to check</param>
    /// <param name="ch">The character to check</param>
    /// <returns><c>true</c> if the scripts match, false otherwise</returns>
    public static bool Matches(this Alphabet alphabet, char ch) => ch.GetScript() == alphabet.Script();

    /// <summary>
    /// Whether the alphabet script matches the given script.
    /// </summary>
    /// <param name="alphabet">The alphabet to check</param>
    /// <param name="script">The script to check</param>
    /// <returns><c>true</c> if the scripts match, false otherwise</returns>
    public static bool Matches(this Alphabet alphabet, UnicodeScript script) => script == alphabet.Script();

    /// <summary>
    /// Whether the alphabet script matches script of all characters in the text.
    /// </summary>
    /// <param name="alphabet">The alphabet to check</param>
    /// <param name="text">The text to check</param>
    /// <returns><c>true</c> if the scripts match for all characters, false otherwise</returns>
    public static bool Matches(this Alphabet alphabet, string text)
    {
        var unicodeScript = alphabet.Script();
        return text.All(ch => ch.GetScript() == unicodeScript);
    }

    private static HashSet<Language> SupportedLanguages(this Alphabet alphabet)
    {
        var languages = new HashSet<Language>();
        foreach (var language in Enum.GetValues<Language>())
        {
            if (language.Alphabets().Contains(alphabet))
	            languages.Add(language);
        }

        return languages;
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
	            alphabets[alphabet] = supportedLanguages.Single();
        }

        return alphabets;
    }
}
