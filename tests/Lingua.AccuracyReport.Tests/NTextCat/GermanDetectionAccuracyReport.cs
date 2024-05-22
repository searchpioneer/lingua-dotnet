using Lingua.AccuracyReport.Tests.Lingua;

namespace Lingua.AccuracyReport.Tests.NTextCat;

public class GermanDetectionAccuracyReport(LanguageDetectionStatistics<NTextCatLanguageDetectorFactory> statistics)
	: NTextCatDetectionAccuracyReport(German, statistics),
		IClassFixture<LanguageDetectionStatistics<NTextCatLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.NTextCat, German)]
	[SingleWordData(German)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.NTextCat, German)]
	[WordPairsData(German)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.NTextCat, German)]
	[SentenceData(German)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
