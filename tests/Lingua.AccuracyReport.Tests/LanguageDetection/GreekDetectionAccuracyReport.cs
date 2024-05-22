namespace Lingua.AccuracyReport.Tests.LanguageDetection;

public class GreekDetectionAccuracyReport(LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory> statistics)
	: LanguageDetectionDetectionAccuracyReport(Greek, statistics),
		IClassFixture<LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.LanguageDetection, Greek)]
	[SingleWordData(Greek)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.LanguageDetection, Greek)]
	[WordPairsData(Greek)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.LanguageDetection, Greek)]
	[SentenceData(Greek)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
