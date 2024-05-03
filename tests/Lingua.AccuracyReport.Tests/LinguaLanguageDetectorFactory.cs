namespace Lingua.AccuracyReport.Tests;

public class LinguaLanguageDetectorFactory : ILanguageDetectorFactory
{
	public Implementation Implementation => Implementation.Lingua;
	public bool SupportsLowAccuracyMode => true;

	public (ILanguageDetector lowAccuracyDetector, ILanguageDetector highAccuracyDetector) Create()
	{
		var languageDetectorWithLowAccuracy = new LinguaLanguageDetector(LanguageDetectorBuilder.FromAllLanguages()
			.WithLowAccuracyMode()
			.Build());

		var languageDetectorWithHighAccuracy = new LinguaLanguageDetector(LanguageDetectorBuilder.FromAllLanguages()
			.Build());

		return (languageDetectorWithLowAccuracy, languageDetectorWithHighAccuracy);
	}
}
