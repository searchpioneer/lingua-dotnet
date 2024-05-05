using System.Text;

namespace Lingua.AccuracyReport.Tests;

// ReSharper disable ClassNeverInstantiated.Global
/// <summary>
/// Collects statistics for language detection for a given language.
/// </summary>
public class LanguageDetectionStatistics<TDetectorFactory> : IDisposable
	where TDetectorFactory : ILanguageDetectorFactory, new()
{
	private static readonly TDetectorFactory Factory;
	// intentional to have different detectors stored per closed generic type of TDetectorFactory
	// ReSharper disable StaticMemberInGenericType
	private static readonly ILanguageDetector LowAccuracyDetector;
	private static readonly ILanguageDetector HighAccuracyDetector;
	// ReSharper restore StaticMemberInGenericType

	private static string Implementation => Factory.Implementation.ToString();
	private static string ImplementationLowercase => Implementation.ToLowerInvariant();

	static LanguageDetectionStatistics()
	{
		Factory = new TDetectorFactory();
		(LowAccuracyDetector, HighAccuracyDetector) = Factory.Create();
	}

	private readonly Dictionary<Language, int[]> _singleWordsStatistics = new();
	private readonly Dictionary<Language, int[]> _wordPairsStatistics = new();
	private readonly Dictionary<Language, int[]> _sentencesStatistics = new();
	private int _wordCount;
	private int _wordPairCount;
	private int _sentenceCount;
	private int _wordLengthCount;
	private int _wordPairLengthCount;
	private int _sentenceLengthCount;

	public Language Language { get; set; }

	public void Dispose()
	{
		var accuracyReportsDirectoryPath = Path.Combine(
			SolutionPaths.Root,
			"accuracy-reports",
			Implementation
		);
		var accuracyReportFilePath = Path.Combine(
			accuracyReportsDirectoryPath,
			$"{Language}.md"
		);

		var statisticsReport = StatisticsReport();
		Directory.CreateDirectory(accuracyReportsDirectoryPath);
		File.WriteAllText(accuracyReportFilePath, statisticsReport);
	}

	private static string Anchor(string text, string id) => $"<a id=\"{id}\">{text}</a>";

	private string StatisticsReport()
	{
		var newlines = new string('\n', 2);
		var language = Language.ToString();
		var report = new StringBuilder($"## {Anchor(language, $"{ImplementationLowercase}-{language.ToLowerInvariant()}")}");

		var singleWordsAccuracyvalues = MapCountsToAccuracies(_singleWordsStatistics);
		var wordPairsAccuracyvalues = MapCountsToAccuracies(_wordPairsStatistics);
		var sentencesAccuracyvalues = MapCountsToAccuracies(_sentencesStatistics);

		var (singleWordAccuracies, singleWordAccuracyReport) = GetReportData(
			singleWordsAccuracyvalues,
			_wordCount,
			_wordLengthCount,
			"single words"
		);
		var (wordPairAccuracies, wordPairAccuracyReport) = GetReportData(
			wordPairsAccuracyvalues,
			_wordPairCount,
			_wordPairLengthCount,
			"word pairs"
		);
		var (sentenceAccuracies, sentenceAccuracyReport) = GetReportData(
			sentencesAccuracyvalues,
			_sentenceCount,
			_sentenceLengthCount,
			"sentences"
		);

		var averageAccuracyInLowAccuracyMode =
			(singleWordAccuracies.LowAccuracy + wordPairAccuracies.LowAccuracy + sentenceAccuracies.LowAccuracy) / 3;
		var averageAccuracyInHighAccuracyMode =
			(singleWordAccuracies.HighAccuracy + wordPairAccuracies.HighAccuracy + sentenceAccuracies.HighAccuracy) / 3;

		if (Factory.SupportsLowAccuracyMode)
		{
			report.AppendLine($"{newlines}Average accuracy");
			report.AppendLine();
			report.AppendLine("| Low Accuracy Mode | High Accuracy Mode |");
			report.AppendLine("| ----------------- | ------------------ |");
			report.AppendLine(
				$"| {FormatAccuracy(averageAccuracyInLowAccuracyMode)} | {FormatAccuracy(averageAccuracyInHighAccuracyMode)} |");
		}
		else
		{
			report.AppendLine($"{newlines}Average accuracy");
			report.AppendLine();
			report.AppendLine("| High Accuracy Mode |");
			report.AppendLine("| ------------------ |");
			report.AppendLine(
				$"| {FormatAccuracy(averageAccuracyInHighAccuracyMode)} |");
		}

		var reportParts = new[]
		{
			singleWordAccuracyReport,
			wordPairAccuracyReport,
			sentenceAccuracyReport
		};

		foreach (var reportPart in reportParts)
		{
			if (!string.IsNullOrEmpty(reportPart))
				report.Append($"{newlines}{reportPart}");
		}

		report.Append($"{newlines}<!-- Exact values:");
		if (Factory.SupportsLowAccuracyMode)
		{
			report.Append($" {averageAccuracyInLowAccuracyMode} {singleWordAccuracies.LowAccuracy} " +
						  $"{wordPairAccuracies.LowAccuracy} {sentenceAccuracies.LowAccuracy}");
			report.AppendLine($" {averageAccuracyInHighAccuracyMode} {singleWordAccuracies.HighAccuracy} " +
						  $"{wordPairAccuracies.HighAccuracy} {sentenceAccuracies.HighAccuracy} -->");
		}
		else
		{
			report.AppendLine($" {averageAccuracyInHighAccuracyMode} {singleWordAccuracies.HighAccuracy} " +
						  $"{wordPairAccuracies.HighAccuracy} {sentenceAccuracies.HighAccuracy} -->");
		}

		report.AppendLine();
		return report.ToString();
	}

	public void ComputeSingleWordStatistics(string singleWord)
	{
		ComputeStatistics(_singleWordsStatistics, singleWord);
		_wordCount++;
		_wordLengthCount += singleWord.Length;
	}

	public void ComputeWordPairStatistics(string wordPair)
	{
		ComputeStatistics(_wordPairsStatistics, wordPair);
		_wordPairCount++;
		_wordPairLengthCount += wordPair.Length;
	}

	public void ComputeSentenceStatistics(string sentence)
	{
		ComputeStatistics(_sentencesStatistics, sentence);
		_sentenceCount++;
		_sentenceLengthCount += sentence.Length;
	}

	private void ComputeStatistics(Dictionary<Language, int[]> statistics, string element)
	{
		var detectedLanguageInHighAccuracyMode = HighAccuracyDetector.DetectLanguageOf(element, Language);
		var detectedLanguageInLowAccuracyMode = Factory.SupportsLowAccuracyMode
			? LowAccuracyDetector.DetectLanguageOf(element, Language)
			: detectedLanguageInHighAccuracyMode;

		var languageCounts = statistics.GetValueOrDefault(detectedLanguageInLowAccuracyMode, [0, 0]);
		languageCounts[0]++;
		statistics[detectedLanguageInLowAccuracyMode] = languageCounts;

		languageCounts = statistics.GetValueOrDefault(detectedLanguageInHighAccuracyMode, [0, 0]);
		languageCounts[1]++;
		statistics[detectedLanguageInHighAccuracyMode] = languageCounts;
	}

	private static Dictionary<Language, (double LowAccuracy, double HighAccuracy)> MapCountsToAccuracies(Dictionary<Language, int[]> statistics)
	{
		var sumOfCountsInLowAccuracyMode = statistics.Values.Sum(values => values[0]);
		var sumOfCountsInHighAccuracyMode = statistics.Values.Sum(values => values[1]);

		return statistics.ToDictionary(
			kvp => kvp.Key,
			kvp =>
			{
				var lowAccuracy = ComputeAccuracy(kvp.Value[0], sumOfCountsInLowAccuracyMode);
				var highAccuracy = ComputeAccuracy(kvp.Value[1], sumOfCountsInHighAccuracyMode);
				return (lowAccuracy, highAccuracy);
			}
		);
	}

	private static double ComputeAccuracy(int languageCount, int totalLanguagesCount) =>
		(double)languageCount / totalLanguagesCount * 100;

	private ((double LowAccuracy, double HighAccuracy), string) GetReportData(
		Dictionary<Language, (double, double)> statistics,
		int count,
		int length,
		string description)
	{
		var accuracies = statistics.GetValueOrDefault(Language, (0d, 0d));
		var report = new StringBuilder($"### {Factory.Implementation}: {Language} {description}");
		report.AppendLine();
		report.AppendLine();
		report.AppendLine($"Detection of {count} {description} (average length: {(int)((double)length / count)} chars)");
		report.AppendLine();

		bool errors;
		if (Factory.SupportsLowAccuracyMode)
		{
			report.AppendLine("| Low Accuracy Mode | High Accuracy Mode |");
			report.AppendLine("| ----------------- | ------------------ |");
			report.AppendLine($"| {FormatAccuracy(accuracies.Item1)} | {FormatAccuracy(accuracies.Item2)} |");
			errors = accuracies.Item1 < 100 || accuracies.Item2 < 100;
		}
		else
		{
			report.AppendLine("| High Accuracy Mode |");
			report.AppendLine("| ------------------ |");
			report.AppendLine($"| {FormatAccuracy(accuracies.Item2)} |");
			errors = accuracies.Item2 < 100;
		}

		if (errors)
		{
			report.AppendLine();
			report.AppendLine("<details>");
			report.AppendLine("<summary>Error details</summary>");
			report.AppendLine();
			report.AppendLine("Erroneously classified as:");
			report.AppendLine();
			FormatStatistics(statistics, Language, report);
			report.AppendLine("</details>");
		}

		return (accuracies, report.ToString());
	}

	private static void FormatStatistics(Dictionary<Language, (double, double)> statistics, Language language,
		StringBuilder builder)
	{
		var sorted = statistics
			.Where(s => s.Key != language)
			.OrderByDescending(s => s.Value.Item2)
			.ToList();

		if (Factory.SupportsLowAccuracyMode)
		{
			builder.AppendLine("| Language | Low Accuracy Mode | High Accuracy Mode |");
			builder.AppendLine("| -------- | ----------------- | ------------------ |");
			foreach (var statistic in sorted)
			{
				builder.AppendLine(
					$"| {statistic.Key} | {FormatAccuracy(statistic.Value.Item1)} | {FormatAccuracy(statistic.Value.Item2)} |");
			}
		}
		else
		{
			builder.AppendLine("| Language | High Accuracy Mode |");
			builder.AppendLine("| -------- | ------------------ |");
			foreach (var statistic in sorted)
				builder.AppendLine($"| {statistic.Key} | {FormatAccuracy(statistic.Value.Item2)} |");
		}
	}

	private static string FormatAccuracy(double value) => $"{value:0.00}%";
}
