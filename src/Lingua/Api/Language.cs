using Lingua.Internal;
using static Lingua.Api.Language;

namespace Lingua.Api;

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
	/// <summary>The 'Bokmal' language</summary>
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
	/// <summary>The 'Nynorsk' language</summary>
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
			[Afrikaans] = new(Api.IsoCode6391.Af, Api.IsoCode6393.Afr, [Alphabet.Latin]),
			[Albanian] = new(Api.IsoCode6391.Sq, Api.IsoCode6393.Sqi, [Alphabet.Latin]),
			[Amharic] = new(Api.IsoCode6391.Am, Api.IsoCode6393.Amh, [Alphabet.Ethiopic]),
			[Arabic] = new(Api.IsoCode6391.Ar, Api.IsoCode6393.Ara, [Alphabet.Arabic]),
			[Armenian] = new(Api.IsoCode6391.Hy, Api.IsoCode6393.Hye, [Alphabet.Armenian]),
			[Azerbaijani] = new(Api.IsoCode6391.Az, Api.IsoCode6393.Aze, [Alphabet.Latin], "Əə"),
			[Basque] = new(Api.IsoCode6391.Eu, Api.IsoCode6393.Eus, [Alphabet.Latin]),
			[Belarusian] = new(Api.IsoCode6391.Be, Api.IsoCode6393.Bel, [Alphabet.Cyrillic]),
			[Bengali] = new(Api.IsoCode6391.Bn, Api.IsoCode6393.Ben, [Alphabet.Bengali]),
			[Bokmal] = new(Api.IsoCode6391.Nb, Api.IsoCode6393.Nob, [Alphabet.Latin]),
			[Bosnian] = new(Api.IsoCode6391.Bs, Api.IsoCode6393.Bos, [Alphabet.Latin]),
			[Bulgarian] = new(Api.IsoCode6391.Bg, Api.IsoCode6393.Bul, [Alphabet.Cyrillic]),
			[Catalan] = new(Api.IsoCode6391.Ca, Api.IsoCode6393.Cat, [Alphabet.Latin], "Ïï"),
			[Chinese] = new(Api.IsoCode6391.Zh, Api.IsoCode6393.Zho, [Alphabet.Han]),
			[Croatian] = new(Api.IsoCode6391.Hr, Api.IsoCode6393.Hrv, [Alphabet.Latin]),
			[Czech] = new(Api.IsoCode6391.Cs, Api.IsoCode6393.Ces, [Alphabet.Latin], "ĚěŘřŮů"),
			[Danish] = new(Api.IsoCode6391.Da, Api.IsoCode6393.Dan, [Alphabet.Latin]),
			[Dutch] = new(Api.IsoCode6391.Nl, Api.IsoCode6393.Nld, [Alphabet.Latin]),
			[English] = new(Api.IsoCode6391.En, Api.IsoCode6393.Eng, [Alphabet.Latin]),
			[Esperanto] = new(Api.IsoCode6391.Eo, Api.IsoCode6393.Epo, [Alphabet.Latin], "ĈĉĜĝĤĥĴĵŜŝŬŭ"),
			[Estonian] = new(Api.IsoCode6391.Et, Api.IsoCode6393.Est, [Alphabet.Latin]),
			[Finnish] = new(Api.IsoCode6391.Fi, Api.IsoCode6393.Fin, [Alphabet.Latin]),
			[French] = new(Api.IsoCode6391.Fr, Api.IsoCode6393.Fra, [Alphabet.Latin]),
			[Ganda] = new(Api.IsoCode6391.Lg, Api.IsoCode6393.Lug, [Alphabet.Latin]),
			[Georgian] = new(Api.IsoCode6391.Ka, Api.IsoCode6393.Kat, [Alphabet.Georgian]),
			[German] = new(Api.IsoCode6391.De, Api.IsoCode6393.Deu, [Alphabet.Latin], "ß"),
			[Greek] = new(Api.IsoCode6391.El, Api.IsoCode6393.Ell, [Alphabet.Greek]),
			[Gujarati] = new(Api.IsoCode6391.Gu, Api.IsoCode6393.Guj, [Alphabet.Gujarati]),
			[Hebrew] = new(Api.IsoCode6391.He, Api.IsoCode6393.Heb, [Alphabet.Hebrew]),
			[Hindi] = new(Api.IsoCode6391.Hi, Api.IsoCode6393.Hin, [Alphabet.Devanagari]),
			[Hungarian] = new(Api.IsoCode6391.Hu, Api.IsoCode6393.Hun, [Alphabet.Latin], "ŐőŰű"),
			[Icelandic] = new(Api.IsoCode6391.Is, Api.IsoCode6393.Isl, [Alphabet.Latin]),
			[Indonesian] = new(Api.IsoCode6391.Id, Api.IsoCode6393.Ind, [Alphabet.Latin]),
			[Irish] = new(Api.IsoCode6391.Ga, Api.IsoCode6393.Gle, [Alphabet.Latin]),
			[Italian] = new(Api.IsoCode6391.It, Api.IsoCode6393.Ita, [Alphabet.Latin]),
			[Japanese] = new(Api.IsoCode6391.Ja, Api.IsoCode6393.Jpn, [Alphabet.Hiragana, Alphabet.Katakana, Alphabet.Han]),
			[Kazakh] = new(Api.IsoCode6391.Kk, Api.IsoCode6393.Kaz, [Alphabet.Cyrillic], "ӘәҒғҚқҢңҰұ"),
			[Korean] = new(Api.IsoCode6391.Ko, Api.IsoCode6393.Kor, [Alphabet.Hangul]),
			[Latin] = new(Api.IsoCode6391.La, Api.IsoCode6393.Lat, [Alphabet.Latin]),
			[Latvian] = new(Api.IsoCode6391.Lv, Api.IsoCode6393.Lav, [Alphabet.Latin], "ĢģĶķĻļŅņ"),
			[Lithuanian] = new(Api.IsoCode6391.Lt, Api.IsoCode6393.Lit, [Alphabet.Latin], "ĖėĮįŲų"),
			[Macedonian] = new(Api.IsoCode6391.Mk, Api.IsoCode6393.Mkd, [Alphabet.Cyrillic], "ЃѓЅѕЌќЏџ"),
			[Malay] = new(Api.IsoCode6391.Ms, Api.IsoCode6393.Msa, [Alphabet.Latin]),
			[Maori] = new(Api.IsoCode6391.Mi, Api.IsoCode6393.Mri, [Alphabet.Latin]),
			[Marathi] = new(Api.IsoCode6391.Mr, Api.IsoCode6393.Mar, [Alphabet.Devanagari], "ळ"),
			[Mongolian] = new(Api.IsoCode6391.Mn, Api.IsoCode6393.Mon, [Alphabet.Cyrillic], "ӨөҮү"),
			[Nynorsk] = new(Api.IsoCode6391.Nn, Api.IsoCode6393.Nno, [Alphabet.Latin]),
			[Oromo] = new(Api.IsoCode6391.Om, Api.IsoCode6393.Orm, [Alphabet.Latin]),
			[Persian] = new(Api.IsoCode6391.Fa, Api.IsoCode6393.Fas, [Alphabet.Arabic]),
			[Polish] = new(Api.IsoCode6391.Pl, Api.IsoCode6393.Pol, [Alphabet.Latin], "ŁłŃńŚśŹź"),
			[Portuguese] = new(Api.IsoCode6391.Pt, Api.IsoCode6393.Por, [Alphabet.Latin]),
			[Punjabi] = new(Api.IsoCode6391.Pa, Api.IsoCode6393.Pan, [Alphabet.Gurmukhi]),
			[Romanian] = new(Api.IsoCode6391.Ro, Api.IsoCode6393.Ron, [Alphabet.Latin], "Țţ"),
			[Russian] = new(Api.IsoCode6391.Ru, Api.IsoCode6393.Rus, [Alphabet.Cyrillic]),
			[Serbian] = new(Api.IsoCode6391.Sr, Api.IsoCode6393.Srp, [Alphabet.Cyrillic], "ЂђЋћ"),
			[Shona] = new(Api.IsoCode6391.Sn, Api.IsoCode6393.Sna, [Alphabet.Latin]),
			[Sinhala] = new(Api.IsoCode6391.Si, Api.IsoCode6393.Sin, [Alphabet.Sinhala]),
			[Slovak] = new(Api.IsoCode6391.Sk, Api.IsoCode6393.Slk, [Alphabet.Latin], "ĹĺĽľŔŕ"),
			[Slovene] = new(Api.IsoCode6391.Sl, Api.IsoCode6393.Slv, [Alphabet.Latin]),
			[Somali] = new(Api.IsoCode6391.So, Api.IsoCode6393.Som, [Alphabet.Latin]),
			[Sotho] = new(Api.IsoCode6391.St, Api.IsoCode6393.Sot, [Alphabet.Latin]),
			[Spanish] = new(Api.IsoCode6391.Es, Api.IsoCode6393.Spa, [Alphabet.Latin], "¿¡"),
			[Swahili] = new(Api.IsoCode6391.Sw, Api.IsoCode6393.Swa, [Alphabet.Latin]),
			[Swedish] = new(Api.IsoCode6391.Sv, Api.IsoCode6393.Swe, [Alphabet.Latin]),
			[Tagalog] = new(Api.IsoCode6391.Tl, Api.IsoCode6393.Tgl, [Alphabet.Latin]),
			[Tamil] = new(Api.IsoCode6391.Ta, Api.IsoCode6393.Tam, [Alphabet.Tamil]),
			[Telugu] = new(Api.IsoCode6391.Te, Api.IsoCode6393.Tel, [Alphabet.Telugu]),
			[Thai] = new(Api.IsoCode6391.Th, Api.IsoCode6393.Tha, [Alphabet.Thai]),
			[Tigrinya] = new(Api.IsoCode6391.Ti, Api.IsoCode6393.Tir, [Alphabet.Ethiopic]),
			[Tsonga] = new(Api.IsoCode6391.Ts, Api.IsoCode6393.Tso, [Alphabet.Latin]),
			[Tswana] = new(Api.IsoCode6391.Tn, Api.IsoCode6393.Tsn, [Alphabet.Latin]),
			[Turkish] = new(Api.IsoCode6391.Tr, Api.IsoCode6393.Tur, [Alphabet.Latin]),
			[Ukrainian] = new(Api.IsoCode6391.Uk, Api.IsoCode6393.Ukr, [Alphabet.Cyrillic], "ҐґЄєЇї"),
			[Urdu] = new(Api.IsoCode6391.Ur, Api.IsoCode6393.Urd, [Alphabet.Arabic]),
			[Vietnamese] = new(Api.IsoCode6391.Vi, Api.IsoCode6393.Vie, [Alphabet.Latin], "ẰằẦầẲẳẨẩẴẵẪẫẮắẤấẠạẶặẬậỀềẺẻỂểẼẽỄễẾếỆệỈỉĨĩỊịƠơỒồỜờỎỏỔổỞởỖỗỠỡỐốỚớỘộỢợƯưỪừỦủỬửŨũỮữỨứỤụỰựỲỳỶỷỸỹỴỵ"),
			[Welsh] = new(Api.IsoCode6391.Cy, Api.IsoCode6393.Cym, [Alphabet.Latin]),
			[Xhosa] = new(Api.IsoCode6391.Xh, Api.IsoCode6393.Xho, [Alphabet.Latin]),
			// TODO for YORUBA: "E̩e̩Ẹ́ẹ́É̩é̩Ẹ̀ẹ̀È̩è̩Ẹ̄ẹ̄Ē̩ē̩ŌōO̩o̩Ọ́ọ́Ó̩ó̩Ọ̀ọ̀Ò̩ò̩Ọ̄ọ̄Ō̩ō̩ṢṣS̩s̩"
			[Yoruba] = new(Api.IsoCode6391.Yo, Api.IsoCode6393.Yor, [Alphabet.Latin], "Ṣṣ"),
			[Zulu] = new(Api.IsoCode6391.Zu, Api.IsoCode6393.Zul, [Alphabet.Latin]),
			[Unknown] = new(Api.IsoCode6391.None, Api.IsoCode6393.None, [Alphabet.None])
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
