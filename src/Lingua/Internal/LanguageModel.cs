using System.Buffers;
using System.Collections.Frozen;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Lingua.Internal;

internal static class LanguageModel
{
	public static FrozenDictionary<string, double> FromJson(Stream stream)
	{
		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		var bytes = new ReadOnlySequence<byte>(memoryStream.ToArray());
		var reader = new Utf8JsonReader(bytes);
		var frequencies = new Dictionary<string, double>();

		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var propertyName = reader.GetString()!;
				switch (propertyName)
				{
					case "language":
						reader.Read();
						break;
					case "ngrams":
						reader.Read();
						while (reader.Read())
						{
							// each ngram is represented as a property in an object of the form {"<fraction>": "<ngram>"}
							if (reader.TokenType != JsonTokenType.PropertyName)
								break;

							var fraction = reader.GetString()!.AsSpan();
							var delimiter = fraction.IndexOf('/');
							var frequency = double.Parse(fraction[..delimiter]) / int.Parse(fraction[(delimiter + 1)..]);
							reader.Read();
							var ngrams = new SplitSpanEnumerable<char>(reader.GetString()!.AsSpan(), " ");
							foreach (var ngram in ngrams)
								frequencies.Add(ngram.AsSpan().ToString(), frequency);
						}
						break;
					default:
						throw new JsonException($"Unexpected property name '{propertyName}' in language model JSON");
				}
			}
		}

		frequencies.TrimExcess();
		return frequencies.ToFrozenDictionary();
	}
}

internal readonly ref struct SplitSpanEnumerable<T>
	where T : IEquatable<T>
{
	private readonly ReadOnlySpan<T> _source;
	private readonly ReadOnlySpan<T> _separator;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SplitSpanEnumerable(ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
	{
		if (separator.Length == 0)
			throw new ArgumentException("Requires non-empty value", nameof(separator));

		_source = source;
		_separator = separator;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SplitSpanEnumerator<T> GetEnumerator() => new(_source, _separator);
}

internal ref struct SplitSpanEnumerator<T>
	where T : IEquatable<T>
{
	private int _nextStartIndex = 0;
	private readonly ReadOnlySpan<T> _separator;
	private readonly ReadOnlySpan<T> _source;
	private SplitSpan _current;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SplitSpanEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
	{
		if (separator.Length == 0)
			throw new ArgumentException("Requires non-empty value", nameof(separator));

		_source = source;
		_separator = separator;
	}

	public bool MoveNext()
	{
		if (_nextStartIndex > _source.Length)
			return false;

		var nextSource = _source[_nextStartIndex..];
		var foundIndex = nextSource.IndexOf(_separator);
		var length = -1 < foundIndex
			? foundIndex
			: nextSource.Length;

		_current = new SplitSpan { StartIndex = _nextStartIndex, Length = length, Source = _source, };
		_nextStartIndex += _separator.Length + _current.Length;

		return true;
	}

	public SplitSpan Current
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _current;
	}

	public readonly ref struct SplitSpan
	{
		public int StartIndex { get; init; }
		public int Length { get; init; }
		public ReadOnlySpan<T> Source { get; init; }

		public ReadOnlySpan<T> AsSpan() => Source.Slice(StartIndex, Length);

		public static implicit operator ReadOnlySpan<T>(SplitSpan value) => value.AsSpan();
	}
}


