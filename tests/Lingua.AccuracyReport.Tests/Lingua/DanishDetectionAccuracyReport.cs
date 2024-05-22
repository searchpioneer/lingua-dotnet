namespace Lingua.AccuracyReport.Tests.Lingua;

public class DanishDetectionAccuracyReport(LanguageDetectionStatistics<LinguaLanguageDetectorFactory> statistics)
	: LinguaDetectionAccuracyReport(Danish, statistics),
		IClassFixture<LanguageDetectionStatistics<LinguaLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.Lingua, Danish)]
	[SingleWordData(Danish)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.Lingua, Danish)]
	[WordPairsData(Danish)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.Lingua, Danish)]
	[SentenceData(Danish)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
