namespace Lingua.AccuracyReport.Tests;

/// <summary>
/// Adapter for testing language detector implementations
/// </summary>
public interface ILanguageDetector
{
	Language DetectLanguageOf(string text, Language expectedLanguage);
}
