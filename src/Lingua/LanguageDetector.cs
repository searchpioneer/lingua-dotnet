using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Lingua.Internal;
using static Lingua.Language;

namespace Lingua;

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

	internal static readonly ConcurrentDictionary<Language, Lazy<FrozenDictionary<string, double>>> UnigramLanguageModels = new();
	internal static readonly ConcurrentDictionary<Language, Lazy<FrozenDictionary<string, double>>> BigramLanguageModels = new();
	internal static readonly ConcurrentDictionary<Language, Lazy<FrozenDictionary<string, double>>> TrigramLanguageModels = new();
	internal static readonly ConcurrentDictionary<Language, Lazy<FrozenDictionary<string, double>>> QuadrigramLanguageModels = new();
	internal static readonly ConcurrentDictionary<Language, Lazy<FrozenDictionary<string, double>>> FivegramLanguageModels = new();

	private readonly HashSet<Language> _languages;
	private readonly double _minimumRelativeDistance;
	private readonly bool _isLowAccuracyModeEnabled;
	private readonly Dictionary<Alphabet, Language> _oneLanguageAlphabets;
	private readonly IEnumerable<Language> _languagesWithUniqueCharacters;

	private static readonly int[] LowAccuracyRange = [3];
	private static readonly int[] HighAccuracyRange = [1, 2, 3, 4, 5];
	private static readonly Lazy<FrozenDictionary<string, double>> Empty = new(() =>
		Enumerable.Empty<KeyValuePair<string, double>>().ToFrozenDictionary());

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
	/// Computes confidence values for each language supported by this detector for the given
	/// input text. These values denote how likely it is that the given text has been written
	/// in any of the languages supported by this detector.
	/// <para />
	/// The <see cref="IDictionary{TKey,TValue}"/> returned by this method contains all languages supported by
	/// this detector, together with their confidence values. The entries are sorted by confidence value descending.
	/// Each confidence value is a probability between 0.0 and 1.0. The probabilities of all languages will
	/// sum to 1.0. If the language is unambiguously identified by the rule engine, the value
	/// 1.0 will always be returned for this language. The other languages will receive a value
	/// of 0.0.
	/// </summary>
	/// <param name="text">The input text to detect the language for.</param>
	/// <returns>A dictionary of all possible languages, sorted by their confidence value in descending order.</returns>
	public IDictionary<Language, double> ComputeLanguageConfidenceValues(string text)
	{
		var values = new Dictionary<Language, double>(_languages.Count);
		foreach (var language in _languages)
			values[language] = 0;

		var cleanedUpText = CleanUpInputText(text);
		if (cleanedUpText.Length == 0 || NoLetter().IsMatch(cleanedUpText))
			return values;

		var words = SplitTextIntoWords(cleanedUpText);
		var languageDetectedByRules = DetectLanguageWithRules(words);

		if (languageDetectedByRules != Unknown)
		{
			values[languageDetectedByRules] = 1;
			return values.OrderByDescending(p => p.Value).ToIndexedDictionary();
		}

		var filteredLanguages = FilterLanguagesByRules(words);
		if (filteredLanguages.Count == 1)
		{
			var filteredLanguage = filteredLanguages.Single();
			values[filteredLanguage] = 1.0;
			return values.OrderByDescending(p => p.Value).ToIndexedDictionary();
		}

		if (_isLowAccuracyModeEnabled && cleanedUpText.Length < 3)
			return values;

		var ngramSizeRange = (cleanedUpText.Length >= HighAccuracyModeMaxTextLength || _isLowAccuracyModeEnabled
			? LowAccuracyRange
			: HighAccuracyRange)
			.Where(r => cleanedUpText.Length >= r)
			.ToArray();

		var allProbabilities = new Dictionary<Language, double>[ngramSizeRange.Length];
		var unigramCounts = new Dictionary<Language, int>();
		var startValue = ngramSizeRange[0];

		Parallel.ForEach(ngramSizeRange, i =>
		{
			var testDataModel = TestLanguageModel.FromText(cleanedUpText, i);
			var probabilities = ComputeLanguageProbabilities(testDataModel, filteredLanguages);
			if (i == 1)
			{
				var unigramFilteredLanguages = _languages.Count > 0
					? filteredLanguages.Where(f => _languages.Contains(f)).ToHashSet()
					: filteredLanguages;
				unigramCounts = CountUnigrams(testDataModel, unigramFilteredLanguages);
			}
			allProbabilities[i - startValue] = probabilities;
		});

		var summedUpProbabilities = SumUpProbabilities(allProbabilities, unigramCounts, filteredLanguages);
		return summedUpProbabilities.Count == 0
			? values.OrderByDescending(c => c.Value).ToIndexedDictionary()
			: ComputeConfidenceValues(values, allProbabilities, summedUpProbabilities);
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
		foreach (var language in _languages)
			TrigramLanguageModels.TryRemove(language, out _);

		if (!_isLowAccuracyModeEnabled)
		{
			foreach (var language in _languages)
				UnigramLanguageModels.TryRemove(language, out _);

			foreach (var language in _languages)
				BigramLanguageModels.TryRemove(language, out _);

			foreach (var language in _languages)
				QuadrigramLanguageModels.TryRemove(language, out _);

			foreach (var language in _languages)
				FivegramLanguageModels.TryRemove(language, out _);
		}
	}

	private IDictionary<Language, double> ComputeConfidenceValues(
		Dictionary<Language, double> values,
		Dictionary<Language, double>[] allProbabilities,
		Dictionary<Language, double> summedUpProbabilities)
	{
		var denominator = summedUpProbabilities.Values.Sum();

		// If the denominator is still zero, the exponent of the summed
		// log probabilities is too large to be computed for very long input strings.
		// So we simply set the probability of the most likely language to 1.0 and
		// leave the other languages at 0.0.
		if (denominator == 0)
		{
			var probabilityMap = allProbabilities[0];
			var mostLikelyLanguage = probabilityMap.MaxBy(p => p.Value).Key;
			UpdateConfidenceValues(values, mostLikelyLanguage, 1.0);
		}
		else
		{
			foreach (var (language, probability) in summedUpProbabilities)
			{
				foreach (var value in values)
				{
					if (value.Key == language)
					{
						// apply softmax function
						var normalizedProbability = probability / denominator;
						values[language] = normalizedProbability;
					}
				}
			}
		}

		return values.OrderByDescending(p => p.Value).ToIndexedDictionary();
	}

	private static void UpdateConfidenceValues(Dictionary<Language, double> values, Language language, double probability)
	{
		foreach (var value in values)
		{
			if (value.Key == language)
			{
				values[language] = probability;
				break;
			}
		}
	}

	private static Dictionary<Language, double> SumUpProbabilities(
		Dictionary<Language, double>[] probabilities,
		Dictionary<Language, int> unigramCounts,
		HashSet<Language> filteredLanguages)
	{
		var summedUpProbabilities = new Dictionary<Language, double>();
		foreach (var language in filteredLanguages)
		{
			var sum = 0d;
			foreach (var probability in probabilities)
			{
				probability.TryGetValue(language, out var value);
				sum += value;
			}

			if (unigramCounts.TryGetValue(language, out var count))
				sum /= count;

			if (sum != 0)
				summedUpProbabilities[language] = Math.Exp(sum);
		}

		return summedUpProbabilities;
	}

	private static Dictionary<Language, int> CountUnigrams(TestLanguageModel unigramLanguageModel, HashSet<Language> filteredLanguages)
	{
		var unigramCounts = new Dictionary<Language, int>();
		foreach (var language in filteredLanguages)
		{
			foreach (var unigram in unigramLanguageModel.Ngrams)
			{
				var probability = LookupNgramProbability(language, unigram.AsSpan());
				if (probability > 0)
					unigramCounts.IncrementCounter(language);
			}
		}

		return unigramCounts;
	}

	internal static Dictionary<Language, double> ComputeLanguageProbabilities(TestLanguageModel testModel, IReadOnlySet<Language> filteredLanguages)
	{
		var probabilities = new Dictionary<Language, double>();
		foreach (var language in filteredLanguages)
		{
			var sum = ComputeSumOfNgramProbabilities(language, testModel.Ngrams);
			if (sum < 0)
				probabilities[language] = sum;
		}

		return probabilities;
	}

	internal static double ComputeSumOfNgramProbabilities(Language language, HashSet<Ngram> ngrams)
	{
		var sum = 0d;
		foreach (var ngram in ngrams)
		{
			foreach (var elem in ngram.LowerOrderNGrams())
			{
				var probability = LookupNgramProbability(language, elem);
				if (probability > 0)
				{
					sum += Math.Log(probability);
					break;
				}
			}
		}

		return sum;
	}

	internal static double LookupNgramProbability(Language language, ReadOnlySpan<char> ngram)
	{
		var model = LoadLanguageModel(language, ngram.Length);
#if NET9_0
		var lookup = model.GetAlternateLookup<ReadOnlySpan<char>>();
		return lookup.TryGetValue(ngram, out var result) ? result : 0;
#else
		// ReSharper disable once CanSimplifyDictionaryTryGetValueWithGetValueOrDefault - skip the null check
		return model.TryGetValue(ngram.ToString(), out var result) ? result : 0;
#endif
	}

	private static FrozenDictionary<string, double> LoadLanguageModel(Language language, int ngramLength)
	{
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

		return LoadLanguageModels(languageModels, language, ngramLength);
	}

	private void PreloadLanguageModels()
	{
		var range = _isLowAccuracyModeEnabled
			? LowAccuracyRange
			: HighAccuracyRange;

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

	private static FrozenDictionary<string, double> LoadLanguageModels(ConcurrentDictionary<Language, Lazy<FrozenDictionary<string, double>>> languageModels, Language language, int ngramLength) =>
		languageModels.GetOrAdd(language, static (l, nl) =>
			new Lazy<FrozenDictionary<string, double>>(() => ReadLanguageModel(l, nl)), ngramLength).Value;

	private static FrozenDictionary<string, double> ReadLanguageModel(Language language, int ngramLength)
	{
		var isoCode = language.IsoCode6391().ToString().ToLowerInvariant();
		var nGramName = Ngram.GetNameByLength(ngramLength);
		var file = Path.Combine("Lingua", "LanguageModels", isoCode, $"{nGramName}s.json.br");

		try
		{
			using var stream = File.OpenRead(file);
			using var brotliStream = new BrotliStream(stream, CompressionMode.Decompress);
			return LanguageModel.FromJson(brotliStream);
		}
		catch (FileNotFoundException)
		{
			// there may not be a model for a given ngram/language
			return Empty.Value;
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
					detectedAlphabets.IncrementCounter(alphabet, word.Length);
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
			var charactersSpan = characters.AsSpan();

			foreach (var word in words)
			{
				var wordSpan = word.AsSpan();
				foreach (var ch in charactersSpan)
				{
					if (wordSpan.Contains(ch))
					{
						foreach (var language in relevantLanguages)
							languageCounts.IncrementCounter(language);
					}
				}
			}
		}

		var halfWordCount = 0.5 * words.Count;
		var languagesSubset = languageCounts
			.Where(l => l.Value >= halfWordCount)
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
		var halfWordCount = 0.5 * words.Count;
		if (unknownLanguageCount < halfWordCount)
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

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj))
			return false;
		if (ReferenceEquals(this, obj))
			return true;
		if (obj.GetType() != GetType())
			return false;
		return Equals((LanguageDetector)obj);
	}

	/// <inheritdoc />
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
