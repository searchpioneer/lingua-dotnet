namespace Lingua.AccuracyReport.Tests.NTextCat;

public abstract class NTextCatDetectionAccuracyReport(
	Language language,
	LanguageDetectionStatistics<NTextCatLanguageDetectorFactory> statistics)
	: AbstractLanguageDetectionAccuracyReport<NTextCatLanguageDetectorFactory>(language, statistics);
