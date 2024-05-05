namespace Lingua.AccuracyReport.Tests;

public class LanguageDetectionLanguageDetectorFactory : ILanguageDetectorFactory
{
	public Implementation Implementation => Implementation.LanguageDetection;
	public bool SupportsLowAccuracyMode => false;

	public (ILanguageDetector lowAccuracyDetector, ILanguageDetector highAccuracyDetector) Create()
	{
		var detector = new global::LanguageDetection.LanguageDetector();
		detector.AddAllLanguages();
		var languageDetectionLanguageDetector = new LanguageDetectionLanguageDetector(detector);
		return (languageDetectionLanguageDetector, languageDetectionLanguageDetector);
	}
}
