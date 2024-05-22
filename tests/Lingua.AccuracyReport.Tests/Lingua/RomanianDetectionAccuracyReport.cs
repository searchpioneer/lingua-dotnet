namespace Lingua.AccuracyReport.Tests.Lingua;

public class RomanianDetectionAccuracyReport(LanguageDetectionStatistics<LinguaLanguageDetectorFactory> statistics)
	: LinguaDetectionAccuracyReport(Romanian, statistics),
		IClassFixture<LanguageDetectionStatistics<LinguaLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.Lingua, Romanian)]
	[SingleWordData(Romanian)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.Lingua, Romanian)]
	[WordPairsData(Romanian)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.Lingua, Romanian)]
	[SentenceData(Romanian)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
