namespace Lingua.AccuracyReport.Tests;

public static class SupportedLanguages
{
	static SupportedLanguages()
	{
		var languageIsoCodesSupportedByLanguageDetection = typeof(global::LanguageDetection.LanguageDetector).Assembly
			.GetManifestResourceNames()
			.Select(r => r[(r.LastIndexOf('.') + 1)..])
			.ToList();

		var languagesSupportedByLanguageDetection = new HashSet<Language>();
		foreach (var isoCode in languageIsoCodesSupportedByLanguageDetection)
		{
			if (Enum.TryParse<IsoCode6393>(isoCode, true, out var result))
				languagesSupportedByLanguageDetection.Add(LanguageInfo.GetByIsoCode6393(result));
			else if (isoCode == "nor")
			{
				languagesSupportedByLanguageDetection.Add(Nynorsk);
				languagesSupportedByLanguageDetection.Add(Bokmal);
			}
			else
			{
				var foo = isoCode;
			}
		}

		var languageDetection = languagesSupportedByLanguageDetection.ToArray();

		Language[] ntextCat =
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
			// substitutes for Norwegian (nor)
			Bokmal,
			Nynorsk,
		];

		LanguagesByImplementation = new Dictionary<Implementation, Language[]>
		{
			[Implementation.Lingua] = LanguageInfo.All().ToArray(),
			[Implementation.LanguageDetection] = languageDetection,
			[Implementation.NTextCat] = ntextCat
		};
	}

	/// <summary>
	/// Languages supported by each of the given implementations
	/// </summary>
	public static readonly IReadOnlyDictionary<Implementation, Language[]> LanguagesByImplementation;

	private static Lazy<string?> TestCompareEnvironmentVariable => new(() =>
		Environment.GetEnvironmentVariable("TEST_COMPARE"));

	private static Lazy<string?> TestFilterEnvironmentVariable => new(() =>
		Environment.GetEnvironmentVariable("TEST_FILTER"));

	/// <summary>
	/// Gets the languages to use for the test. When the environment variable
	/// <c>TEST_COMPARE</c> is present, all detectors use the same subset of languages supported
	/// by all detectors, to provide a fair detection probability comparison. As a result, accuracy reports
	/// for languages not supported by all detectors will be skipped. When the environment variable is not
	/// present, detectors will use all the languages that they support.
	/// </summary>
	public static Language[] GetLanguagesForTest(Implementation implementation)
	{
		var testCompare = TestCompareEnvironmentVariable.Value;
		return string.IsNullOrEmpty(testCompare)
			? LanguagesByImplementation[implementation]
			: LanguagesByTestedImplementations.Value;
	}

	private static Lazy<Language[]> LanguagesByTestedImplementations => new(() =>
	{
		IEnumerable<Language> languages = new List<Language>(LanguagesByImplementation[Implementation.Lingua]);
		var testFilter = TestFilterEnvironmentVariable.Value;
		if (!string.IsNullOrEmpty(testFilter))
		{
			// run with the subset of languages supported by the implementations in the filter
			languages = Enum.GetValues<Implementation>()
				.Where(implementation => testFilter.Contains($"(FullyQualifiedName~.{implementation})"))
				.Aggregate(languages, (current, implementation) =>
					current.Intersect(LanguagesByImplementation[implementation]));
		}
		else
		{
			// run just with the subset of languages supported by all
			languages = LanguagesByImplementation.Values.Aggregate(languages, (current, value) =>
				current.Intersect(value));
		}

		return languages.ToArray();
	});
}
