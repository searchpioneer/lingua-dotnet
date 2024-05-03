namespace Lingua.AccuracyReport.Tests.Lingua;

public abstract class LinguaDetectionAccuracyReport(
	Language language,
	LanguageDetectionStatistics<LinguaLanguageDetectorFactory> statistics)
	: AbstractLanguageDetectionAccuracyReport<LinguaLanguageDetectorFactory>(language, statistics);
