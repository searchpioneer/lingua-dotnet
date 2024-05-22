using Lingua.AccuracyReport.Tests.Lingua;

namespace Lingua.AccuracyReport.Tests.NTextCat;

public class NynorskDetectionAccuracyReport(LanguageDetectionStatistics<NTextCatLanguageDetectorFactory> statistics)
	: NTextCatDetectionAccuracyReport(Nynorsk, statistics),
		IClassFixture<LanguageDetectionStatistics<NTextCatLanguageDetectorFactory>>
{
	[SingleWordReportTheory(Implementation.NTextCat, Nynorsk)]
	[SingleWordData(Nynorsk)]
	public override void SingleWordsAreIdentifiedCorrectly(string singleWord) =>
		ComputeSingleWordStatistics(singleWord);

	[WordPairsReportTheory(Implementation.NTextCat, Nynorsk)]
	[WordPairsData(Nynorsk)]
	public override void WordPairsAreIdentifiedCorrectly(string wordPair) =>
		ComputeWordPairStatistics(wordPair);

	[SentenceReportTheory(Implementation.NTextCat, Nynorsk)]
	[SentenceData(Nynorsk)]
	public override void EntireSentencesAreIdentifiedCorrectly(string sentence) =>
		ComputeSentenceStatistics(sentence);
}
