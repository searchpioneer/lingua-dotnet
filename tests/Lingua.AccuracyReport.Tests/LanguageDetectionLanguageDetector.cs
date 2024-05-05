namespace Lingua.AccuracyReport.Tests;

public class LanguageDetectionLanguageDetector(global::LanguageDetection.LanguageDetector detector) : ILanguageDetector
{
	public Language DetectLanguageOf(string text)
	{
		var code = detector.Detect(text);
		return code is null
			? Language.Unknown
			: Enum.TryParse<IsoCode6393>(code, true, out var result)
				? LanguageInfo.GetByIsoCode6393(result)
				: Language.Unknown;
	}
}
