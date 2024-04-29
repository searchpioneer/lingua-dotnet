using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Fractions;
using Lingua.Api;

namespace Lingua.Internal;

public class TrainingDataLanguageModel
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

public class FractionConverter : JsonConverter<Fraction>
{
    public override Fraction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Fraction.FromString(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, Fraction value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}

public readonly ref struct SpanSplitter<T>
    where T : IEquatable<T>
{
    private readonly ReadOnlySpan<T> _source;
    private readonly ReadOnlySpan<T> _separator;

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public SpanSplitter( ReadOnlySpan<T> source, ReadOnlySpan<T> separator )
    {
        if( 0 == separator.Length )
        {
            throw new ArgumentException( "Requires non-empty value", nameof( separator ) );
        }

        _source = source;
        _separator = separator;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public SpanSplitEnumerator<T> GetEnumerator() => new( _source, _separator );
}

public ref struct SpanSplitEnumerator<T>
    where T : IEquatable<T>
{
    private int _nextStartIndex = 0;
    private readonly ReadOnlySpan<T> _separator;
    private readonly ReadOnlySpan<T> _source;
    private SpanSplitValue _current;

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public SpanSplitEnumerator( ReadOnlySpan<T> source, ReadOnlySpan<T> separator )
    {
        _source = source;
        _separator = separator;

        if( 0 == separator.Length )
        {
            throw new ArgumentException( "Requires non-empty value", nameof( separator ) );
        }
    }

    public bool MoveNext()
    {
        if( _nextStartIndex > _source.Length )
        {
            return false;
        }

        var nextSource = _source.Slice( _nextStartIndex );

        var foundIndex = nextSource.IndexOf( _separator );

        var length = -1 < foundIndex
            ? foundIndex
            : nextSource.Length;

        _current = new SpanSplitValue
        {
            StartIndex = _nextStartIndex,
            Length = length,
            Source = _source,
        };

        _nextStartIndex += _separator.Length + _current.Length;

        return true;
    }

    public SpanSplitValue Current
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        get => _current;
    }

    public readonly ref struct SpanSplitValue
    {
        public int StartIndex { get; init; }
        public int Length { get; init; }
        public ReadOnlySpan<T> Source { get; init; }

        public ReadOnlySpan<T> AsSpan() => Source.Slice( StartIndex, Length );

        public static implicit operator ReadOnlySpan<T>( SpanSplitValue value )
            => value.AsSpan();
    }
}


public static class ExtensionMethods
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static SpanSplitter<T> Split<T>( this ReadOnlySpan<T> source, ReadOnlySpan<T> separator )
        where T : IEquatable<T> =>
	    new( source, separator );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static SpanSplitter<T> Split<T>( this Span<T> source, ReadOnlySpan<T> separator )
        where T : IEquatable<T> =>
	    new( source, separator );
}
