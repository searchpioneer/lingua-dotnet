using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Lingua.Internal;
using static Lingua.Api.Language;

namespace Lingua.Api;

/// <summary>
/// Detects language of given input text, and computes confidence values for every language considered possible
/// for given input text.
/// </summary>
public sealed partial class LanguageDetector
{
    private const int HighAccuracyModeMaxTextLength = 120;

    [GeneratedRegex("\\s+")]
    private static partial Regex MultipleWhitespace();

    [GeneratedRegex("^[^\\p{L}]+$")]
    private static partial Regex NoLetter();

    [GeneratedRegex("\\p{N}")]
    private static partial Regex Numbers();

    [GeneratedRegex("\\p{P}")]
    private static partial Regex Punctuation();

    private static readonly Dictionary<string, HashSet<Language>> CharsToLanguagesMapping = new()
    {
        ["Ãã"] = [Portuguese, Vietnamese],
        ["ĄąĘę"] = [Lithuanian, Polish],
        ["Żż"] = [Polish, Romanian],
        ["Îî"] = [French, Romanian],
        ["Ññ"] = [Basque, Spanish],
        ["ŇňŤť"] = [Czech, Slovak],
        ["Ăă"] = [Romanian, Vietnamese],
        ["İıĞğ"] = [Azerbaijani, Turkish],
        ["ЈјЉљЊњ"] = [Macedonian, Serbian],
        ["ẸẹỌọ"] = [Vietnamese, Yoruba],
        ["ÐðÞþ"] = [Icelandic, Turkish],
        ["Ûû"] = [French, Hungarian],
        ["Ōō"] = [Maori, Yoruba],
        ["ĀāĒēĪī"] = [Latvian, Maori, Yoruba],
        ["Şş"] = [Azerbaijani, Romanian, Turkish],
        ["Ďď"] = [Czech, Romanian, Slovak],
        ["Ćć"] = [Bosnian, Croatian, Polish],
        ["Đđ"] = [Bosnian, Croatian, Vietnamese],
        ["Іі"] = [Belarusian, Kazakh, Ukrainian],
        ["Ìì"] = [Italian, Vietnamese, Yoruba],
        ["Øø"] = [Bokmal, Danish, Nynorsk],
        ["Ūū"] = [Latvian, Lithuanian, Maori, Yoruba],
        ["Ëë"] = [Afrikaans, Albanian, Dutch, French],
        ["ÈèÙù"] = [French, Italian, Vietnamese, Yoruba],
        ["Êê"] = [Afrikaans, French, Portuguese, Vietnamese],
        ["Õõ"] = [Estonian, Hungarian, Portuguese, Vietnamese],
        ["Ôô"] = [French, Portuguese, Slovak, Vietnamese],
        ["ЁёЫыЭэ"] = [Belarusian, Kazakh, Mongolian, Russian],
        ["ЩщЪъ"] = [Bulgarian, Kazakh, Mongolian, Russian],
        ["Òò"] = [Catalan, Italian, Vietnamese, Yoruba],
        ["Ææ"] = [Bokmal, Danish, Icelandic, Nynorsk],
        ["Åå"] = [Bokmal, Danish, Nynorsk, Swedish],
        ["Ýý"] = [Czech, Icelandic, Slovak, Turkish, Vietnamese],
        ["Ää"] = [Estonian, Finnish, German, Slovak, Swedish],
        ["Àà"] = [Catalan, French, Italian, Portuguese, Vietnamese],
        ["Ââ"] = [French, Portuguese, Romanian, Turkish, Vietnamese],
        ["Üü"] = [Azerbaijani, Catalan, Estonian, German, Hungarian, Spanish, Turkish],
        ["ČčŠšŽž"] = [Bosnian, Czech, Croatian, Latvian, Lithuanian, Slovak, Slovene],
        ["Çç"] = [Albanian, Azerbaijani, Basque, Catalan, French, Portuguese, Turkish],
        ["Öö"] = [Azerbaijani, Estonian, Finnish, German, Hungarian, Icelandic, Swedish, Turkish],
        ["Óó"] = [Catalan, Hungarian, Icelandic, Irish, Polish, Portuguese, Slovak, Spanish, Vietnamese, Yoruba],
        ["ÁáÍíÚú"] = [Catalan, Czech, Icelandic, Irish, Hungarian, Portuguese, Slovak, Spanish, Vietnamese, Yoruba],
        ["Éé"] = [Catalan, Czech, French, Hungarian, Icelandic, Irish, Italian, Portuguese, Slovak, Spanish, Vietnamese, Yoruba],
    };

