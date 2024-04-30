using FluentAssertions;
using Lingua.Internal;
using Xunit;

namespace Lingua.Tests;

public class NgramTests
{
	private static readonly Ngram Zerogram = Ngram.Zerogram;
	private static readonly Ngram Unigram = new("q");
	private static readonly Ngram Bigram = new("qw");
	private static readonly Ngram Trigram = new("qwe");
	private static readonly Ngram Quadrigram = new("qwer");
	private static readonly Ngram Fivegram = new("qwert");
	private static readonly List<Ngram> Ngrams = [Unigram, Bigram, Trigram, Quadrigram, Fivegram];

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
		var quadrigram = Fivegram.Dec();
		quadrigram.Should().Be(Quadrigram);

		var trigram = quadrigram.Dec();
		trigram.Should().Be(Trigram);

		var bigram = trigram.Dec();
		bigram.Should().Be(Bigram);

		var unigram = bigram.Dec();
		unigram.Should().Be(Unigram);

		var zerogram = unigram.Dec();
		zerogram.Should().Be(Zerogram);

		var exception = Assert.Throws<InvalidOperationException>(() => zerogram.Dec());
		exception.Message.Should().Be("Zerogram is ngram type of lowest order and can not be decremented");
	}

	[Fact]
	public void EnumeratorWorksCorrectly()
	{
		var enumerator = new NgramEnumerator(Fivegram);

		enumerator.MoveNext();
		enumerator.Current.Should().Be(Fivegram);
		enumerator.MoveNext();
		enumerator.Current.Should().Be(Quadrigram);
		enumerator.MoveNext();
		enumerator.Current.Should().Be(Trigram);
		enumerator.MoveNext();
		enumerator.Current.Should().Be(Bigram);
		enumerator.MoveNext();
		enumerator.Current.Should().Be(Unigram);
		enumerator.MoveNext();
		enumerator.Current.Should().Be(Zerogram);

		enumerator.MoveNext().Should().BeFalse();
		enumerator.MoveNext().Should().BeFalse();
	}
}
