using System.Text;
using Lingua.Api;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Lingua.AccuracyReport.Tests;

// ReSharper disable ClassNeverInstantiated.Global
/// <summary>
/// Collects statistics for language detection for a given language.
/// </summary>
public class LanguageDetectionStatistics(IMessageSink messageSink) : IDisposable
{
	private static readonly Lazy<string> LazyRoot = new(FindSolutionRoot);
	private static string Root => LazyRoot.Value;

	private static readonly Language[] Languages = LanguageInfo.All().ToArray();

	private static readonly Lazy<LanguageDetector> LinguaLanguageDetectorWithLowAccuracy = new(() =>
		LanguageDetectorBuilder.FromLanguages(Languages)
			.WithLowAccuracyMode()
			.Build());

	private static readonly Lazy<LanguageDetector> LinguaLanguageDetectorWithHighAccuracy = new(() =>
		LanguageDetectorBuilder.FromLanguages(Languages)
			.Build());

	private readonly Dictionary<Language, (int, int)> _singleWordsStatistics = new();
	private readonly Dictionary<Language, (int, int)> _wordPairsStatistics = new();
	private readonly Dictionary<Language, (int, int)> _sentencesStatistics = new();
	private int _wordCount;
	private int _wordPairCount;
	private int _sentenceCount;
	private int _wordLengthCount;
	private int _wordPairLengthCount;
	private int _sentenceLengthCount;

	public Language Language { get; set; }
	public Implementation Implementation { get; set; }

	private static string FindSolutionRoot()
	{
		var linguaSln = "Lingua.sln";
		var startDir = Directory.GetCurrentDirectory();
		var currentDirectory = new DirectoryInfo(startDir);
		do
		{
			if (File.Exists(Path.Combine(currentDirectory.FullName, linguaSln)))
				return currentDirectory.FullName;

			currentDirectory = currentDirectory.Parent;
		} while (currentDirectory != null);

		throw new InvalidOperationException(
			$"Could not find solution root directory from the current directory {startDir}");
	}