    internal static readonly Dictionary<Language, Dictionary<string, float>> UnigramLanguageModels = new();
    internal static readonly Dictionary<Language, Dictionary<string, float>> BigramLanguageModels = new();
    internal static readonly Dictionary<Language, Dictionary<string, float>> TrigramLanguageModels = new();
    internal static readonly Dictionary<Language, Dictionary<string, float>> QuadrigramLanguageModels = new();
    internal static readonly Dictionary<Language, Dictionary<string, float>> FivegramLanguageModels = new();

    private readonly HashSet<Language> _languages;
    private readonly double _minimumRelativeDistance;
    private readonly bool _isLowAccuracyModeEnabled;
    private readonly Dictionary<Alphabet, Language> _oneLanguageAlphabets;
    private readonly IEnumerable<Language> _languagesWithUniqueCharacters;

    internal LanguageDetector(
        HashSet<Language> languages,
        double minimumRelativeDistance = 0,
        bool isEveryLanguageModelPreloaded = false,
        bool isLowAccuracyModeEnabled = false)
    {
        _languages = languages;
        _minimumRelativeDistance = minimumRelativeDistance;
        _isLowAccuracyModeEnabled = isLowAccuracyModeEnabled;
        _oneLanguageAlphabets = AlphabetExtensions.AllSupportingExactlyOneLanguage()
            .Where(a => languages.Contains(a.Value)).ToDictionary();
        _languagesWithUniqueCharacters = languages.Where(l => !string.IsNullOrWhiteSpace(l.UniqueCharacters()));

        if (isEveryLanguageModelPreloaded)
	        PreloadLanguageModels();
    }

    /// <summary>
    /// Gets the set of languages that can be detected.
    /// </summary>
    public IReadOnlySet<Language> Languages => _languages;

    /// <summary>
    /// Detects the language of the given input text.
    /// </summary>
    /// <param name="text">The input text to detect the language for.</param>
    /// <returns>The identified language or <see cref="Language.Unknown"/></returns>
    public Language DetectLanguageOf(string text)
    {
        var confidenceValues = ComputeLanguageConfidenceValues(text);
        if (confidenceValues.Count == 0)
            return Unknown;

        var mostLikelyLanguage = confidenceValues.First().Key;
        if (confidenceValues.Count == 1)
            return mostLikelyLanguage;

        var mostLikelyLanguageProbability = confidenceValues[mostLikelyLanguage];
        var secondMostLikelyLanguageProbability = confidenceValues.ElementAt(1).Value;

        return mostLikelyLanguageProbability.Equals(secondMostLikelyLanguageProbability)
            ? Unknown
            : mostLikelyLanguageProbability - secondMostLikelyLanguageProbability < _minimumRelativeDistance
                ? Unknown
                : mostLikelyLanguage;
    }

