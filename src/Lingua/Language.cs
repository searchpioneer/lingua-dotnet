using Lingua.Internal;
using static Lingua.Language;

namespace Lingua;

/// <summary>
/// The supported detectable languages.
/// </summary>
public enum Language
{
	/// <summary>The 'Afrikaans' language</summary>
	Afrikaans,
	/// <summary>The 'Albanian' language</summary>
	Albanian,
	/// <summary>The 'Amharic' language</summary>
	Amharic,
	/// <summary>The 'Arabic' language</summary>
	Arabic,
	/// <summary>The 'Armenian' language</summary>
	Armenian,
	/// <summary>The 'Azerbaijani' language</summary>
	Azerbaijani,
	/// <summary>The 'Basque' language</summary>
	Basque,
	/// <summary>The 'Belarusian' language</summary>
	Belarusian,
	/// <summary>The 'Bengali' language</summary>
	Bengali,
	/// <summary>The Norwegian 'Bokmal' language</summary>
	Bokmal,
	/// <summary>The 'Bosnian' language</summary>
	Bosnian,
	/// <summary>The 'Bulgarian' language</summary>
	Bulgarian,
	/// <summary>The 'Catalan' language</summary>
	Catalan,
	/// <summary>The 'Chinese' language</summary>
	Chinese,
	/// <summary>The 'Croatian' language</summary>
	Croatian,
	/// <summary>The 'Czech' language</summary>
	Czech,
	/// <summary>The 'Danish' language</summary>
	Danish,
	/// <summary>The 'Dutch' language</summary>
	Dutch,
	/// <summary>The 'English' language</summary>
	English,
	/// <summary>The 'Esperanto' language</summary>
	Esperanto,
	/// <summary>The 'Estonian' language</summary>
	Estonian,
	/// <summary>The 'Finnish' language</summary>
	Finnish,
	/// <summary>The 'French' language</summary>
	French,
	/// <summary>The 'Ganda' language</summary>
	Ganda,
	/// <summary>The 'Georgian' language</summary>
	Georgian,
	/// <summary>The 'German' language</summary>
	German,
	/// <summary>The 'Greek' language</summary>
	Greek,
	/// <summary>The 'Gujarati' language</summary>
	Gujarati,
	/// <summary>The 'Hebrew' language</summary>
	Hebrew,
	/// <summary>The 'Hindi' language</summary>
	Hindi,
	/// <summary>The 'Hungarian' language</summary>
	Hungarian,
	/// <summary>The 'Icelandic' language</summary>
	Icelandic,
	/// <summary>The 'Indonesian' language</summary>
	Indonesian,
	/// <summary>The 'Irish' language</summary>
	Irish,
	/// <summary>The 'Italian' language</summary>
	Italian,
	/// <summary>The 'Japanese' language</summary>
	Japanese,
	/// <summary>The 'Kazakh' language</summary>
	Kazakh,
	/// <summary>The 'Korean' language</summary>
	Korean,
	/// <summary>The 'Latin' language</summary>
	Latin,
	/// <summary>The 'Latvian' language</summary>
	Latvian,
	/// <summary>The 'Lithuanian' language</summary>
	Lithuanian,
	/// <summary>The 'Macedonian' language</summary>
	Macedonian,
	/// <summary>The 'Malay' language</summary>
	Malay,
	/// <summary>The 'Maori' language</summary>
	Maori,
	/// <summary>The 'Marathi' language</summary>
	Marathi,
	/// <summary>The 'Mongolian' language</summary>
	Mongolian,
	/// <summary>The Norwegian 'Nynorsk' language</summary>
	Nynorsk,
	/// <summary>The 'Oromo' language</summary>
	Oromo,
	/// <summary>The 'Persian' language</summary>
	Persian,
	/// <summary>The 'Polish' language</summary>
	Polish,
	/// <summary>The 'Portuguese' language</summary>
	Portuguese,
	/// <summary>The 'Punjabi' language</summary>
	Punjabi,
	/// <summary>The 'Romanian' language</summary>
	Romanian,
	/// <summary>The 'Russian' language</summary>
	Russian,
	/// <summary>The 'Serbian' language</summary>
	Serbian,
	/// <summary>The 'Shona' language</summary>
	Shona,
	/// <summary>The 'Sinhala' language</summary>
	Sinhala,
	/// <summary>The 'Slovak' language</summary>
	Slovak,
	/// <summary>The 'Slovene' language</summary>
	Slovene,
	/// <summary>The 'Somali' language</summary>
	Somali,
	/// <summary>The 'Sotho' language</summary>
	Sotho,
	/// <summary>The 'Spanish' language</summary>
	Spanish,
	/// <summary>The 'Swahili' language</summary>
	Swahili,
	/// <summary>The 'Swedish' language</summary>
	Swedish,
	/// <summary>The 'Tagalog' language</summary>
	Tagalog,
	/// <summary>The 'Tamil' language</summary>
	Tamil,
	/// <summary>The 'Telugu' language</summary>
	Telugu,
	/// <summary>The 'Thai' language</summary>
	Thai,
	/// <summary>The 'Tigrinya' language</summary>
	Tigrinya,
	/// <summary>The 'Tsonga' language</summary>
	Tsonga,
	/// <summary>The 'Tswana' language</summary>
	Tswana,
	/// <summary>The 'Turkish' language</summary>
	Turkish,
	/// <summary>The 'Ukrainian' language</summary>
	Ukrainian,
	/// <summary>The 'Urdu' language</summary>
	Urdu,
	/// <summary>The 'Vietnamese' language</summary>
	Vietnamese,
	/// <summary>The 'Welsh' language</summary>
	Welsh,
	/// <summary>The 'Xhosa' language</summary>
	Xhosa,
	/// <summary>The 'Yoruba' language</summary>
	Yoruba,
	/// <summary>The 'Zulu' language</summary>
	Zulu,
	/// <summary>The imaginary 'Unknown' language</summary>
	Unknown,
}

