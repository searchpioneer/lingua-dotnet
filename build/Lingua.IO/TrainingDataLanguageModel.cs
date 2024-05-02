using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Fractions;
using Lingua.Api;
using Lingua.Internal;

namespace Lingua.IO;

internal class TrainingDataLanguageModel
{
	private record JsonLanguageModel(Language language, Dictionary<Fraction, string> ngrams);

	private readonly Language _language;
	private readonly Dictionary<Ngram, int> _absoluteFrequencies;
	private readonly Dictionary<Ngram, Fraction> _relativeFrequencies;

	public Dictionary<Ngram, int> AbsoluteFrequencies => _absoluteFrequencies;

	private static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		Converters = { new FractionConverter(), new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper) },
	};

	public TrainingDataLanguageModel(
		Language language,
		Dictionary<Ngram, int> absoluteFrequencies,
		Dictionary<Ngram, Fraction> relativeFrequencies)
	{
		_language = language;
		_absoluteFrequencies = absoluteFrequencies;
		_relativeFrequencies = relativeFrequencies;
	}

	public string ToJson()
	{
		var nGramsByFraction = new Dictionary<Fraction, List<Ngram>>();
		foreach (var (ngram, fraction) in _relativeFrequencies)
		{
			if (!nGramsByFraction.TryGetValue(fraction, out var ngrams))
			{
				ngrams = new List<Ngram>();
				nGramsByFraction.Add(fraction, ngrams);
			}
			ngrams.Add(ngram);
		}

		var jsonLanguageModel =
			new JsonLanguageModel(
				_language,
				nGramsByFraction.ToDictionary(k => k.Key, v => string.Join(' ', v.Value)));

		return JsonSerializer.Serialize(jsonLanguageModel, JsonSerializerOptions);
	}

	public static TrainingDataLanguageModel FromText(
		IEnumerable<string> text,
		Language language,
		int ngramLength,
		string charClass,
		Dictionary<Ngram, int> lowerNgramAbsoluteFrequencies)
	{
		if (ngramLength is < 1 or > 5)
			throw new ArgumentException($"ngram length {ngramLength} is not in range 1..5");

		var absoluteFrequencies = ComputeAbsoluteFrequencies(
			text,
			ngramLength,
			charClass
		);

		var relativeFrequencies = ComputeRelativeFrequencies(
			ngramLength,
			absoluteFrequencies,
			lowerNgramAbsoluteFrequencies
		);

		return new TrainingDataLanguageModel(
			language,
			absoluteFrequencies,
			relativeFrequencies
		);
	}

	private static Dictionary<Ngram, Fraction> ComputeRelativeFrequencies(int ngramLength, Dictionary<Ngram, int> absoluteFrequencies, Dictionary<Ngram, int> lowerNgramAbsoluteFrequencies)
	{
		var ngramProbabilities = new Dictionary<Ngram, Fraction>();
		var totalNgramFrequency = absoluteFrequencies.Values.Sum();
		foreach (var (ngram, frequency) in absoluteFrequencies)
		{
			var denominator = ngramLength == 1 || !lowerNgramAbsoluteFrequencies.Any()
				? totalNgramFrequency
				: lowerNgramAbsoluteFrequencies[new Ngram(ngram.ToString().Substring(0, ngramLength - 1))];

			ngramProbabilities[ngram] = new Fraction(frequency, denominator);
		}

		return ngramProbabilities;
	}

	private static Dictionary<Ngram, int> ComputeAbsoluteFrequencies(IEnumerable<string> text, int ngramLength, string charClass)
	{
		var absoluteFrequencies = new Dictionary<Ngram, int>();
		var regex = new Regex($"^[{charClass}]+$");

		foreach (var line in text)
		{
			var lowerCasedLine = line.ToLowerInvariant().AsSpan();
			for (var i = 0; i < lowerCasedLine.Length - ngramLength; i++)
			{
				var textSlice = lowerCasedLine.Slice(i, ngramLength);
				if (regex.IsMatch(textSlice))
				{
					var ngram = new Ngram(textSlice.ToString());
					absoluteFrequencies.TryGetValue(ngram, out var count);
					absoluteFrequencies[ngram] = count + 1;
				}
			}
		}

		return absoluteFrequencies;
	}


}

