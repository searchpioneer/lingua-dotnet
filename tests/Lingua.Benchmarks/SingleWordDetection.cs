using BenchmarkDotNet.Attributes;

namespace Lingua.Benchmarks;

public class SingleWordDetection
{
	private readonly LanguageDetector _linguaLanguageDetector;
	private readonly LanguageDetection.LanguageDetector _languageDetectionLanguageDetector;

	public SingleWordDetection()
	{
		var detector = new LanguageDetection.LanguageDetector();
		detector.AddAllLanguages();
		_languageDetectionLanguageDetector = detector;

		var languagesSupportedByLanguageDetection = detector.GetType().Assembly.GetManifestResourceNames()
			.Select(r => r[(r.LastIndexOf('.') + 1)..])
			.ToList();

		// ensure that lingua is initialized with the same supported languages
		// **and** number of languages, for fairness
		var languages = new List<Language>();
		foreach (var language in languagesSupportedByLanguageDetection)
		{
			if (Enum.TryParse<IsoCode6393>(language, true, out var result))
				languages.Add(LanguageInfo.GetByIsoCode6393(result));
			else if (language == "nor")
			{
				// Norwegian languages
				languages.Add(Language.Nynorsk);
				languages.Add(Language.Bokmal);
			}
		}

		var languageSet = languages.ToHashSet();
		if (languageSet.Count < languagesSupportedByLanguageDetection.Count)
		{
			foreach (var language in LanguageInfo.All())
			{
				languageSet.Add(language);
				if (languageSet.Count == languagesSupportedByLanguageDetection.Count)
					break;
			}
		}

		_linguaLanguageDetector = LanguageDetectorBuilder
			.FromLanguages(languageSet.ToArray())
			.WithPreloadedLanguageModels()
			.Build();
	}

	[Params("ialomiţa", "pohľade", "ґрунтовому", "cằm")]
	// ReSharper disable once UnassignedField.Global
	public string? Text;

	[Benchmark(Baseline = true)]
	public Language Lingua() => _linguaLanguageDetector.DetectLanguageOf(Text!);

	[Benchmark]
	public string LanguageDetection() => _languageDetectionLanguageDetector.Detect(Text!);
}