/// <summary>
/// Extension methods for <see cref="Language"/>.
/// </summary>
public static class LanguageInfo
{
	private static readonly Dictionary<Language, LanguageProperties> LanguageProperties =
		new(Enum.GetValues(typeof(Language)).Length)
		{
			[Afrikaans] = new(Lingua.IsoCode6391.Af, Lingua.IsoCode6393.Afr, [Alphabet.Latin]),
			[Albanian] = new(Lingua.IsoCode6391.Sq, Lingua.IsoCode6393.Sqi, [Alphabet.Latin]),
			[Amharic] = new(Lingua.IsoCode6391.Am, Lingua.IsoCode6393.Amh, [Alphabet.Ethiopic]),
			[Arabic] = new(Lingua.IsoCode6391.Ar, Lingua.IsoCode6393.Ara, [Alphabet.Arabic]),
			[Armenian] = new(Lingua.IsoCode6391.Hy, Lingua.IsoCode6393.Hye, [Alphabet.Armenian]),
			[Azerbaijani] = new(Lingua.IsoCode6391.Az, Lingua.IsoCode6393.Aze, [Alphabet.Latin], "Əə"),
			[Basque] = new(Lingua.IsoCode6391.Eu, Lingua.IsoCode6393.Eus, [Alphabet.Latin]),
			[Belarusian] = new(Lingua.IsoCode6391.Be, Lingua.IsoCode6393.Bel, [Alphabet.Cyrillic]),
			[Bengali] = new(Lingua.IsoCode6391.Bn, Lingua.IsoCode6393.Ben, [Alphabet.Bengali]),
			[Bokmal] = new(Lingua.IsoCode6391.Nb, Lingua.IsoCode6393.Nob, [Alphabet.Latin]),
			[Bosnian] = new(Lingua.IsoCode6391.Bs, Lingua.IsoCode6393.Bos, [Alphabet.Latin]),
			[Bulgarian] = new(Lingua.IsoCode6391.Bg, Lingua.IsoCode6393.Bul, [Alphabet.Cyrillic]),
			[Catalan] = new(Lingua.IsoCode6391.Ca, Lingua.IsoCode6393.Cat, [Alphabet.Latin], "Ïï"),
			[Chinese] = new(Lingua.IsoCode6391.Zh, Lingua.IsoCode6393.Zho, [Alphabet.Han]),
			[Croatian] = new(Lingua.IsoCode6391.Hr, Lingua.IsoCode6393.Hrv, [Alphabet.Latin]),
			[Czech] = new(Lingua.IsoCode6391.Cs, Lingua.IsoCode6393.Ces, [Alphabet.Latin], "ĚěŘřŮů"),
			[Danish] = new(Lingua.IsoCode6391.Da, Lingua.IsoCode6393.Dan, [Alphabet.Latin]),
			[Dutch] = new(Lingua.IsoCode6391.Nl, Lingua.IsoCode6393.Nld, [Alphabet.Latin]),
			[English] = new(Lingua.IsoCode6391.En, Lingua.IsoCode6393.Eng, [Alphabet.Latin]),
			[Esperanto] = new(Lingua.IsoCode6391.Eo, Lingua.IsoCode6393.Epo, [Alphabet.Latin], "ĈĉĜĝĤĥĴĵŜŝŬŭ"),
			[Estonian] = new(Lingua.IsoCode6391.Et, Lingua.IsoCode6393.Est, [Alphabet.Latin]),
			[Finnish] = new(Lingua.IsoCode6391.Fi, Lingua.IsoCode6393.Fin, [Alphabet.Latin]),
			[French] = new(Lingua.IsoCode6391.Fr, Lingua.IsoCode6393.Fra, [Alphabet.Latin]),
			[Ganda] = new(Lingua.IsoCode6391.Lg, Lingua.IsoCode6393.Lug, [Alphabet.Latin]),
			[Georgian] = new(Lingua.IsoCode6391.Ka, Lingua.IsoCode6393.Kat, [Alphabet.Georgian]),
			[German] = new(Lingua.IsoCode6391.De, Lingua.IsoCode6393.Deu, [Alphabet.Latin], "ß"),
			[Greek] = new(Lingua.IsoCode6391.El, Lingua.IsoCode6393.Ell, [Alphabet.Greek]),
			[Gujarati] = new(Lingua.IsoCode6391.Gu, Lingua.IsoCode6393.Guj, [Alphabet.Gujarati]),
			[Hebrew] = new(Lingua.IsoCode6391.He, Lingua.IsoCode6393.Heb, [Alphabet.Hebrew]),
			[Hindi] = new(Lingua.IsoCode6391.Hi, Lingua.IsoCode6393.Hin, [Alphabet.Devanagari]),
			[Hungarian] = new(Lingua.IsoCode6391.Hu, Lingua.IsoCode6393.Hun, [Alphabet.Latin], "ŐőŰű"),
			[Icelandic] = new(Lingua.IsoCode6391.Is, Lingua.IsoCode6393.Isl, [Alphabet.Latin]),
			[Indonesian] = new(Lingua.IsoCode6391.Id, Lingua.IsoCode6393.Ind, [Alphabet.Latin]),
			[Irish] = new(Lingua.IsoCode6391.Ga, Lingua.IsoCode6393.Gle, [Alphabet.Latin]),
			[Italian] = new(Lingua.IsoCode6391.It, Lingua.IsoCode6393.Ita, [Alphabet.Latin]),
			[Japanese] = new(Lingua.IsoCode6391.Ja, Lingua.IsoCode6393.Jpn, [Alphabet.Hiragana, Alphabet.Katakana, Alphabet.Han]),
			[Kazakh] = new(Lingua.IsoCode6391.Kk, Lingua.IsoCode6393.Kaz, [Alphabet.Cyrillic], "ӘәҒғҚқҢңҰұ"),
			[Korean] = new(Lingua.IsoCode6391.Ko, Lingua.IsoCode6393.Kor, [Alphabet.Hangul]),
			[Latin] = new(Lingua.IsoCode6391.La, Lingua.IsoCode6393.Lat, [Alphabet.Latin]),
			[Latvian] = new(Lingua.IsoCode6391.Lv, Lingua.IsoCode6393.Lav, [Alphabet.Latin], "ĢģĶķĻļŅņ"),
			[Lithuanian] = new(Lingua.IsoCode6391.Lt, Lingua.IsoCode6393.Lit, [Alphabet.Latin], "ĖėĮįŲų"),
			[Macedonian] = new(Lingua.IsoCode6391.Mk, Lingua.IsoCode6393.Mkd, [Alphabet.Cyrillic], "ЃѓЅѕЌќЏџ"),
			[Malay] = new(Lingua.IsoCode6391.Ms, Lingua.IsoCode6393.Msa, [Alphabet.Latin]),
			[Maori] = new(Lingua.IsoCode6391.Mi, Lingua.IsoCode6393.Mri, [Alphabet.Latin]),
			[Marathi] = new(Lingua.IsoCode6391.Mr, Lingua.IsoCode6393.Mar, [Alphabet.Devanagari], "ळ"),
			[Mongolian] = new(Lingua.IsoCode6391.Mn, Lingua.IsoCode6393.Mon, [Alphabet.Cyrillic], "ӨөҮү"),
			[Nynorsk] = new(Lingua.IsoCode6391.Nn, Lingua.IsoCode6393.Nno, [Alphabet.Latin]),
			[Oromo] = new(Lingua.IsoCode6391.Om, Lingua.IsoCode6393.Orm, [Alphabet.Latin]),
			[Persian] = new(Lingua.IsoCode6391.Fa, Lingua.IsoCode6393.Fas, [Alphabet.Arabic]),
			[Polish] = new(Lingua.IsoCode6391.Pl, Lingua.IsoCode6393.Pol, [Alphabet.Latin], "ŁłŃńŚśŹź"),
			[Portuguese] = new(Lingua.IsoCode6391.Pt, Lingua.IsoCode6393.Por, [Alphabet.Latin]),
			[Punjabi] = new(Lingua.IsoCode6391.Pa, Lingua.IsoCode6393.Pan, [Alphabet.Gurmukhi]),
			[Romanian] = new(Lingua.IsoCode6391.Ro, Lingua.IsoCode6393.Ron, [Alphabet.Latin], "Țţ"),
			[Russian] = new(Lingua.IsoCode6391.Ru, Lingua.IsoCode6393.Rus, [Alphabet.Cyrillic]),
			[Serbian] = new(Lingua.IsoCode6391.Sr, Lingua.IsoCode6393.Srp, [Alphabet.Cyrillic], "ЂђЋћ"),
			[Shona] = new(Lingua.IsoCode6391.Sn, Lingua.IsoCode6393.Sna, [Alphabet.Latin]),
			[Sinhala] = new(Lingua.IsoCode6391.Si, Lingua.IsoCode6393.Sin, [Alphabet.Sinhala]),
			[Slovak] = new(Lingua.IsoCode6391.Sk, Lingua.IsoCode6393.Slk, [Alphabet.Latin], "ĹĺĽľŔŕ"),
			[Slovene] = new(Lingua.IsoCode6391.Sl, Lingua.IsoCode6393.Slv, [Alphabet.Latin]),
			[Somali] = new(Lingua.IsoCode6391.So, Lingua.IsoCode6393.Som, [Alphabet.Latin]),
			[Sotho] = new(Lingua.IsoCode6391.St, Lingua.IsoCode6393.Sot, [Alphabet.Latin]),
			[Spanish] = new(Lingua.IsoCode6391.Es, Lingua.IsoCode6393.Spa, [Alphabet.Latin], "¿¡"),
			[Swahili] = new(Lingua.IsoCode6391.Sw, Lingua.IsoCode6393.Swa, [Alphabet.Latin]),
			[Swedish] = new(Lingua.IsoCode6391.Sv, Lingua.IsoCode6393.Swe, [Alphabet.Latin]),
			[Tagalog] = new(Lingua.IsoCode6391.Tl, Lingua.IsoCode6393.Tgl, [Alphabet.Latin]),
			[Tamil] = new(Lingua.IsoCode6391.Ta, Lingua.IsoCode6393.Tam, [Alphabet.Tamil]),
			[Telugu] = new(Lingua.IsoCode6391.Te, Lingua.IsoCode6393.Tel, [Alphabet.Telugu]),
			[Thai] = new(Lingua.IsoCode6391.Th, Lingua.IsoCode6393.Tha, [Alphabet.Thai]),
			[Tigrinya] = new(Lingua.IsoCode6391.Ti, Lingua.IsoCode6393.Tir, [Alphabet.Ethiopic]),
			[Tsonga] = new(Lingua.IsoCode6391.Ts, Lingua.IsoCode6393.Tso, [Alphabet.Latin]),
			[Tswana] = new(Lingua.IsoCode6391.Tn, Lingua.IsoCode6393.Tsn, [Alphabet.Latin]),
			[Turkish] = new(Lingua.IsoCode6391.Tr, Lingua.IsoCode6393.Tur, [Alphabet.Latin]),
			[Ukrainian] = new(Lingua.IsoCode6391.Uk, Lingua.IsoCode6393.Ukr, [Alphabet.Cyrillic], "ҐґЄєЇї"),
			[Urdu] = new(Lingua.IsoCode6391.Ur, Lingua.IsoCode6393.Urd, [Alphabet.Arabic]),
			[Vietnamese] = new(Lingua.IsoCode6391.Vi, Lingua.IsoCode6393.Vie, [Alphabet.Latin], "ẰằẦầẲẳẨẩẴẵẪẫẮắẤấẠạẶặẬậỀềẺẻỂểẼẽỄễẾếỆệỈỉĨĩỊịƠơỒồỜờỎỏỔổỞởỖỗỠỡỐốỚớỘộỢợƯưỪừỦủỬửŨũỮữỨứỤụỰựỲỳỶỷỸỹỴỵ"),
			[Welsh] = new(Lingua.IsoCode6391.Cy, Lingua.IsoCode6393.Cym, [Alphabet.Latin]),
			[Xhosa] = new(Lingua.IsoCode6391.Xh, Lingua.IsoCode6393.Xho, [Alphabet.Latin]),
			// TODO for YORUBA: "E̩e̩Ẹ́ẹ́É̩é̩Ẹ̀ẹ̀È̩è̩Ẹ̄ẹ̄Ē̩ē̩ŌōO̩o̩Ọ́ọ́Ó̩ó̩Ọ̀ọ̀Ò̩ò̩Ọ̄ọ̄Ō̩ō̩ṢṣS̩s̩"
			[Yoruba] = new(Lingua.IsoCode6391.Yo, Lingua.IsoCode6393.Yor, [Alphabet.Latin], "Ṣṣ"),
			[Zulu] = new(Lingua.IsoCode6391.Zu, Lingua.IsoCode6393.Zul, [Alphabet.Latin]),
			[Unknown] = new(Lingua.IsoCode6391.None, Lingua.IsoCode6393.None, [Alphabet.None])
		};