	public void Dispose()
	{
		var accuracyReportsDirectoryPath = Path.Combine(
			Root,
			"accuracy-reports",
			Implementation.ToString().ToLowerInvariant()
		);
		var accuracyReportFilePath = Path.Combine(
			accuracyReportsDirectoryPath,
			$"{Language}.txt"
		);

		var statisticsReport = StatisticsReport();
		messageSink.OnMessage(new DiagnosticMessage(statisticsReport));

		Directory.CreateDirectory(accuracyReportsDirectoryPath);
		File.WriteAllText(accuracyReportFilePath, statisticsReport);
	}
	private string StatisticsReport()
	{
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

		var averageAccuracyReport = Implementation == Implementation.Lingua
			? $">>> Accuracy on average: {FormatAccuracy(averageAccuracyInLowAccuracyMode)} | " +
			  FormatAccuracy(averageAccuracyInHighAccuracyMode)
			: $">>> Accuracy on average: {FormatAccuracy(averageAccuracyInHighAccuracyMode)}";

		var reportParts = new[]
		{
			averageAccuracyReport,
			singleWordAccuracyReport,
			wordPairAccuracyReport,
			sentenceAccuracyReport
		};
		var newlines = new string('\n', 2);
		var report = new StringBuilder($"##### {Language} #####");

		if (Implementation == Implementation.Lingua)
		{
			report.Append(newlines);
			report.Append("Legend: 'low accuracy mode | high accuracy mode'");
		}

		foreach (var reportPart in reportParts)
		{
			if (!string.IsNullOrEmpty(reportPart))
				report.Append($"{newlines}{reportPart}");
		}

		report.Append($"{newlines}>> Exact values:");
		if (Implementation == Implementation.Lingua)
		{
			report.Append($" {averageAccuracyInLowAccuracyMode} {singleWordAccuracies.LowAccuracy} " +
			              $"{wordPairAccuracies.LowAccuracy} {sentenceAccuracies.LowAccuracy}");
			report.Append($" {averageAccuracyInHighAccuracyMode} {singleWordAccuracies.HighAccuracy} " +
			              $"{wordPairAccuracies.HighAccuracy} {sentenceAccuracies.HighAccuracy}");
		}
		else
		{
			report.Append($" {averageAccuracyInHighAccuracyMode} {singleWordAccuracies.HighAccuracy} " +
			              $"{wordPairAccuracies.HighAccuracy} {sentenceAccuracies.HighAccuracy}");
		}

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

	private void ComputeStatistics(Dictionary<Language, (int, int)> statistics, string element)
	{
		var detectedLanguages = (Language.Unknown, Language.Unknown);
		switch (Implementation)
		{
			case Implementation.Lingua:
			{
				var low = LinguaLanguageDetectorWithLowAccuracy.Value.DetectLanguageOf(element);
				var high = LinguaLanguageDetectorWithHighAccuracy.Value.DetectLanguageOf(element);
				detectedLanguages = (low, high);
				break;
			}
		};

		var languageInLowAccuracyMode = detectedLanguages.Item1;
		var languageInHighAccuracyMode = detectedLanguages.Item2;

		var languageCounts = statistics.GetValueOrDefault(languageInLowAccuracyMode, (0,0));
		languageCounts = (languageCounts.Item1 + 1, languageCounts.Item2);
		statistics[languageInLowAccuracyMode] = languageCounts;

		languageCounts = statistics.GetValueOrDefault(languageInHighAccuracyMode, (0,0));
		languageCounts = (languageCounts.Item1, languageCounts.Item2 + 1);
		statistics[languageInHighAccuracyMode] = languageCounts;
	}

	private static Dictionary<Language, (double LowAccuracy , double HighAccuracy)> MapCountsToAccuracies(Dictionary<Language, (int, int)> statistics)
	{
		var sumOfCountsInLowAccuracyMode = statistics.Values.Sum(pair => pair.Item1);
		var sumOfCountsInHighAccuracyMode = statistics.Values.Sum(pair => pair.Item2);

		return statistics.ToDictionary(
			kvp => kvp.Key,
			kvp =>
			{
				var lowAccuracy = ComputeAccuracy(kvp.Value.Item1, sumOfCountsInLowAccuracyMode);
				var highAccuracy = ComputeAccuracy(kvp.Value.Item2, sumOfCountsInHighAccuracyMode);
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
		var report = new StringBuilder(
			$">> Detection of {count} {description} (average length: {(int)((double)length / count)} chars)\n");

		if (Implementation == Implementation.Lingua)
			report.Append($"Accuracy: {FormatAccuracy(accuracies.Item1)} | {FormatAccuracy(accuracies.Item2)}\n");
		else
			report.Append($"Accuracy: {FormatAccuracy(accuracies.Item2)}\n");

		report.AppendLine("Erroneously classified as");
		report.Append(FormatStatistics(statistics, Language));

		return (accuracies, report.ToString());
	}

	private string FormatStatistics(Dictionary<Language,(double, double)> statistics, Language language)
	{
		var sorted = statistics
			.Where(s => s.Key != language)
			.OrderByDescending(s => s.Value.Item2)
			.ToList();

		var builder = new StringBuilder();
		if (Implementation == Implementation.Lingua)
		{
			foreach (var statistic in sorted)
			{
				builder.AppendLine(
					$"{statistic.Key}: {FormatAccuracy(statistic.Value.Item1)} | {FormatAccuracy(statistic.Value.Item2)}");
			}
		}
		else
		{
			foreach (var statistic in sorted)
			{
				builder.AppendLine($"{statistic.Key}: {FormatAccuracy(statistic.Value.Item2)}");
			}
		}

		return builder.ToString();
	}

	private static string FormatAccuracy(double value) => $"{value:0.00}%";
}