    /// <summary>
    /// Computes confidence values for every language considered possible for the given input text.
    /// <para />
    /// The values that this method computes are part of a **relative** confidence metric, not of an absolute one.
    /// Each value is a number between 0.0 and 1.0. The most likely language is always returned with value 1.0.
    /// All other languages get values assigned which are lower than 1.0, denoting how less likely those languages
    /// are in comparison to the most likely language.
    /// <para />
    /// The dictionary returned by this method does not necessarily contain all languages which the calling instance of
    /// <see cref="LanguageDetector"/> was built from. If the rule-based engine decides that a specific language is
    /// truly impossible, then it will not be part of the returned dictionary. Likewise, if no ngram probabilities can
    /// be found within the detector's languages for the given input text, the returned dictionary will be empty.
    /// The confidence value for each language not being part of the returned dictionary is assumed to be 0.0.
    /// </summary>
    /// <param name="text">The input text to detect the language for.</param>
    /// <returns>A dictionary of all possible languages, sorted by their confidence value in descending order.</returns>
    public IDictionary<Language, double> ComputeLanguageConfidenceValues(string text)
    {
        var values = new IndexedDictionary<Language, double>();
        var cleanedUpText = CleanUpInputText(text);

        if (cleanedUpText.Length == 0 || NoLetter().IsMatch(cleanedUpText))
            return values;

        var words = SplitTextIntoWords(cleanedUpText);
        var languageDetectedByRules = DetectLanguageWithRules(words);

        if (languageDetectedByRules != Unknown)
        {
            values[languageDetectedByRules] = 1;
            return values;
        }

        var filteredLanguages = FilterLanguagesByRules(words);
        if (filteredLanguages.Count == 1)
        {
            var filteredLanguage = filteredLanguages.Single();
            values[filteredLanguage] = 1.0;
            return values;
        }

        if (_isLowAccuracyModeEnabled && cleanedUpText.Length < 3)
	        return values;

        var ngramSizeRange = cleanedUpText.Length >= HighAccuracyModeMaxTextLength || _isLowAccuracyModeEnabled
            ? Enumerable.Range(3, 1)
            : Enumerable.Range(1, 5);

        var allProbabilitiesAndUnigramCounts =
            new ConcurrentBag<KeyValuePair<Dictionary<Language, float>, Dictionary<Language, int>?>>();

        Parallel.ForEach(ngramSizeRange.Where(r => cleanedUpText.Length >= r), i =>
        {
            var testDataModel = TestDataLanguageModel.FromText(cleanedUpText, i);
            var probabilities = ComputeLanguageProbabilities(testDataModel, filteredLanguages);

            Dictionary<Language, int>? unigramCounts = null;
            if (i == 1)
            {
                var unigramFilteredLanguages = _languages.Count > 0
                    ? filteredLanguages.Where(f => _languages.Contains(f)).ToHashSet()
                    : filteredLanguages;
                unigramCounts = CountUnigramsOfInputText(testDataModel, unigramFilteredLanguages);
            }
            allProbabilitiesAndUnigramCounts.Add(KeyValuePair.Create(probabilities, unigramCounts));
        });

        var allProbabilities = allProbabilitiesAndUnigramCounts
            .Select(probabilities => probabilities.Key)
            .ToList();

        var unigramCounts = allProbabilitiesAndUnigramCounts.First().Value ?? new Dictionary<Language, int>();
        var summedUpProbabilities = SumUpProbabilities(allProbabilities, unigramCounts, filteredLanguages);

        if (summedUpProbabilities.Count == 0)
	        return new Dictionary<Language, double>();

        var highestProbability = summedUpProbabilities.Max(p => p.Value);
        var confidenceValues = summedUpProbabilities.ToDictionary(
            p => p.Key,
            p => (double)highestProbability / p.Value);

        return confidenceValues
            .OrderByDescending(c => c.Value)
            .ThenBy(c => c.Key)
            .ToIndexedDictionary();
    }

    /// <summary>
    /// Unloads all language models loaded by this <see cref="LanguageDetector"/> instance
    /// and frees associated resources.
    /// <para />
    /// This will be useful if the library is used within a web application inside
    /// an application server. By calling this method prior to undeploying the
    /// web application, the language models are removed and memory is freed.
    /// </summary>
    public void UnloadLanguageModels()
    {
	    lock (TrigramLanguageModels)
	    {
		    foreach (var language in _languages)
			    TrigramLanguageModels.Remove(language);
	    }

	    if (!_isLowAccuracyModeEnabled)
	    {
		    lock (UnigramLanguageModels)
		    {
			    foreach (var language in _languages)
				    UnigramLanguageModels.Remove(language);
		    }

		    lock (BigramLanguageModels)
		    {
			    foreach (var language in _languages)
				    BigramLanguageModels.Remove(language);
		    }

		    lock (QuadrigramLanguageModels)
		    {
			    foreach (var language in _languages)
				    QuadrigramLanguageModels.Remove(language);
		    }

		    lock (FivegramLanguageModels)
		    {
			    foreach (var language in _languages)
				    FivegramLanguageModels.Remove(language);
		    }
	    }
    }

