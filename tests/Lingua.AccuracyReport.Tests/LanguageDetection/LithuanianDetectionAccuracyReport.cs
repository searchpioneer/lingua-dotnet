namespace Lingua.AccuracyReport.Tests.LanguageDetection;

public class LithuanianDetectionAccuracyReport(LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory> statistics)
	: LanguageDetectionDetectionAccuracyReport(Lithuanian, statistics),
		IClassFixture<LanguageDetectionStatistics<LanguageDetectionLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.LanguageDetection, Lithuanian)]
	[SingleWordData(Lithuanian)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.LanguageDetection, Lithuanian)]
	[WordPairsData(Lithuanian)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.LanguageDetection, Lithuanian)]
	[SentenceData(Lithuanian)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
