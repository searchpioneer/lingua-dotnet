using Lingua.AccuracyReport.Tests.Lingua;

namespace Lingua.AccuracyReport.Tests.NTextCat;

public class JapaneseDetectionAccuracyReport(LanguageDetectionStatistics<NTextCatLanguageDetectorFactory> statistics)
	: NTextCatDetectionAccuracyReport(Japanese, statistics),
		IClassFixture<LanguageDetectionStatistics<NTextCatLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.NTextCat, Japanese)]
	[SingleWordData(Japanese)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.NTextCat, Japanese)]
	[WordPairsData(Japanese)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.NTextCat, Japanese)]
	[SentenceData(Japanese)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
