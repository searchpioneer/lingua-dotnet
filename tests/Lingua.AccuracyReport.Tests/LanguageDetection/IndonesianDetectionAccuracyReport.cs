namespace Lingua.AccuracyReport.Tests.LanguageDetection;

public class IndonesianDetectionAccuracyReport(LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory> statistics)
	: LanguageDetectionDetectionAccuracyReport(Indonesian, statistics),
		IClassFixture<LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.LanguageDetection, Indonesian)]
	[SingleWordData(Indonesian)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.LanguageDetection, Indonesian)]
	[WordPairsData(Indonesian)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.LanguageDetection, Indonesian)]
	[SentenceData(Indonesian)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
