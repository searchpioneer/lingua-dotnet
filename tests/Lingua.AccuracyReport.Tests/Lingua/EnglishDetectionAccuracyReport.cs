using Xunit;
using static Lingua.Api.Language;

namespace Lingua.AccuracyReport.Tests.Lingua;

public class EnglishDetectionAccuracyReport(LanguageDetectionStatistics statistics)
	: AbstractLanguageDetectionAccuracyReport(English, Implementation.Lingua, statistics),
		IClassFixture<LanguageDetectionStatistics>
{
	[Theory(DisplayName = "single word detection")]
	[SingleWordData(English)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[Theory(DisplayName = "word pair detection")]
	[WordPairsData(English)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[Theory(DisplayName = "sentence detection")]
	[SentenceData(English)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
