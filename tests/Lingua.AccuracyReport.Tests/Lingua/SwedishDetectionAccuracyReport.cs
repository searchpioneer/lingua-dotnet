using Xunit;
using static Lingua.Api.Language;

namespace Lingua.AccuracyReport.Tests.Lingua;

public class SwedishDetectionAccuracyReport(LanguageDetectionStatistics statistics)
	: AbstractLanguageDetectionAccuracyReport(Swedish, Implementation.Lingua, statistics),
		IClassFixture<LanguageDetectionStatistics>
{
	[Theory(DisplayName = "single word detection")]
	[SingleWordData(Swedish)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[Theory(DisplayName = "word pair detection")]
	[WordPairsData(Swedish)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[Theory(DisplayName = "sentence detection")]
	[SentenceData(Swedish)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