    private static Dictionary<Language, float> SumUpProbabilities(
        List<Dictionary<Language,float>> probabilities,
        Dictionary<Language,int> unigramCountsOfInputText,
        HashSet<Language> filteredLanguages)
    {
        var summedUpProbabilities = new Dictionary<Language, float>();
        foreach (var language in filteredLanguages)
        {
            var sum = 0f;
            foreach (var probability in probabilities)
            {
                probability.TryGetValue(language, out var value);
                sum += value;
            }

            summedUpProbabilities[language] = sum;
            if (unigramCountsOfInputText.TryGetValue(language, out var count))
	            summedUpProbabilities[language] /= count;
        }

        return summedUpProbabilities.Where(p => p.Value != 0).ToDictionary();
    }

    private static Dictionary<Language,int> CountUnigramsOfInputText(TestDataLanguageModel unigramLanguageModel, HashSet<Language> filteredLanguages)
    {
        var unigramCounts = new Dictionary<Language, int>();
        foreach (var language in filteredLanguages)
        {
            foreach (var unigram in unigramLanguageModel.Ngrams)
            {
                var probability = LookupNgramProbability(language, unigram);
                if (probability > 0)
	                unigramCounts.IncrementCounter(language);
            }
        }

        return unigramCounts;
    }

    internal static Dictionary<Language, float> ComputeLanguageProbabilities(TestDataLanguageModel testDataModel, IReadOnlySet<Language> filteredLanguages)
    {
        var probabilities = new Dictionary<Language, float>();
        foreach (var language in filteredLanguages)
	        probabilities[language] = ComputeSumOfNgramProbabilities(language, testDataModel.Ngrams);

        return probabilities.Where(p => p.Value < 0).ToDictionary();
    }

    internal static float ComputeSumOfNgramProbabilities(Language language, HashSet<Ngram> ngrams)
    {
        var probabilitiesSum = 0d;
        foreach (var ngram in ngrams)
        {
            foreach (var elem in ngram.RangeOfLowerOrderNGrams())
            {
                var probability = LookupNgramProbability(language, elem);
                if (probability > 0)
                {
                    probabilitiesSum += Math.Log(probability);
                    break;
                }
            }
        }

        return (float)probabilitiesSum;
    }

    internal static float LookupNgramProbability(Language language, Ngram ngram)
    {
        var ngramLength = ngram.ToString().Length;
        var languageModels = ngramLength switch
        {
            5 => FivegramLanguageModels,
            4 => QuadrigramLanguageModels,
            3 => TrigramLanguageModels,
            2 => BigramLanguageModels,
            1 => UnigramLanguageModels,
            0 => throw new ArgumentException("Zerogram detected"),
            _ => throw new ArgumentException($"unsupported ngram length detected: ${ngramLength}")
        };

        var model = LoadLanguageModels(languageModels, language, ngramLength);
        return model.GetValueOrDefault(ngram.ToString(), 0);
    }

    private void PreloadLanguageModels()
    {
	    var range = _isLowAccuracyModeEnabled
		    ? Enumerable.Range(3, 1)
		    : Enumerable.Range(1, 5);

	    var languagesAndRange =
		    _languages.SelectMany(language => range.Select(ngramLength => (language, ngramLength)));

	    Parallel.ForEach(languagesAndRange, li =>
	    {
		    var (language, ngramLength) = li;
		    switch (ngramLength)
		    {
			    case 1:
				    LoadLanguageModels(UnigramLanguageModels, language, ngramLength);
				    break;
			    case 2:
				    LoadLanguageModels(BigramLanguageModels, language, ngramLength);
				    break;
			    case 3:
				    LoadLanguageModels(TrigramLanguageModels, language, ngramLength);
				    break;
			    case 4:
				    LoadLanguageModels(QuadrigramLanguageModels, language, ngramLength);
				    break;
			    case 5:
				    LoadLanguageModels(FivegramLanguageModels, language, ngramLength);
				    break;
		    }
	    });
    }

