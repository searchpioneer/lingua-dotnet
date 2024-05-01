using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Fractions;
using Lingua.Api;

namespace Lingua.Internal;

internal class TrainingDataLanguageModel
{
    private record JsonLanguageModel(Language language, Dictionary<Fraction, string> ngrams);

    private readonly Language _language;
    private readonly Dictionary<Ngram, int> _absoluteFrequencies;
    private readonly Dictionary<Ngram, Fraction> _relativeFrequencies;

    public Dictionary<Ngram, int> AbsoluteFrequencies => _absoluteFrequencies;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        Converters = { new FractionConverter() },
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
        Dictionary<Ngram,int> lowerNgramAbsoluteFrequencies)
    {
        if (ngramLength is < 1 or > 5)
        {
            throw new ArgumentException($"ngram length {ngramLength} is not in range 1..5");
        }

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

    public static Dictionary<string, float> FromJson(Stream stream)
    {
        var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        var reader = new Utf8JsonReader(memoryStream.ToArray());
        var frequencies = new Dictionary<string, float>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString()!;
                switch (propertyName)
                {
                    case nameof(JsonLanguageModel.language):
                        reader.Read();
                        break;
                    case nameof(JsonLanguageModel.ngrams):
                        reader.Read();
                        while (reader.Read())
                        {
                            // each ngram is represented as a property in an object of the form {"<fraction>": "<ngram>"}
                            if (reader.TokenType != JsonTokenType.PropertyName)
                                break;

                            var fraction = reader.GetString()!.AsSpan();
                            var delimiter = fraction.IndexOf('/');
                            var frequency = float.Parse(fraction[..delimiter]) / int.Parse(fraction[(delimiter+1)..]);
                            reader.Read();
                            var ngrams = reader.GetString()!.AsSpan();
                            foreach (var ngram in ngrams.Split(" "))
                            {
                                frequencies.Add(ngram.AsSpan().ToString(), frequency);
                            }
                        }
                        break;
                    default:
                        throw new JsonException($"Unexpected property name '{propertyName}' in language model JSON");
                }
            }
        }

        frequencies.TrimExcess();
        return frequencies;
    }

    private static Dictionary<Ngram,Fraction> ComputeRelativeFrequencies(int ngramLength, Dictionary<Ngram,int> absoluteFrequencies, Dictionary<Ngram,int> lowerNgramAbsoluteFrequencies)
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

    private static Dictionary<Ngram,int> ComputeAbsoluteFrequencies(IEnumerable<string> text, int ngramLength, string charClass)
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

