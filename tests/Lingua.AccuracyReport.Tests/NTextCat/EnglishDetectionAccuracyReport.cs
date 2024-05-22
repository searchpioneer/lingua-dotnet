using Lingua.AccuracyReport.Tests.Lingua;

namespace Lingua.AccuracyReport.Tests.NTextCat;

public class EnglishDetectionAccuracyReport(LanguageDetectionStatistics<NTextCatLanguageDetectorFactory> statistics)
	: NTextCatDetectionAccuracyReport(English, statistics),
		IClassFixture<LanguageDetectionStatistics<NTextCatLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.NTextCat, English)]
	[SingleWordData(English)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.NTextCat, English)]
	[WordPairsData(English)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.NTextCat, English)]
	[SentenceData(English)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
