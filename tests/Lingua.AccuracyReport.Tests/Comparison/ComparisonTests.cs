using System.Runtime.InteropServices;
using FluentAssertions;

namespace Lingua.AccuracyReport.Tests.Comparison;

public class ComparisonTests
{
	private readonly LanguageDetector _linguaDetector;
	private readonly LanguageDetector _lowAccuracyLinguaDetector;
	private readonly global::LanguageDetection.LanguageDetector _languageDetectionDetector;

	public ComparisonTests()
	{
		_linguaDetector = LanguageDetectorBuilder.FromAllLanguages()
			.WithPreloadedLanguageModels()
			.Build();
		_lowAccuracyLinguaDetector = LanguageDetectorBuilder.FromAllLanguages()
			.WithLowAccuracyMode()
			.WithPreloadedLanguageModels()
			.Build();
		_languageDetectionDetector = new global::LanguageDetection.LanguageDetector();
		_languageDetectionDetector.AddAllLanguages();
	}

	[Theory(Skip = "This test exists to find text for benchmark comparisons")]
	[InlineData("ialomiţa", Romanian)]
	[InlineData("podĺa", Slovak)]
	[InlineData("ґрунтовому", Ukrainian)]
	[InlineData("cằm", Vietnamese)]
	[InlineData("suspiciously", English)]
	public void DetectorsProduceTheSameResult(string text, Language language)
	{
		var linguaDetected = _lowAccuracyLinguaDetector.DetectLanguageOf(text);
		linguaDetected.Should().Be(language);

		var languageDetectionDetected = _languageDetectionDetector.Detect(text);
		languageDetectionDetected.Should().Be(language.IsoCode6393().ToString().ToLowerInvariant());
	}
}
