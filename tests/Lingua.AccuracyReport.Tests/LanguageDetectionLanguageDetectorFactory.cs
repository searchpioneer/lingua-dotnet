namespace Lingua.AccuracyReport.Tests;

public class LanguageDetectionLanguageDetectorFactory : ILanguageDetectorFactory
{
	public Implementation Implementation => Implementation.LanguageDetection;
	public bool SupportsLowAccuracyMode => false;

	public (ILanguageDetector lowAccuracyDetector, ILanguageDetector highAccuracyDetector) Create()
	{
		var detector = new global::LanguageDetection.LanguageDetector();
		var isoCodes = SupportedLanguages.GetLanguagesForTest(Implementation)
			.Select(GetLanguageDetectionCompatibleIsoCode)
			.ToHashSet()
			.ToArray();

		detector.AddLanguages(isoCodes);
		var languageDetectionLanguageDetector = new LanguageDetectionLanguageDetector(detector);
		return (languageDetectionLanguageDetector, languageDetectionLanguageDetector);
	}

	private static string GetLanguageDetectionCompatibleIsoCode(Language language) =>
		language switch
		{
			Nynorsk => "nor",
			Bokmal => "nor",
			_ => language.IsoCode6393().ToString().ToLowerInvariant()
		};
}
