using FluentAssertions;
using Lingua.Internal;
using Xunit;

namespace Lingua.Tests;

public class NgramTests
{
	private static readonly Ngram Zerogram = new("");
	private static readonly Ngram Unigram = new("q");
	private static readonly Ngram Bigram = new("qw");
	private static readonly Ngram Trigram = new("qwe");
	private static readonly Ngram Quadrigram = new("qwer");
	private static readonly Ngram Fivegram = new("qwert");

	[Fact]
	public void ToStringReturnsValue()
	{
		Fivegram.ToString().Should().Be("qwert");
		Quadrigram.ToString().Should().Be("qwer");
		Trigram.ToString().Should().Be("qwe");
		Bigram.ToString().Should().Be("qw");
		Unigram.ToString().Should().Be("q");
		Zerogram.ToString().Should().Be("");
	}

	[Fact]
	public void FivegramsShouldDecrementCorrectly()
	{
		var count = 5;
		foreach (var orderedNgram in Fivegram.LowerOrderNGrams())
		{
			switch (count)
			{
				case 5:
					Assert.Equal(Fivegram.AsSpan(), orderedNgram);
					break;
				case 4:
					Assert.Equal(Quadrigram.AsSpan(), orderedNgram);
					break;
				case 3:
					Assert.Equal(Trigram.AsSpan(), orderedNgram);
					break;
				case 2:
					Assert.Equal(Bigram.AsSpan(), orderedNgram);
					break;
				case 1:
					Assert.Equal(Unigram.AsSpan(), orderedNgram);
					break;
				case 0:
					Assert.Equal(Zerogram.AsSpan(), orderedNgram);
					break;
				default:
					Assert.Fail("Enumerated passed the end value");
					break;
			}
			count--;
		}
	}

	[Fact]
	public void EnumeratorWorksCorrectly()
	{
		var enumerator = new NgramEnumerator(Fivegram);

		enumerator.MoveNext();
		Assert.Equal(Fivegram.AsSpan(), enumerator.Current);
		enumerator.MoveNext();
		Assert.Equal(Quadrigram.AsSpan(), enumerator.Current);
		enumerator.MoveNext();
		Assert.Equal(Trigram.AsSpan(), enumerator.Current);
		enumerator.MoveNext();
		Assert.Equal(Bigram.AsSpan(), enumerator.Current);
		enumerator.MoveNext();
		Assert.Equal(Unigram.AsSpan(), enumerator.Current);
		enumerator.MoveNext();
		Assert.Equal(Zerogram.AsSpan(), enumerator.Current);

		enumerator.MoveNext().Should().BeFalse();
		enumerator.MoveNext().Should().BeFalse();
	}
}
