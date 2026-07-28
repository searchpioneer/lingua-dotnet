using BenchmarkDotNet.Attributes;

namespace Lingua.Benchmarks;

/// <summary>
/// Computing confidence values scales with the number of languages the detector was built with,
/// so this benchmarks the whole supported set rather than a handful of languages.
/// </summary>
[ShortRunJob]
[MemoryDiagnoser]
public class ConfidenceValuesBenchmarks
{
	private const string GermanText =
		"""
		Weltweit    gibt es ungefähr 6.000 Sprachen,
		wobei laut Schätzungen zufolge ungefähr 90  Prozent davon
		am Ende dieses Jahrhunderts verdrängt sein werden.
		""";

	private LanguageDetector _detector = null!;

	[GlobalSetup]
	public void Setup() => _detector = LanguageDetectorBuilder
		.FromAllLanguages()
		.WithPreloadedLanguageModels()
		.Build();

	[Benchmark]
	public object ComputeLanguageConfidenceValues() => _detector.ComputeLanguageConfidenceValues(GermanText);
}
