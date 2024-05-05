namespace Lingua.AccuracyReport.Tests;

public class LinguaLanguageDetector(LanguageDetector detector) : ILanguageDetector
{
	public Language DetectLanguageOf(string text, Language expectedLanguage) => detector.DetectLanguageOf(text);
}
