namespace Lingua.AccuracyReport.Tests;

public abstract class AbstractLanguageDetectionAccuracyReport
{
	private readonly LanguageDetectionStatistics _statistics;

	public abstract void SingleWordsAreIdentifiedCorrectly(string singleWord);
	public abstract void WordPairsAreIdentifiedCorrectly(string wordPair);
	public abstract void EntireSentencesAreIdentifiedCorrectly(string wordPair);

	protected AbstractLanguageDetectionAccuracyReport(
		Language language,
		Implementation implementation,
		LanguageDetectionStatistics statistics)
	{
		_statistics = statistics;
		_statistics.Language = language;
		_statistics.Implementation = implementation;
	}

	protected void ComputeSingleWordStatistics(string singleWord) =>
		_statistics.ComputeSingleWordStatistics(singleWord);

	protected void ComputeWordPairStatistics(string wordPair) =>
		_statistics.ComputeWordPairStatistics(wordPair);

	protected void ComputeSentenceStatistics(string sentence) =>
		_statistics.ComputeSentenceStatistics(sentence);
}
