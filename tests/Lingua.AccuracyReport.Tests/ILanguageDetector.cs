namespace Lingua.AccuracyReport.Tests;

/// <summary>
/// Adapter for testing language detector implementations
/// </summary>
public interface ILanguageDetector
{
	public Language DetectLanguageOf(string text, Language expectedLanguage);
}