	/// <summary>
	/// Gets the ISO639-1 code for the language
	/// </summary>
	/// <param name="language">The language</param>
	/// <returns>The ISO639-1 code</returns>
	public static IsoCode6391 IsoCode6391(this Language language) => LanguageProperties[language].IsoCode6391;

	/// <summary>
	/// Gets the ISO639-3 code for the language
	/// </summary>
	/// <param name="language">The language</param>
	/// <returns>The ISO639-3 code</returns>
	public static IsoCode6393 IsoCode6393(this Language language) => LanguageProperties[language].IsoCode6393;

	/// <summary>
	/// Gets the unique characters for the language, or null if there are no unique characters.
	/// </summary>
	/// <param name="language">The language</param>
	/// <returns>The unique characters for the language, or null if there are no unique characters.</returns>
	public static string? UniqueCharacters(this Language language) => LanguageProperties[language].UniqueCharacters;

	/// <summary>
	/// Gets the list of languages supporting logograms
	/// </summary>
	public static readonly IReadOnlySet<Language> LanguagesSupportingLogograms =
		new HashSet<Language> { Chinese, Japanese, Korean };

	/// <summary>
	/// Gets a set of all built-in languages.
	/// </summary>
	/// <returns>A set of all built-in languages.</returns>
	public static HashSet<Language> All() => LanguageProperties.Keys
		.Where(l => l is not Unknown).ToHashSet();

