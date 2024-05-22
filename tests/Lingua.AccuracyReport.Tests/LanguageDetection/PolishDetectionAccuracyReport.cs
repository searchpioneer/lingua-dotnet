namespace Lingua.AccuracyReport.Tests.LanguageDetection;

public class PolishDetectionAccuracyReport(LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory> statistics)
	: LanguageDetectionDetectionAccuracyReport(Polish, statistics),
		IClassFixture<LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.LanguageDetection, Polish)]
	[SingleWordData(Polish)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.LanguageDetection, Polish)]
	[WordPairsData(Polish)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.LanguageDetection, Polish)]
	[SentenceData(Polish)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
