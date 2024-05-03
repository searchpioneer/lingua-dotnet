namespace Lingua.AccuracyReport.Tests;

public interface ILanguageDetectorFactory
{
	Implementation Implementation { get; }

	bool SupportsLowAccuracyMode { get; }

	(ILanguageDetector lowAccuracyDetector, ILanguageDetector highAccuracyDetector) Create();
}
