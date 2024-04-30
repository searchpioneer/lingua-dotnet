using System.Collections;

namespace Lingua.Internal;

/// <summary>
/// A connected string of N items from a sample of text.
/// </summary>
public readonly struct Ngram : IEquatable<Ngram>
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of <see cref="Ngram"/>
    /// </summary>
    /// <param name="value">The sample of text.</param>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is greater than 5</exception>
    public Ngram(string value)
    {
        if (value.Length > 5)
	        throw new ArgumentException($"length of ngram '{value}' is not in range 0..5");

        _value = value;
    }

    /// <summary>
    /// An Ngram of zero length
    /// </summary>
    public static readonly Ngram Zerogram = new("");

    /// <summary>
    /// Whether value is empty.
    /// </summary>
    public bool IsEmpty => _value.Length == 0;

    /// <summary>
    /// Gets the length of the value.
    /// </summary>
    public int Length => _value.Length;

    /// <summary>
    /// Decrements and returns a new instance of an <see cref="Ngram"/> that is a substring of this instance.
    /// </summary>
    /// <returns>A new instance of <see cref="Ngram"/></returns>
    /// <exception cref="InvalidOperationException">If the Ngram has a length of 0.</exception>
    public Ngram Dec() =>
	    _value.Length switch
	    {
		    0 => throw new InvalidOperationException("Zerogram is ngram type of lowest order and can not be decremented"),
		    1 => Zerogram,
		    _ => new Ngram(_value.Substring(0, _value.Length - 1))
	    };

    /// <inheritdoc />
    public override string ToString() => _value;

    /// <inheritdoc />
    public bool Equals(Ngram other) => _value == other._value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Ngram other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    public IEnumerable<Ngram> RangeOfLowerOrderNGrams() =>
        new NgramEnumerable(this, new Ngram(_value[0].ToString()));

    internal static string GetNgramNameByLength(int ngramLength) =>
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

public struct NgramEnumerable : IEnumerable<Ngram>
{
    private readonly Ngram _start;

    public NgramEnumerable(Ngram start, Ngram endInclusive)
    {
        if (endInclusive.Length > start.Length)
	        throw new ArgumentException($"'{start}' must be of higher order than '{endInclusive}'");

        _start = start;
    }

    /// <inheritdoc />
    public IEnumerator<Ngram> GetEnumerator() => new NgramEnumerator(_start);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public struct NgramEnumerator : IEnumerator<Ngram>
{
    private readonly Ngram _start;
    private Ngram? _current;

    /// <summary>
    /// Initializes a new instance of <see cref="NgramEnumerator"/>
    /// </summary>
    /// <param name="start"></param>
    public NgramEnumerator(Ngram start) => _start = start;

    /// <inheritdoc />
    public bool MoveNext()
    {
        if (_current is null)
        {
            _current = _start;
            return true;
        }

        if (_current.Value.IsEmpty)
	        return false;

        _current = _current.Value.Dec();
        return !_current.Value.IsEmpty;
    }

    /// <inheritdoc />
    public void Reset() => _current = null;

    /// <inheritdoc />
    public Ngram Current => _current!.Value;

    object IEnumerator.Current => Current;

    /// <inheritdoc />
    public void Dispose()
    {
    }
}
