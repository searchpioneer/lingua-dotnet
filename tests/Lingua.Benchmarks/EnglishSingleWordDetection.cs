using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using NTextCat;

namespace Lingua.Benchmarks;

public class EnglishSingleWordDetection
{
	private readonly LanguageDetector _linguaLanguageDetector;
	private readonly LanguageDetector _lowAccuracyLinguaLanguageDetector;
	private readonly LanguageDetection.LanguageDetector _languageDetectionLanguageDetector;
	private readonly RankedLanguageIdentifier _nTextCatLanguageDetector;

	public EnglishSingleWordDetection()
	{
		var languages = SupportedLanguages.ByAllImplementations;

		var detector = new LanguageDetection.LanguageDetector();
		detector.AddLanguages(languages.Select(l => l.IsoCode6393().ToString().ToLowerInvariant()).ToArray());
		_languageDetectionLanguageDetector = detector;

		_linguaLanguageDetector = LanguageDetectorBuilder
			.FromLanguages(languages)
			.WithPreloadedLanguageModels()
			.Build();

		_lowAccuracyLinguaLanguageDetector = LanguageDetectorBuilder
			.FromLanguages(languages)
			.WithPreloadedLanguageModels()
			.WithLowAccuracyMode()
			.Build();

		var factory = new RankedLanguageIdentifierFactory();
		using var stream = typeof(EnglishSingleWordDetection).Assembly
			.GetManifestResourceStream("Lingua.Benchmarks.Core14.profile.xml");
		var nTextCatDetector = factory.Load(stream);
		_nTextCatLanguageDetector = nTextCatDetector;
	}

	[Params("suspiciously")]
	// ReSharper disable once UnassignedField.Global
	public string? Text;

	[Benchmark(Baseline = true, Description = "Lingua Low Accuracy")]
	public Language LinguaLowAccuracy() => _lowAccuracyLinguaLanguageDetector.DetectLanguageOf(Text!);

	[Benchmark]
	public Language Lingua() => _linguaLanguageDetector.DetectLanguageOf(Text!);

	[Benchmark]
	public string LanguageDetection() => _languageDetectionLanguageDetector.Detect(Text!);

	[Benchmark]
	public Tuple<NTextCat.LanguageInfo, double> NTextCat() => _nTextCatLanguageDetector.Identify(Text!).First();
}
