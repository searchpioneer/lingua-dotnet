using FluentAssertions;
using Lingua.Internal;
using Xunit;

namespace Lingua.Tests;

public class TestLanguageModelTests
{
	[Theory]
	[InlineData(1, new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" })]
	[InlineData(2, new[] { "ab", "bc", "de", "fg", "gh", "hi", "ij" })]
	[InlineData(3, new[] { "abc", "fgh", "ghi", "hij" })]
	[InlineData(4, new[] { "fghi", "ghij" })]
	[InlineData(5, new[] { "fghij" })]
	public void NgramsAreOnlyCreatedFromRunsOfLetters(int ngramLength, string[] expectedNgrams) =>
		TestLanguageModel.FromText("abc, de 12 fghij!", ngramLength).Ngrams
			.Select(ngram => ngram.Value)
			.Should().BeEquivalentTo(expectedNgrams);

	[Fact]
	public void DuplicateNgramsAreOnlyIncludedOnce() =>
		TestLanguageModel.FromText("aa aa", 2).Ngrams
			.Select(ngram => ngram.Value)
			.Should().BeEquivalentTo("aa");

	[Fact]
	public void NgramsSpanningCombiningMarksAreExcluded() =>
		// U+0301 combining acute accent is a mark, not a letter, and splits the letter run
		TestLanguageModel.FromText("ab\u0301cd", 2).Ngrams
			.Select(ngram => ngram.Value)
			.Should().BeEquivalentTo("ab", "cd");

	[Theory]
	[InlineData(0)]
	[InlineData(6)]
	public void FromTextThrowsForNgramLengthOutOfRange(int ngramLength)
	{
		var exception = Assert.Throws<ArgumentOutOfRangeException>(() => TestLanguageModel.FromText("text", ngramLength));
		exception.ParamName.Should().Be("ngramLength");
	}
}