	/// <summary>
	/// Gets a set of all built-in languages that are still spoken today.
	/// </summary>
	/// <returns>A set of all built-in languages that are still spoken today.</returns>
	public static HashSet<Language> AllSpokenOnes() => LanguageProperties.Keys
		.Where(l => l is not (Unknown or Latin)).ToHashSet();

	/// <summary>
	/// Gets a set of all built-in languages supporting the Arabic script.
	/// </summary>
	/// <returns>A set of all built-in languages supporting the Arabic script.</returns>
	public static HashSet<Language> AllWithArabicScript() => LanguageProperties
		.Where(l => l.Value.Alphabets.Contains(Alphabet.Arabic))
		.Select(l => l.Key)
		.ToHashSet();

	/// <summary>
	/// Gets a set of all built-in languages supporting the Cyrillic script.
	/// </summary>
	/// <returns>A set of all built-in languages supporting the Cyrillic script.</returns>
	public static HashSet<Language> AllWithCyrillicScript() => LanguageProperties
		.Where(l => l.Value.Alphabets.Contains(Alphabet.Cyrillic))
		.Select(l => l.Key)
		.ToHashSet();

	/// <summary>
	/// Gets a set of all built-in languages supporting the Devangari script.
	/// </summary>
	/// <returns>A set of all built-in languages supporting the Devangari script.</returns>
	public static HashSet<Language> AllWithDevangariScript() => LanguageProperties
		.Where(l => l.Value.Alphabets.Contains(Alphabet.Devanagari))
		.Select(l => l.Key)
		.ToHashSet();

