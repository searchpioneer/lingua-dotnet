namespace Lingua.AccuracyReport.Tests;

public class LinguaLanguageDetector(LanguageDetector detector) : ILanguageDetector
{
	public Language DetectLanguageOf(string text) => detector.DetectLanguageOf(text);
}