    private static Dictionary<string, float> LoadLanguageModels(Dictionary<Language, Dictionary<string, float>> languageModels, Language language, int ngramLength)
    {
        lock (languageModels)
        {
            if (languageModels.TryGetValue(language, out var value))
	            return value;
        }

        var model = LoadLanguageModel(language, ngramLength);
        lock (languageModels)
        {
            languageModels.TryAdd(language, model);
            return model;
        }
    }

    private static Dictionary<string,float> LoadLanguageModel(Language language, int ngramLength)
    {
        var file = $"Lingua.LanguageModels.{language.IsoCode6391().ToString().ToLowerInvariant()}.{Ngram.GetNgramNameByLength(ngramLength)}s.json";
        using var stream = typeof(LanguageDetector).Assembly.GetManifestResourceStream(file);
        return stream == null
            ? new Dictionary<string, float>()
            : TrainingDataLanguageModel.FromJson(stream);
    }

    internal HashSet<Language> FilterLanguagesByRules(List<string> words)
    {
        var detectedAlphabets = new Dictionary<Alphabet, int>();
        foreach (var word in words)
        {
            foreach (var alphabet in AlphabetExtensions.Values)
            {
                if (alphabet.Matches(word))
                {
                    detectedAlphabets.IncrementCounter(alphabet);
                    break;
                }
            }
        }

        switch (detectedAlphabets.Count)
        {
            case 0:
                return _languages;
            case > 1:
            {
                var distinctAlphabets = new HashSet<int>();
                foreach (var count in detectedAlphabets.Values)
	                distinctAlphabets.Add(count);

                if (distinctAlphabets.Count == 1)
	                return _languages;

                break;
            }
        }

        var mostFrequentAlphabet = detectedAlphabets.MaxBy(a => a.Value).Key;
        var filteredLanguages = _languages
            .Where(l => l.Alphabets().Contains(mostFrequentAlphabet))
            .ToHashSet();
        var languageCounts = new Dictionary<Language, int>();

        foreach (var (characters, languages) in CharsToLanguagesMapping)
        {
            var relevantLanguages = languages.Intersect(filteredLanguages).ToList();
            foreach (var word in words)
            {
                foreach (var ch in characters)
                {
                    if (word.Contains(ch))
                    {
                        foreach (var language in relevantLanguages)
	                        languageCounts.IncrementCounter(language);
                    }
                }
            }
        }

        var languagesSubset = languageCounts
            .Where(l => l.Value >= words.Count / 2d)
            .Select(l => l.Key)
            .ToHashSet();

        return languagesSubset.Count == 0 ? filteredLanguages : languagesSubset;
    }

