namespace Lingua.AccuracyReport.Tests;

public interface ILanguageDetector
{
	public Language DetectLanguageOf(string text, Language expectedLanguage);
}
