using Lingua.Api;

namespace Lingua;

public enum Alphabet
{
    ARABIC,
    ARMENIAN,
    BENGALI,
    CYRILLIC,
    DEVANAGARI,
    ETHIOPIC,
    GEORGIAN,
    GREEK,
    GUJARATI,
    GURMUKHI,
    HAN,
    HANGUL,
    HEBREW,
    HIRAGANA,
    KATAKANA,
    LATIN,
    SINHALA,
    TAMIL,
    TELUGU,
    THAI,
    NONE,
}

public static class AlphabetExtensions
{
    internal static readonly Alphabet[] Values = Enum.GetValues<Alphabet>();
    
    public static UnicodeScript? Script(this Alphabet alphabet) =>
        alphabet switch
        {
            Alphabet.ARABIC => UnicodeScript.Arabic,
            Alphabet.ARMENIAN => UnicodeScript.Armenian,
            Alphabet.BENGALI => UnicodeScript.Bengali,
            Alphabet.CYRILLIC => UnicodeScript.Cyrillic,
            Alphabet.DEVANAGARI => UnicodeScript.Devanagari,
            Alphabet.ETHIOPIC => UnicodeScript.Ethiopic,
            Alphabet.GEORGIAN => UnicodeScript.Georgian,
            Alphabet.GREEK => UnicodeScript.Greek,
            Alphabet.GUJARATI => UnicodeScript.Gujarati,
            Alphabet.GURMUKHI => UnicodeScript.Gurmukhi,
            Alphabet.HAN => UnicodeScript.Han,
            Alphabet.HANGUL => UnicodeScript.Hangul,
            Alphabet.HEBREW => UnicodeScript.Hebrew,
            Alphabet.HIRAGANA => UnicodeScript.Hiragana,
            Alphabet.KATAKANA => UnicodeScript.Katakana,
            Alphabet.LATIN => UnicodeScript.Latin,
            Alphabet.SINHALA => UnicodeScript.Sinhala,
            Alphabet.TAMIL => UnicodeScript.Tamil,
            Alphabet.TELUGU => UnicodeScript.Telugu,
            Alphabet.THAI => UnicodeScript.Thai,
            Alphabet.NONE => null,
            _ => throw new ArgumentOutOfRangeException(nameof(alphabet), alphabet, null)
        };

    public static bool Matches(this Alphabet alphabet, char ch) => ch.GetScript() == alphabet.Script();

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
            {
                languages.Add(language);
            }
        }
        
        return languages;
    }

    public static Dictionary<Alphabet, Language> AllSupportingExactlyOneLanguage()
    {
        var alphabets = new Dictionary<Alphabet, Language>();
        foreach (var alphabet in Enum.GetValues<Alphabet>())
        {
            if (alphabet == Alphabet.NONE)
                continue;

            var supportedLanguages = alphabet.SupportedLanguages();
            if (supportedLanguages.Count == 1)
            {
                alphabets[alphabet] = supportedLanguages.Single();
            }
        }
        
        return alphabets;
    }
}