    internal Language DetectLanguageWithRules(List<string> words)
    {
        var totalLanguageCounts = new Dictionary<Language, int>();

        foreach (var word in words)
        {
            var wordLanguageCounts = new Dictionary<Language, int>();

            foreach (var ch in word)
            {
                var isMatch = false;
                foreach (var (alphabet, language) in _oneLanguageAlphabets)
                {
                    if (alphabet.Matches(ch))
                    {
                        wordLanguageCounts.IncrementCounter(language);
                        isMatch = true;
                        break;
                    }
                }

                if (!isMatch)
                {
                    if (Alphabet.Han.Matches(ch))
	                    wordLanguageCounts.IncrementCounter(Chinese);
                    else if (IsJapaneseAlphabet(ch))
	                    wordLanguageCounts.IncrementCounter(Japanese);
                    else if (Alphabet.Latin.Matches(ch) || Alphabet.Cyrillic.Matches(ch) || Alphabet.Devanagari.Matches(ch))
                    {
                        foreach (var language in _languagesWithUniqueCharacters.Where(l => l.UniqueCharacters()?.Contains(ch) ?? false))
	                        wordLanguageCounts.IncrementCounter(language);
                    }
                }
            }

            switch (wordLanguageCounts.Count)
            {
                case 0:
                    totalLanguageCounts.IncrementCounter(Unknown);
                    break;
                case 1:
                {
                    var language = wordLanguageCounts.Single().Key;
                    totalLanguageCounts.IncrementCounter(_languages.Contains(language) ? language : Unknown);
                    break;
                }
                default:
                {
                    using var sortedWordLanguageCounts = wordLanguageCounts
                        .OrderByDescending(a => a.Value)
                        .GetEnumerator();

                    sortedWordLanguageCounts.MoveNext();
                    var (mostFrequentLanguage, firstCharCount) = sortedWordLanguageCounts.Current;
                    sortedWordLanguageCounts.MoveNext();
                    var (_, secondCharCount) = sortedWordLanguageCounts.Current;

                    if (firstCharCount > secondCharCount && _languages.Contains(mostFrequentLanguage))
	                    totalLanguageCounts.IncrementCounter(mostFrequentLanguage);
                    else
	                    totalLanguageCounts.IncrementCounter(Unknown);

                    break;
                }
            }
        }

        totalLanguageCounts.TryGetValue(Unknown, out var unknownLanguageCount);
        if (unknownLanguageCount < 0.5 * words.Count)
	        totalLanguageCounts.Remove(Unknown);

        switch (totalLanguageCounts.Count)
        {
            case 0:
                return Unknown;
            case 1:
                return totalLanguageCounts.Keys.Single();
            case 2 when
                totalLanguageCounts.ContainsKey(Chinese) &&
                totalLanguageCounts.ContainsKey(Japanese):
                return Japanese;
        }

        using var sortedTotalLanguageCounts = totalLanguageCounts
            .OrderByDescending(l => l.Value)
            .GetEnumerator();

        sortedTotalLanguageCounts.MoveNext();
        var (mostFrequentTotalLanguage, firstTotalCharCount) = sortedTotalLanguageCounts.Current;
        sortedTotalLanguageCounts.MoveNext();
        var (_, secondTotalCharCount) = sortedTotalLanguageCounts.Current;

        return firstTotalCharCount == secondTotalCharCount ? Unknown : mostFrequentTotalLanguage;
    }

    private static bool IsJapaneseAlphabet(char ch) =>
        ch.GetScript() is UnicodeScript.Hiragana or UnicodeScript.Katakana or UnicodeScript.Han;

    internal static List<string> SplitTextIntoWords(string text)
    {
        var words = new List<string>();
        var nextWordStart = 0;
        for (var i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            if (ch == ' ')
            {
                if (nextWordStart != i)
	                words.Add(text.Substring(nextWordStart, i - nextWordStart));

                nextWordStart = i + 1;
            }
            else if (ch.IsLogogram())
            {
                if (nextWordStart != i)
	                words.Add(text.Substring(nextWordStart, i - nextWordStart));

                words.Add(text[i].ToString());
                nextWordStart = i + 1;
            }
        }

        if (nextWordStart != text.Length)
	        words.Add(text.Substring(nextWordStart));

        return words;
    }

    internal static string CleanUpInputText(string text) =>
        MultipleWhitespace().Replace(
            Numbers().Replace(
                Punctuation().Replace(text.Trim().ToLowerInvariant(), ""), ""), " ");

    private bool Equals(LanguageDetector other) =>
        _languages.SetEquals(other._languages)
        && _minimumRelativeDistance.Equals(other._minimumRelativeDistance)
        && _isLowAccuracyModeEnabled == other._isLowAccuracyModeEnabled;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LanguageDetector)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var language in _languages.Order())
            hashCode.Add(language);
        hashCode.Add(_minimumRelativeDistance);
        hashCode.Add(_isLowAccuracyModeEnabled);
        return hashCode.ToHashCode();
    }
}
