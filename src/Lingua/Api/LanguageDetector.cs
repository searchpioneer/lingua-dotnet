using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Lingua.Internal;

namespace Lingua.Api;

public class LanguageDetector
{
    private const int HighAccuracyModeMaxTextLength = 120;
    private static readonly Regex MultipleWhitespace = new("\\s+", RegexOptions.Compiled);
    private static readonly Regex NoLetter = new("^[^\\p{L}]+$", RegexOptions.Compiled);
    private static readonly Regex Numbers = new("\\p{N}", RegexOptions.Compiled);
    private static readonly Regex Punctuation = new("\\p{P}", RegexOptions.Compiled);
    private static readonly Dictionary<string, HashSet<Language>> CharsToLanguagesMapping = new()
    {
        ["Ãã"] = [Language.Portuguese, Language.Vietnamese],
        ["ĄąĘę"] = [Language.Lithuanian, Language.Polish],
        ["Żż"] = [Language.Polish, Language.Romanian],
        ["Îî"] = [Language.French, Language.Romanian],
        ["Ññ"] = [Language.Basque, Language.Spanish],
        ["ŇňŤť"] = [Language.Czech, Language.Slovak],
        ["Ăă"] = [Language.Romanian, Language.Vietnamese],
        ["İıĞğ"] = [Language.Azerbaijani, Language.Turkish],
        ["ЈјЉљЊњ"] = [Language.Macedonian, Language.Serbian],
        ["ẸẹỌọ"] = [Language.Vietnamese, Language.Yoruba],
        ["ÐðÞþ"] = [Language.Icelandic, Language.Turkish],
        ["Ûû"] = [Language.French, Language.Hungarian],
        ["Ōō"] = [Language.Maori, Language.Yoruba],
        ["ĀāĒēĪī"] = [Language.Latvian, Language.Maori, Language.Yoruba],
        ["Şş"] = [Language.Azerbaijani, Language.Romanian, Language.Turkish],
        ["Ďď"] = [Language.Czech, Language.Romanian, Language.Slovak],
        ["Ćć"] = [Language.Bosnian, Language.Croatian, Language.Polish],
        ["Đđ"] = [Language.Bosnian, Language.Croatian, Language.Vietnamese],
        ["Іі"] = [Language.Belarusian, Language.Kazakh, Language.Ukrainian],
        ["Ìì"] = [Language.Italian, Language.Vietnamese, Language.Yoruba],
        ["Øø"] = [Language.Bokmal, Language.Danish, Language.Nynorsk],
        ["Ūū"] = [Language.Latvian, Language.Lithuanian, Language.Maori, Language.Yoruba],
        ["Ëë"] = [Language.Afrikaans, Language.Albanian, Language.Dutch, Language.French],
        ["ÈèÙù"] = [Language.French, Language.Italian, Language.Vietnamese, Language.Yoruba],
        ["Êê"] = [Language.Afrikaans, Language.French, Language.Portuguese, Language.Vietnamese],
        ["Õõ"] = [Language.Estonian, Language.Hungarian, Language.Portuguese, Language.Vietnamese],
        ["Ôô"] = [Language.French, Language.Portuguese, Language.Slovak, Language.Vietnamese],
        ["ЁёЫыЭэ"] = [Language.Belarusian, Language.Kazakh, Language.Mongolian, Language.Russian],
        ["ЩщЪъ"] = [Language.Bulgarian, Language.Kazakh, Language.Mongolian, Language.Russian],
        ["Òò"] = [Language.Catalan, Language.Italian, Language.Vietnamese, Language.Yoruba],
        ["Ææ"] = [Language.Bokmal, Language.Danish, Language.Icelandic, Language.Nynorsk],
        ["Åå"] = [Language.Bokmal, Language.Danish, Language.Nynorsk, Language.Swedish],
        ["Ýý"] = [Language.Czech, Language.Icelandic, Language.Slovak, Language.Turkish, Language.Vietnamese],
        ["Ää"] = [Language.Estonian, Language.Finnish, Language.German, Language.Slovak, Language.Swedish],
        ["Àà"] = [Language.Catalan, Language.French, Language.Italian, Language.Portuguese, Language.Vietnamese],
        ["Ââ"] = [Language.French, Language.Portuguese, Language.Romanian, Language.Turkish, Language.Vietnamese],
        ["Üü"] = [Language.Azerbaijani, Language.Catalan, Language.Estonian, Language.German, Language.Hungarian, Language.Spanish, Language.Turkish],
        ["ČčŠšŽž"] = [Language.Bosnian, Language.Czech, Language.Croatian, Language.Latvian, Language.Lithuanian, Language.Slovak, Language.Slovene],
        ["Çç"] = [Language.Albanian, Language.Azerbaijani, Language.Basque, Language.Catalan, Language.French, Language.Portuguese, Language.Turkish],
        ["Öö"] = [Language.Azerbaijani, Language.Estonian, Language.Finnish, Language.German, Language.Hungarian, Language.Icelandic, Language.Swedish, Language.Turkish],
        ["Óó"] = [Language.Catalan, Language.Hungarian, Language.Icelandic, Language.Irish, Language.Polish, Language.Portuguese, Language.Slovak, Language.Spanish, Language.Vietnamese, Language.Yoruba],
        ["ÁáÍíÚú"] = [Language.Catalan, Language.Czech, Language.Icelandic, Language.Irish, Language.Hungarian, Language.Portuguese, Language.Slovak, Language.Spanish, Language.Vietnamese, Language.Yoruba],
        ["Éé"] = [Language.Catalan, Language.Czech, Language.French, Language.Hungarian, Language.Icelandic, Language.Irish, Language.Italian, Language.Portuguese, Language.Slovak, Language.Spanish, Language.Vietnamese, Language.Yoruba],
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

    public IReadOnlySet<Language> Languages => _languages;

    public LanguageDetector(
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
        {
            PreloadLanguageModels();
        }
    }

    private void PreloadLanguageModels()
    {
        var range = _isLowAccuracyModeEnabled
            ? Enumerable.Range(3, 1)
            : Enumerable.Range(1, 5);

        var languagesAndRange = _languages.SelectMany(language => range.Select(ngramLength => (language, ngramLength)));

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

    /// <summary>
    /// Detects the language of the given input text.
    /// </summary>
    /// <param name="text">The input text to detect the language for.</param>
    /// <returns>The identified language or <see cref="Language.Unknown"/></returns>
    public Language DetectLanguageOf(string text)
    {
        var confidenceValues = ComputeLanguageConfidenceValues(text);
        if (confidenceValues.Count == 0)
            return Language.Unknown;

        var mostLikelyLanguage = confidenceValues.First().Key;
        if (confidenceValues.Count == 1)
            return mostLikelyLanguage;

        var mostLikelyLanguageProbability = confidenceValues[mostLikelyLanguage];
        var secondMostLikelyLanguageProbability = confidenceValues.ElementAt(1).Value;

        return mostLikelyLanguageProbability.Equals(secondMostLikelyLanguageProbability)
            ? Language.Unknown
            : (mostLikelyLanguageProbability - secondMostLikelyLanguageProbability) < _minimumRelativeDistance
                ? Language.Unknown
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

        if (cleanedUpText.Length == 0 || NoLetter.IsMatch(cleanedUpText))
            return values;

        var words = SplitTextIntoWords(cleanedUpText);
        var languageDetectedByRules = DetectLanguageWithRules(words);

        if (languageDetectedByRules != Language.Unknown)
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
        {
            return values;
        }

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
        {
            return new Dictionary<Language, double>();
        }

        var highestProbability = summedUpProbabilities.Max(p => p.Value);
        var confidenceValues = summedUpProbabilities.ToDictionary(
            p => p.Key,
            p => (double)highestProbability / p.Value);

        return confidenceValues
            .OrderByDescending(c => c.Value)
            .ThenBy(c => c.Key)
            .ToIndexedDictionary();
    }

    private Dictionary<Language, float> SumUpProbabilities(
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
            {
                summedUpProbabilities[language] /= count;
            }
        }

        return summedUpProbabilities.Where(p => p.Value != 0).ToDictionary();
    }

    private Dictionary<Language,int> CountUnigramsOfInputText(TestDataLanguageModel unigramLanguageModel, HashSet<Language> filteredLanguages)
    {
        var unigramCounts = new Dictionary<Language, int>();
        foreach (var language in filteredLanguages)
        {
            foreach (var unigram in unigramLanguageModel.Ngrams)
            {
                var probability = LookupNgramProbability(language, unigram);
                if (probability > 0)
                {
                    unigramCounts.IncrementCounter(language);
                }
            }
        }

        return unigramCounts;
    }

    internal Dictionary<Language, float> ComputeLanguageProbabilities(TestDataLanguageModel testDataModel, IReadOnlySet<Language> filteredLanguages)
    {
        var probabilities = new Dictionary<Language, float>();
        foreach (var language in filteredLanguages)
        {
            probabilities[language] = ComputeSumOfNgramProbabilities(language, testDataModel.Ngrams);
        }
        return probabilities.Where(p => p.Value < 0).ToDictionary();
    }

    internal float ComputeSumOfNgramProbabilities(Language language, HashSet<Ngram> ngrams)
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

    internal float LookupNgramProbability(Language language, Ngram ngram)
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

    private Dictionary<string, float> LoadLanguageModels(Dictionary<Language, Dictionary<string, float>> languageModels, Language language, int ngramLength)
    {
        lock (languageModels)
        {
            if (languageModels.TryGetValue(language, out var value))
            {
                return value;
            }
        }

        var model = LoadLanguageModel(language, ngramLength);
        lock (languageModels)
        {
            languageModels.TryAdd(language, model);
            return model;
        }
    }

    private Dictionary<string,float> LoadLanguageModel(Language language, int ngramLength)
    {
        var file = $"Lingua.LanguageModels.{language.IsoCode6391().ToString().ToLowerInvariant()}.{Ngram.GetNgramNameByLength(ngramLength)}s.json";
        using var stream = typeof(LanguageDetector).Assembly.GetManifestResourceStream(file);
        return stream == null
            ? new Dictionary<string, float>()
            : TrainingDataLanguageModel.FromJson(stream);
    }

    /// <summary>
    /// Unloads all language models loaded by this [LanguageDetector] instance
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
                {
                    distinctAlphabets.Add(count);
                }

                if (distinctAlphabets.Count == 1)
                {
                    return _languages;
                }

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
                        {
                            languageCounts.IncrementCounter(language);
                        }
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
                        wordLanguageCounts.IncrementCounter(language);;
                        isMatch = true;
                        break;
                    }
                }

                if (!isMatch)
                {
                    if (Alphabet.Han.Matches(ch))
                    {
                        wordLanguageCounts.IncrementCounter(Language.Chinese);
                    }
                    else if (IsJapaneseAlphabet(ch))
                    {
                        wordLanguageCounts.IncrementCounter(Language.Japanese);
                    }
                    else if (Alphabet.Latin.Matches(ch) || Alphabet.Cyrillic.Matches(ch) || Alphabet.Devanagari.Matches(ch))
                    {
                        foreach (var language in _languagesWithUniqueCharacters.Where(l => l.UniqueCharacters()?.Contains(ch) ?? false))
                        {
                            wordLanguageCounts.IncrementCounter(language);
                        }
                    }
                }
            }

            switch (wordLanguageCounts.Count)
            {
                case 0:
                    totalLanguageCounts.IncrementCounter(Language.Unknown);
                    break;
                case 1:
                {
                    var language = wordLanguageCounts.Single().Key;
                    totalLanguageCounts.IncrementCounter(_languages.Contains(language) ? language : Language.Unknown);
                    break;
                }
                default:
                {
                    var sortedWordLanguageCounts = wordLanguageCounts
                        .OrderByDescending(a => a.Value)
                        .ToList();
                    var (mostFrequentLanguage, firstCharCount) = sortedWordLanguageCounts[0];
                    var (_, secondCharCount) = sortedWordLanguageCounts[1];

                    if (firstCharCount > secondCharCount && _languages.Contains(mostFrequentLanguage)) {
                        totalLanguageCounts.IncrementCounter(mostFrequentLanguage);
                    } else {
                        totalLanguageCounts.IncrementCounter(Language.Unknown);
                    }

                    break;
                }
            }
        }

        totalLanguageCounts.TryGetValue(Language.Unknown, out var unknownLanguageCount);
        if (unknownLanguageCount < (0.5 * words.Count))
        {
            totalLanguageCounts.Remove(Language.Unknown);
        }

        switch (totalLanguageCounts.Count)
        {
            case 0:
                return Language.Unknown;
            case 1:
                return totalLanguageCounts.Keys.Single();
            case 2 when
                totalLanguageCounts.ContainsKey(Language.Chinese) &&
                totalLanguageCounts.ContainsKey(Language.Japanese):
                return Language.Japanese;
        }

        using var sortedTotalLanguageCounts = totalLanguageCounts
            .OrderByDescending(l => l.Value)
            .GetEnumerator();

        sortedTotalLanguageCounts.MoveNext();
        var (mostFrequentTotalLanguage, firstTotalCharCount) = sortedTotalLanguageCounts.Current;
        sortedTotalLanguageCounts.MoveNext();
        var (_, secondTotalCharCount) = sortedTotalLanguageCounts.Current;

        return firstTotalCharCount == secondTotalCharCount ? Language.Unknown : mostFrequentTotalLanguage;
    }

    private bool IsJapaneseAlphabet(char ch) =>
        ch.GetScript() is UnicodeScript.Hiragana or UnicodeScript.Katakana or UnicodeScript.Han;

    internal List<string> SplitTextIntoWords(string text)
    {
        var words = new List<string>();
        var nextWordStart = 0;
        for (var i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            if (ch == ' ')
            {
                if (nextWordStart != i)
                {
                    words.Add(text.Substring(nextWordStart, i - nextWordStart));
                }

                nextWordStart = i + 1;
            }
            else if (ch.IsLogogram())
            {
                if (nextWordStart != i)
                {
                    words.Add(text.Substring(nextWordStart, i - nextWordStart));
                }

                words.Add(text[i].ToString());
                nextWordStart = i + 1;
            }
        }

        if (nextWordStart != text.Length)
        {
            words.Add(text.Substring(nextWordStart));
        }

        return words;
    }

    internal string CleanUpInputText(string text) =>
        MultipleWhitespace.Replace(
            Numbers.Replace(
                Punctuation.Replace(text.Trim().ToLowerInvariant(), ""), ""), " ");

    protected bool Equals(LanguageDetector other) =>
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
