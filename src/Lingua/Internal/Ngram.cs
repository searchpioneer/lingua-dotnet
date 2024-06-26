using System.Runtime.CompilerServices;

namespace Lingua.Internal;

/// <summary>
/// A connected string of N items from a sample of text.
/// </summary>
internal readonly struct Ngram : IEquatable<Ngram>
{
	private readonly string _value;

	public Ngram(string value)
	{
		if (value.Length > 5)
			throw new ArgumentException($"length of ngram '{value}' is not in range 0..5");

		_value = value;
	}

	public ReadOnlySpan<char> AsSpan() => _value.AsSpan();

	public int Length => _value.Length;

	public string Value => _value;

	public override string ToString() => _value;

	public bool Equals(Ngram other) => _value == other._value;

	public override bool Equals(object? obj) => obj is Ngram other && Equals(other);

	public override int GetHashCode() => _value.GetHashCode();

	public NgramEnumerable LowerOrderNGrams() => new(this);

	internal static string GetNameByLength(int ngramLength) =>
		ngramLength switch
		{
			1 => "unigram",
			2 => "bigram",
			3 => "trigram",
			4 => "quadrigram",
			5 => "fivegram",
			_ => throw new ArgumentException($"ngram length {ngramLength} is not in range 1..5")
		};
}

internal readonly ref struct NgramEnumerable(Ngram start)
{
	public NgramEnumerator GetEnumerator() => new(start);
}

internal ref struct NgramEnumerator(Ngram start)
{
	private readonly ReadOnlySpan<char> _start = start.AsSpan();
	private int _currentEndIndex = start.Length + 1;

	public bool MoveNext()
	{
		if (_currentEndIndex == 0)
			return false;

		_currentEndIndex--;
		return _currentEndIndex > 0;
	}

	public void Reset() => _currentEndIndex = _start.Length + 1;

	// IMPORTANT: MoveNext() must be called before Current
	public OrderedNgram Current
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => new(_start[.._currentEndIndex]);
	}

	public void Dispose()
	{
	}

	internal readonly ref struct OrderedNgram(ReadOnlySpan<char> ngram)
	{
		private readonly ReadOnlySpan<char> _ngram = ngram;

		public static implicit operator ReadOnlySpan<char>(OrderedNgram entry) => entry._ngram;
	}
}


