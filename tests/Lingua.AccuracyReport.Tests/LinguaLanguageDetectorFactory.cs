using static Lingua.AccuracyReport.Tests.SupportedLanguages;

namespace Lingua.AccuracyReport.Tests;

public class LinguaLanguageDetectorFactory : ILanguageDetectorFactory
{
	public Implementation Implementation => Implementation.Lingua;
	public bool SupportsLowAccuracyMode => true;

	public (ILanguageDetector lowAccuracyDetector, ILanguageDetector highAccuracyDetector) Create()
	{
		var languageDetectorWithLowAccuracy = new LinguaLanguageDetector(
			LanguageDetectorBuilder.FromLanguages(GetLanguagesForTest(Implementation))
				.WithLowAccuracyMode()
				.Build());

		var languageDetectorWithHighAccuracy = new LinguaLanguageDetector(
			LanguageDetectorBuilder.FromLanguages(GetLanguagesForTest(Implementation))
				.Build());

		return (languageDetectorWithLowAccuracy, languageDetectorWithHighAccuracy);
	}
}
