namespace Lingua.AccuracyReport.Tests.Lingua;

public class SinhalaDetectionAccuracyReport(LanguageDetectionStatistics<LinguaLanguageDetectorFactory> statistics)
	: LinguaDetectionAccuracyReport(Sinhala, statistics),
		IClassFixture<LanguageDetectionStatistics<LinguaLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.Lingua, Sinhala)]
	[SingleWordData(Sinhala)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.Lingua, Sinhala)]
	[WordPairsData(Sinhala)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.Lingua, Sinhala)]
	[SentenceData(Sinhala)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
