namespace Lingua.AccuracyReport.Tests;

public class LanguageDetectionLanguageDetector(global::LanguageDetection.LanguageDetector detector) : ILanguageDetector
{
	public Language DetectLanguageOf(string text, Language expectedLanguage)
	{
		var iso6393 = detector.Detect(text);

		switch (iso6393)
		{
			case null:
				return Language.Unknown;
			// LanguageDetection only supports Norwegian, so convert to two written forms supported by Lingua
			case "nor":
				// Default to Bokmal, unless Nynorsk is expected
				iso6393 = expectedLanguage == Language.Nynorsk ? "nno" : "nob";
				break;
		}

		return Enum.TryParse<IsoCode6393>(iso6393, true, out var result)
				? LanguageInfo.GetByIsoCode6393(result)
				: Language.Unknown;
	}
}
