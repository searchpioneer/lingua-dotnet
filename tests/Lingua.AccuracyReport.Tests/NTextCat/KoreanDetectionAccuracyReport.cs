using Lingua.AccuracyReport.Tests.Lingua;

namespace Lingua.AccuracyReport.Tests.NTextCat;

public class KoreanDetectionAccuracyReport(LanguageDetectionStatistics<NTextCatLanguageDetectorFactory> statistics)
	: NTextCatDetectionAccuracyReport(Korean, statistics),
		IClassFixture<LanguageDetectionStatistics<NTextCatLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.NTextCat, Korean)]
	[SingleWordData(Korean)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.NTextCat, Korean)]
	[WordPairsData(Korean)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.NTextCat, Korean)]
	[SentenceData(Korean)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