	/// <summary>
	/// Gets a set of all built-in languages supporting the Ethiopic script.
	/// </summary>
	/// <returns>A set of all built-in languages supporting the Ethiopic script.</returns>
	public static HashSet<Language> AllWithEthiopicScript() => LanguageProperties
		.Where(l => l.Value.Alphabets.Contains(Alphabet.Ethiopic))
		.Select(l => l.Key)
		.ToHashSet();

	/// <summary>
	/// Gets a set of all built-in languages supporting the Latin script.
	/// </summary>
	/// <returns>A set of all built-in languages supporting the Latin script.</returns>
	public static HashSet<Language> AllWithLatinScript() => LanguageProperties
		.Where(l => l.Value.Alphabets.Contains(Alphabet.Latin))
		.Select(l => l.Key)
		.ToHashSet();

	/// <summary>
	/// Gets a language by its <see cref="IsoCode6391"/> code.
	/// </summary>
	/// <param name="isoCode6391">The ISO 939-1 code</param>
	/// <returns>The language identified by the given ISO 939-1 code.</returns>
	public static Language GetByIsoCode6391(IsoCode6391 isoCode6391) =>
		LanguageProperties.First(l => l.Value.IsoCode6391 == isoCode6391).Key;

	/// <summary>
	/// Gets a language by its <see cref="IsoCode6393"/> code.
	/// </summary>
	/// <param name="isoCode6393">The ISO 939-3 code</param>
	/// <returns>The language identified by the given ISO 939-3 code.</returns>
	public static Language GetByIsoCode6393(IsoCode6393 isoCode6393) =>
		LanguageProperties.First(l => l.Value.IsoCode6393 == isoCode6393).Key;

	internal static IReadOnlySet<Alphabet> Alphabets(this Language language) => LanguageProperties[language].Alphabets;
}

internal readonly record struct LanguageProperties(IsoCode6391 IsoCode6391, IsoCode6393 IsoCode6393, HashSet<Alphabet> Alphabets, string? UniqueCharacters = null)
{
}
