// ReSharper disable VirtualMemberCallInConstructor
namespace Lingua.AccuracyReport.Tests;

[AttributeUsage(AttributeTargets.Method)]
public class ReportTheoryAttribute : TheoryAttribute
{
	protected ReportTheoryAttribute(Implementation implementation, Language language)
	{
		if (!SupportedLanguages.GetLanguagesForTest(implementation).Contains(language))
			Skip = $"Filtered languages does not contain {language}";
	}
}

public class WordPairsReportTheoryAttribute : ReportTheoryAttribute
{
	public WordPairsReportTheoryAttribute(Implementation implementation, Language language)
		: base(implementation, language) =>
		DisplayName = $"{implementation} {language} word pair detection";
}

public class SingleWordReportTheoryAttribute : ReportTheoryAttribute
{
	public SingleWordReportTheoryAttribute(Implementation implementation, Language language)
		: base(implementation, language) =>
		DisplayName = $"{implementation} {language} single word detection";
}

public class SentenceReportTheoryAttribute : ReportTheoryAttribute
{
	public SentenceReportTheoryAttribute(Implementation implementation, Language language)
		: base(implementation, language) =>
		DisplayName = $"{implementation} {language} sentence detection";
}

