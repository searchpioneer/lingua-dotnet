using Xunit;
using static Lingua.Api.Language;

namespace Lingua.AccuracyReport.Tests.Lingua;

public class MarathiDetectionAccuracyReport(LanguageDetectionStatistics statistics)
	: AbstractLanguageDetectionAccuracyReport(Marathi, Implementation.Lingua, statistics),
		IClassFixture<LanguageDetectionStatistics>
{
	[Theory(DisplayName = "single word detection")]
	[SingleWordData(Marathi)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[Theory(DisplayName = "word pair detection")]
	[WordPairsData(Marathi)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[Theory(DisplayName = "sentence detection")]
	[SentenceData(Marathi)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
