using BenchmarkDotNet.Attributes;
using Lingua.Internal;

namespace Lingua.Benchmarks;

[ShortRunJob]
[MemoryDiagnoser]
public class TokenizerBenchmarks
{
	[ParamsSource(nameof(Texts))]
	public NamedText Text { get; set; }

	public static IEnumerable<NamedText> Texts() =>
	[
		new("English", "I'm really not sure whether multi-language detection is a good idea. A little bit is better than nothing."),
		new("Chinese", "上海大学是一个好大学. 五、同业间良性的普遍推广与各团体的互助。"),
		new("Thai", "สตูล จัดพิธีมอบบ้านนักเรียน คืนความสุข สู่ลูก สพฐ."),
		new("Korean", "그러나 아름다움은 또한 아주 가까이 있다."),
		new("Mixed", "Parlez-vous français? Ich spreche Französisch nur ein bisschen. A little bit is better than nothing."),
		new("DigitsPunctuation", "12345 !!! 678,90 ??? (2024-05-23) [#42] {99%} $1.000.000"),
	];

	[Benchmark]
	public object TokensWithoutWhitespace() => Tokenizer.FindTokensWithoutWhitespace(Text.Value);

	[Benchmark]
	public object TokensWithOptionalWhitespace() => Tokenizer.FindTokensWithOptionalWhitespace(Text.Value);

	public readonly record struct NamedText(string Name, string Value)
	{
		public override string ToString() => Name;
	}
}

[ShortRunJob]
[MemoryDiagnoser]
public class DetectMultipleLanguagesBenchmarks
{
	private const string Sentence =
		"Parlez-vous français? Ich spreche Französisch nur ein bisschen. A little bit is better than nothing.";

	private LanguageDetector _detector = null!;

	[GlobalSetup]
	public void Setup() => _detector = LanguageDetectorBuilder
		.FromLanguages(Language.English, Language.French, Language.German)
		.WithPreloadedLanguageModels()
		.Build();

	[Benchmark]
	public object DetectMultipleLanguages() => _detector.DetectMultipleLanguagesOf(Sentence);
}
