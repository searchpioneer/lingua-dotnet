using NTextCat;

namespace Lingua.AccuracyReport.Tests;

public class NTextCatLanguageDetectorFactory : ILanguageDetectorFactory
{
	public Implementation Implementation => Implementation.NTextCat;
	public bool SupportsLowAccuracyMode => false;

	public (ILanguageDetector lowAccuracyDetector, ILanguageDetector highAccuracyDetector) Create()
	{
		var factory = new RankedLanguageIdentifierFactory();
		var isoCodes = SupportedLanguages.GetLanguagesForTest(Implementation)
			.Select(GetNTextCatCompatibleIsoCode)
			.ToHashSet();
		var rankedLanguageIdentifier = factory.Load(Path.Combine("NTextCat", "Core14.profile.xml"),
			model => isoCodes.Contains(model.Language.Iso639_3));
		var detector = new NTextCatLanguageDetector(rankedLanguageIdentifier);

		return (detector, detector);
	}

	private static string GetNTextCatCompatibleIsoCode(Language language) =>
		language switch
		{
			Nynorsk => "nor",
			Bokmal => "nor",
			_ => language.IsoCode6393().ToString().ToLowerInvariant()
		};
}
