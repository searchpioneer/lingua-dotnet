namespace Lingua.AccuracyReport.Tests.LanguageDetection;

public class DanishDetectionAccuracyReport(LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory> statistics)
	: LanguageDetectionDetectionAccuracyReport(Danish, statistics),
		IClassFixture<LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.LanguageDetection, Danish)]
	[SingleWordData(Danish)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.LanguageDetection, Danish)]
	[WordPairsData(Danish)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.LanguageDetection, Danish)]
	[SentenceData(Danish)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
