using BenchmarkDotNet.Attributes;
using NTextCat;

namespace Lingua.Benchmarks.SingleWord;

public class VietnameseSingleWordDetection
{
	private LanguageDetector _linguaLanguageDetector;
	private LanguageDetector _lowAccuracyLinguaLanguageDetector;
	private LanguageDetection.LanguageDetector _languageDetectionLanguageDetector;
	private RankedLanguageIdentifier _nTextCatLanguageDetector;

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
		var nTextCatDetector = factory.Load(stream);
		_nTextCatLanguageDetector = nTextCatDetector;
	}

	[Benchmark(Baseline = true)]
	public Language LinguaLowAccuracy() => _lowAccuracyLinguaLanguageDetector.DetectLanguageOf(Text);

	[Benchmark]
	public Language Lingua() => _linguaLanguageDetector.DetectLanguageOf(Text);

	[Benchmark]
	public string LanguageDetection() => _languageDetectionLanguageDetector.Detect(Text);

	[Benchmark]
	public Tuple<NTextCat.LanguageInfo, double> NTextCat() => _nTextCatLanguageDetector.Identify(Text).First();

	[ParamsSource(nameof(ValuesForText))]
	public string Text { get; set; }

	public IEnumerable<string> ValuesForText => new[]
	{
		"cằm"
	};
}
