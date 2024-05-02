using Xunit;
using static Lingua.Api.Language;

namespace Lingua.AccuracyReport.Tests.Lingua;

public class TamilDetectionAccuracyReport(LanguageDetectionStatistics statistics)
	: AbstractLanguageDetectionAccuracyReport(Tamil, Implementation.Lingua, statistics),
		IClassFixture<LanguageDetectionStatistics>
{
	[Theory(DisplayName = "single word detection")]
	[SingleWordData(Tamil)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[Theory(DisplayName = "word pair detection")]
	[WordPairsData(Tamil)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[Theory(DisplayName = "sentence detection")]
	[SentenceData(Tamil)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
