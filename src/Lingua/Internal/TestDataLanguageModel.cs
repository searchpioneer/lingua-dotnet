using System.Text.RegularExpressions;

namespace Lingua.Internal;

internal readonly partial record struct TestDataLanguageModel(HashSet<Ngram> Ngrams)
{
	[GeneratedRegex("^\\p{L}+$")]
	private static partial Regex LetterRegex();

	public static TestDataLanguageModel FromText(string text, int ngramLength)
	{
		if (ngramLength is < 1 or > 5)
			throw new ArgumentOutOfRangeException(nameof(ngramLength), $"ngram length {ngramLength} is not in range 1..5");

		var ngrams = new HashSet<Ngram>();
		var textSpan = text.AsSpan();
		for (var i = 0; i <= text.Length - ngramLength; i++)
		{
			var textSlice = textSpan.Slice(i, ngramLength);
			if (LetterRegex().IsMatch(textSlice))
				ngrams.Add(new Ngram(textSlice.ToString()));
		}

		return new(ngrams);
	}
}
