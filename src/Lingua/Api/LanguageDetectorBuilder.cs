namespace Lingua.Api;

/// <summary>
/// Builder for <see cref="LanguageDetector"/>
/// </summary>
public class LanguageDetectorBuilder
{
	private readonly HashSet<Language> _languages;
	private double _minimumRelativeDistance;
	private bool _isEveryLanguageModelPreloaded;
	private bool _isLowAccuracyModeEnabled;

	private LanguageDetectorBuilder(HashSet<Language> languages) => _languages = languages;

	public static LanguageDetectorBuilder FromAllLanguages() =>
		new(LanguageExtensions.All().ToHashSet());

	public static LanguageDetectorBuilder FromAllSpokenLanguages() =>
		new(LanguageExtensions.AllSpokenOnes().ToHashSet());

	public static LanguageDetectorBuilder FromAllLanguagesWithArabicScript() =>
		new(LanguageExtensions.AllWithArabicScript().ToHashSet());

	public static LanguageDetectorBuilder FromAllLanguagesWithCyrillicScript() =>
		new(LanguageExtensions.AllWithCyrillicScript().ToHashSet());

	public static LanguageDetectorBuilder FromAllLanguagesWithDevangariScript() =>
		new(LanguageExtensions.AllWithDevangariScript().ToHashSet());

	public static LanguageDetectorBuilder FromAllLanguagesWithLatinScript() =>
		new(LanguageExtensions.AllWithLatinScript().ToHashSet());

	public static LanguageDetectorBuilder FromAllLanguagesExcept(params Language[] languages)
	{
		var languagesToLoad = Enum.GetValues<Language>().ToHashSet();
		languagesToLoad.RemoveWhere(language => language == Language.Unknown || languages.Contains(language));
		if (languagesToLoad.Count < 2)
			throw new ArgumentException("LanguageDetector needs at least 2 languages to choose from");

		return new LanguageDetectorBuilder(languagesToLoad);
	}

	public static LanguageDetectorBuilder FromLanguages(params Language[] languages)
	{
		var languagesToLoad = languages.ToHashSet();
		languagesToLoad.Remove(Language.Unknown);

		if (languagesToLoad.Count < 2)
			throw new ArgumentException("LanguageDetector needs at least 2 languages to choose from");

		return new LanguageDetectorBuilder(languagesToLoad);
	}

	/// <summary>
	/// Sets the desired value for the minimum relative distance measure.
	/// <para />
	/// By default, *Lingua* returns the most likely language for a given
	/// input text. However, there are certain words that are spelled the
	/// same in more than one language. The word *prologue*, for instance,
	/// is both a valid English and French word. Lingua would output either
	/// English or French which might be wrong in the given context.
	/// For cases like that, it is possible to specify a minimum relative
	/// distance that the logarithmized and summed up probabilities for
	/// each possible language have to satisfy.
	/// <para />
	/// Be aware that the distance between the language probabilities is
	/// dependent on the length of the input text. The longer the input
	/// text, the larger the distance between the languages. So if you
	/// want to classify very short text phrases, do not set the minimum
	/// relative distance too high. Otherwise you will get most results
	/// returned as <see cref="Language.Unknown"/> which is the return value for cases
	/// where language detection is not reliably possible.
	/// </summary>
	/// <param name="distance">distance A value between 0.0 and 0.99. Defaults to 0.0.</param>
	/// <exception cref="ArgumentException">if <paramref name="distance"/> is not between 0.0 and 0.99</exception>
	public LanguageDetectorBuilder WithMinimumRelativeDistance(double distance)
	{
		if (distance is < 0 or > 0.99)
			throw new ArgumentException("minimum relative distance must lie in between 0.0 and 0.99");

		_minimumRelativeDistance = distance;
		return this;
	}

	/// <summary>
	/// Preloads all language models when creating the instance of <see cref="LanguageDetector"/>.
	/// <para />
	/// By default, *Lingua* uses lazy-loading to load only those language models
	/// on demand which are considered relevant by the rule-based filter engine.
	/// For web services, for instance, it is rather beneficial to preload all language
	/// models into memory to avoid unexpected latency while waiting for the
	/// service response. This method allows to switch between these two loading modes.
	/// </summary>
	public LanguageDetectorBuilder WithPreloadedLanguageModels()
	{
		_isEveryLanguageModelPreloaded = true;
		return this;
	}

	/// <summary>
	/// Disables the high accuracy mode in order to save memory and increase performance.
	/// <para />
	/// By default, *Lingua's* high detection accuracy comes at the cost of
	/// loading large language models into memory which might not be feasible
	/// for systems running low on resources.
	/// <para />
	/// This method disables the high accuracy mode so that only a small subset
	/// of language models is loaded into memory. The downside of this approach
	/// is that detection accuracy for short texts consisting of less than 120
	/// characters will drop significantly. However, detection accuracy for texts
	/// which are longer than 120 characters will remain mostly unaffected.
	/// </summary>
	public LanguageDetectorBuilder WithLowAccuracyMode()
	{
		_isLowAccuracyModeEnabled = true;
		return this;
	}

	/// <summary>
	/// Builds a new instance of <see cref="LanguageDetector"/>.
	/// </summary>
	/// <returns>a new instance of <see cref="LanguageDetector"/></returns>
	public LanguageDetector Build() =>
		new(_languages,
			_minimumRelativeDistance,
			_isEveryLanguageModelPreloaded,
			_isLowAccuracyModeEnabled);
}
