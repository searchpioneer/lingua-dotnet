using BenchmarkDotNet.Attributes;
using Lingua.Internal;

namespace Lingua.Benchmarks;

[ShortRunJob]
[MemoryDiagnoser]
public class TestLanguageModelBenchmarks
{
	private const string CleanedText =
		"weltweit gibt es ungefähr sprachen wobei laut schätzungen zufolge ungefähr " +
		"prozent davon am ende dieses jahrhunderts verdrängt sein werden";

	[Params(1, 3, 5)]
	public int NgramLength { get; set; }

	[Benchmark]
	public object FromText() => TestLanguageModel.FromText(CleanedText, NgramLength);
}
