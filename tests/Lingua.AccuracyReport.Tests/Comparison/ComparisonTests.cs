using System.Runtime.InteropServices;
using FluentAssertions;
using Xunit;
using static Lingua.Language;
namespace Lingua.AccuracyReport.Tests.Comparison;

public class ComparisonTests
{
	private readonly LanguageDetector _linguaDetector;
	private readonly LanguageDetection.LanguageDetector _languageDetectionDetector;

	public ComparisonTests()
	{
		_linguaDetector = LanguageDetectorBuilder.FromAllLanguages().WithPreloadedLanguageModels().Build();
		_languageDetectionDetector = new LanguageDetection.LanguageDetector();
		_languageDetectionDetector.AddAllLanguages();
	}

	[Theory(Skip = "This test exists to find text for benchmark comparisons")]
	[InlineData("ialomiţa", Romanian)]
	[InlineData("podĺa", Slovak)]
	[InlineData("pohľade", Slovak)]
	[InlineData("mŕtvych", Slovak)]
	[InlineData("ґрунтовому", Ukrainian)]
	[InlineData("пропонує", Ukrainian)]
	[InlineData("пристрої", Ukrainian)]
	[InlineData("cằm", Vietnamese)]
	public void DetectorsProduceTheSameResult(string text, Language language)
	{
		var linguaDetected = _linguaDetector.DetectLanguageOf(text);
		linguaDetected.Should().Be(language);

		var languageDetectionDetected = _languageDetectionDetector.Detect(text);
		languageDetectionDetected.Should().Be(language.IsoCode6393().ToString().ToLowerInvariant());
	}
}
