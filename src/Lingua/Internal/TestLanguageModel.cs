namespace Lingua.Internal;

internal readonly record struct TestLanguageModel(HashSet<Ngram> Ngrams)
{
	public static TestLanguageModel FromText(string text, int ngramLength)
	{
		if (ngramLength is < 1 or > 5)
			throw new ArgumentOutOfRangeException(nameof(ngramLength), $"ngram length {ngramLength} is not in range 1..5");

#if NET9_0_OR_GREATER
		var ngrams = new HashSet<Ngram>(NgramComparer.Instance);
		var lookup = ngrams.GetAlternateLookup<ReadOnlySpan<char>>();
#else
		var ngrams = new HashSet<Ngram>();
#endif
		var textSpan = text.AsSpan();
		var i = 0;
		while (i < textSpan.Length)
		{
			if (!char.IsLetter(textSpan[i]))
			{
				i++;
				continue;
			}

			var letterRunStart = i;
			while (i < textSpan.Length && char.IsLetter(textSpan[i]))
				i++;

			for (var start = letterRunStart; start + ngramLength <= i; start++)
			{
#if NET9_0_OR_GREATER
				lookup.Add(textSpan.Slice(start, ngramLength));
#else
				ngrams.Add(new Ngram(textSpan.Slice(start, ngramLength).ToString()));
#endif
			}
		}

		return new(ngrams);
	}

#if NET9_0_OR_GREATER
	// Allows ngram candidates to be checked for set membership as spans,
	// so that a string is only allocated for distinct ngrams
	private sealed class NgramComparer : IEqualityComparer<Ngram>, IAlternateEqualityComparer<ReadOnlySpan<char>, Ngram>
	{
		public static readonly NgramComparer Instance = new();

		public bool Equals(Ngram x, Ngram y) => x.Value == y.Value;

		public int GetHashCode(Ngram ngram) => ngram.Value.GetHashCode();

		public bool Equals(ReadOnlySpan<char> alternate, Ngram other) => alternate.SequenceEqual(other.Value);

		public int GetHashCode(ReadOnlySpan<char> alternate) => string.GetHashCode(alternate);

		public Ngram Create(ReadOnlySpan<char> alternate) => new(alternate.ToString());
	}
#endif
}
