namespace Lingua.AccuracyReport.Tests.Lingua;

public class PortugueseDetectionAccuracyReport(LanguageDetectionStatistics<LinguaLanguageDetectorFactory> statistics)
	: LinguaDetectionAccuracyReport(Portuguese, statistics),
		IClassFixture<LanguageDetectionStatistics<LinguaLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.Lingua, Portuguese)]
	[SingleWordData(Portuguese)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.Lingua, Portuguese)]
	[WordPairsData(Portuguese)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.Lingua, Portuguese)]
	[SentenceData(Portuguese)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
