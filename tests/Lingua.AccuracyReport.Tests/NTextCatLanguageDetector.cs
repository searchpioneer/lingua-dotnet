using NTextCat;

namespace Lingua.AccuracyReport.Tests;

public class NTextCatLanguageDetector(RankedLanguageIdentifier rankedLanguageIdentifier) : ILanguageDetector
{
	public Language DetectLanguageOf(string text, Language expectedLanguage)
	{
		var values = rankedLanguageIdentifier.Identify(text).ToList();

		if (values.Count == 0)
			return Unknown;

		// NTextCat only supports Norwegian, so convert to two written forms supported by Lingua
		var iso6393 = values[0].Item1.Iso639_3;
		if (iso6393 == "nor")
		{
			// Default to Bokmal, unless Nynorsk is expected
			iso6393 = expectedLanguage == Nynorsk ? "nno" : "nob";
		}

		return Enum.TryParse<IsoCode6393>(iso6393, true, out var result)
			? LanguageInfo.GetByIsoCode6393(result)
			: Unknown;
	}
}
