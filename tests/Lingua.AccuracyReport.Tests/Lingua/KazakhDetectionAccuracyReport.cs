using Xunit;
using static Lingua.Language;

namespace Lingua.AccuracyReport.Tests.Lingua;

public class KazakhDetectionAccuracyReport(LanguageDetectionStatistics<LinguaLanguageDetectorFactory> statistics)
	: LinguaDetectionAccuracyReport(Kazakh, statistics),
		IClassFixture<LanguageDetectionStatistics<LinguaLanguageDetectorFactory>>
{
	[Theory(DisplayName = "single word detection")]
	[SingleWordData(Kazakh)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[Theory(DisplayName = "word pair detection")]
	[WordPairsData(Kazakh)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[Theory(DisplayName = "sentence detection")]
	[SentenceData(Kazakh)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
