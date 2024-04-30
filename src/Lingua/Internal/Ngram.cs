using System.Collections;

namespace Lingua.Internal;

public readonly struct Ngram : IEquatable<Ngram>
{
    private readonly string _value;

    public Ngram(string value)
    {
        if (value.Length > 5)
	        throw new ArgumentException($"length of ngram '{value}' is not in range 0..5");

        _value = value;
    }

    public static readonly Ngram Zerogram = new("");

    public bool IsEmpty => _value.Length == 0;

    public int Length => _value.Length;

    public Ngram Dec() =>
	    _value.Length switch
	    {
		    0 => throw new Exception("Zerogram is ngram type of lowest order and can not be decremented"),
		    1 => Zerogram,
		    _ => new Ngram(_value.Substring(0, _value.Length - 1))
	    };

    public override string ToString() => _value;
    public bool Equals(Ngram other) => _value == other._value;

    public override bool Equals(object? obj) => obj is Ngram other && Equals(other);

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

public class NgramEnumerable : IEnumerable<Ngram>
{
    private readonly Ngram _start;

    public NgramEnumerable(Ngram start, Ngram endInclusive)
    {
        if (endInclusive.Length > start.Length)
	        throw new ArgumentException($"'{start}' must be of higher order than '{endInclusive}'");

        _start = start;
    }

    public IEnumerator<Ngram> GetEnumerator() => new NgramEnumerator(_start);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class NgramEnumerator : IEnumerator<Ngram>
{
    private readonly Ngram _start;
    private Ngram? _current;

    public NgramEnumerator(Ngram start) => _start = start;

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

    public void Reset() => _current = null;

    public Ngram Current => _current!.Value;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}
