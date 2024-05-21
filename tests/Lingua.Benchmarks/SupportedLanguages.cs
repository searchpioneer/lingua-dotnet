using static Lingua.Language;

namespace Lingua.Benchmarks;

public static class SupportedLanguages
{
	static SupportedLanguages()
	{
		var languagesSupportedByLanguageDetection = typeof(LanguageDetection.LanguageDetector).Assembly
			.GetManifestResourceNames()
			.Select(r => r[(r.LastIndexOf('.') + 1)..])
			.ToList();

		var languages = new List<Language>();
		foreach (var language in languagesSupportedByLanguageDetection)
		{
			if (Enum.TryParse<IsoCode6393>(language, true, out var result))
				languages.Add(LanguageInfo.GetByIsoCode6393(result));
		}

		ByLanguageDetectionLibrary = languages.ToArray();

		ByNTextCat =
		[
			Danish,
			German,
			English,
			French,
			Italian,
			Japanese,
			Korean,
			Dutch,
			Portuguese,
			Russian,
			Spanish,
			Swedish,
			Chinese,
		];

		ByAllImplementations = LanguageInfo.All()
			.Intersect(ByLanguageDetectionLibrary)
			.Intersect(ByNTextCat)
			.ToArray();
	}

	public static readonly Language[] ByNTextCat;

	public static readonly Language[] ByLanguageDetectionLibrary;

	public static readonly Language[] ByAllImplementations;
}
