using NTextCat;

namespace Lingua.AccuracyReport.Tests;

public class NTextCatLanguageDetector(RankedLanguageIdentifier rankedLanguageIdentifier) : ILanguageDetector
{
	public Language DetectLanguageOf(string text, Language expectedLanguage)
	{
		var values = rankedLanguageIdentifier.Identify(text).ToList();

		if (values.Count == 0)
			return Language.Unknown;

		// NTextCat only supports Norwegian, so convert to two written forms supported by Lingua
		var iso6393 = values[0].Item1.Iso639_3;
		if (iso6393 == "nor")
		{
			// Default to Bokmal, unless Nynorsk is expected
			iso6393 = expectedLanguage == Language.Nynorsk ? "nno" : "nob";
		}

		return Enum.TryParse<IsoCode6393>(iso6393, true, out var result)
			? LanguageInfo.GetByIsoCode6393(result)
			: Language.Unknown;
	}
}

public class NTextCatLanguageDetectorFactory : ILanguageDetectorFactory
{
	public Implementation Implementation => Implementation.NTextCat;
	public bool SupportsLowAccuracyMode => false;

	public (ILanguageDetector lowAccuracyDetector, ILanguageDetector highAccuracyDetector) Create()
	{
		var factory = new RankedLanguageIdentifierFactory();
		var detector = new NTextCatLanguageDetector(factory.Load(Path.Combine("NTextCat", "Core14.profile.xml")));

		return (detector, detector);
	}
}

