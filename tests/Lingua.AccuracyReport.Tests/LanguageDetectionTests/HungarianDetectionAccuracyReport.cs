using Xunit;
using static Lingua.Language;

namespace Lingua.AccuracyReport.Tests.LanguageDetectionTests;

public class HungarianDetectionAccuracyReport(LanguageDetectionStatistics statistics)
	: AbstractLanguageDetectionAccuracyReport(Hungarian, Implementation.LanguageDetection, statistics),
		IClassFixture<LanguageDetectionStatistics>
{
	[Theory(DisplayName = "single word detection")]
	[SingleWordData(Hungarian)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[Theory(DisplayName = "word pair detection")]
	[WordPairsData(Hungarian)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[Theory(DisplayName = "sentence detection")]
	[SentenceData(Hungarian)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}