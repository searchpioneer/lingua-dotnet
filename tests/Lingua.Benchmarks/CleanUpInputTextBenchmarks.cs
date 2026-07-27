using BenchmarkDotNet.Attributes;

namespace Lingua.Benchmarks;

[ShortRunJob]
[MemoryDiagnoser]
public class CleanUpInputTextBenchmarks
{
	private const string GermanText =
		"""
		Weltweit    gibt es ungefähr 6.000 Sprachen,
		wobei laut Schätzungen zufolge ungefähr 90  Prozent davon
		am Ende dieses Jahrhunderts verdrängt sein werden.
		""";

	private const string MixedText =
		"Parlez-vous français? Ich spreche Französisch nur ein bisschen. A little bit is better than nothing.";

	[Benchmark]
	public string CleanUpGermanText() => LanguageDetector.CleanUpInputText(GermanText);

	[Benchmark]
	public string CleanUpMixedText() => LanguageDetector.CleanUpInputText(MixedText);
}
