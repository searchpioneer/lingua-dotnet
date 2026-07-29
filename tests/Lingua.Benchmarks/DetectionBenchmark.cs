using BenchmarkDotNet.Attributes;
using NTextCat;

namespace Lingua.Benchmarks;

/// <summary>
/// Compares Lingua against other .NET language detection libraries, over the languages supported
/// by all of them. Derived classes supply the text to detect through <see cref="ValuesForText"/>.
/// </summary>
public abstract class DetectionBenchmark
{
	private LanguageDetector _linguaLanguageDetector = null!;
	private LanguageDetector _lowAccuracyLinguaLanguageDetector = null!;
	private LanguageDetection.LanguageDetector _languageDetectionLanguageDetector = null!;
	private RankedLanguageIdentifier _nTextCatLanguageDetector = null!;

	[GlobalSetup]
	public void GlobalSetup()
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
		using var stream = typeof(Program).Assembly
			.GetManifestResourceStream("Lingua.Benchmarks.Core14.profile.xml");
		_nTextCatLanguageDetector = factory.Load(stream);
	}

	[Benchmark(Baseline = true)]
	public Language LinguaLowAccuracy() => _lowAccuracyLinguaLanguageDetector.DetectLanguageOf(Text);

	[Benchmark]
	public Language Lingua() => _linguaLanguageDetector.DetectLanguageOf(Text);

	[Benchmark]
	public string? LanguageDetection() => _languageDetectionLanguageDetector.Detect(Text);

	[Benchmark]
	public Tuple<NTextCat.LanguageInfo, double> NTextCat() => _nTextCatLanguageDetector.Identify(Text).First();

	[ParamsSource(nameof(ValuesForText))]
	public string Text { get; set; } = null!;

	public abstract IEnumerable<string> ValuesForText { get; }
}
