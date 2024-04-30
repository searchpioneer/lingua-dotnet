namespace Lingua.Api;

/// <summary>
/// The supported detectable languages.
/// </summary>
public enum Language
{
    Afrikaans,
    Albanian,
    Amharic,
    Arabic,
    Armenian,
    Azerbaijani,
    Basque,
    Belarusian,
    Bengali,
    Bokmal,
    Bosnian,
    Bulgarian,
    Catalan,
    Chinese,
    Croatian,
    Czech,
    Danish,
    Dutch,
    English,
    Esperanto,
    Estonian,
    Finnish,
    French,
    Ganda,
    Georgian,
    German,
    Greek,
    Gujarati,
    Hebrew,
    Hindi,
    Hungarian,
    Icelandic,
    Indonesian,
    Irish,
    Italian,
    Japanese,
    Kazakh,
    Korean,
    Latin,
    Latvian,
    Lithuanian,
    Macedonian,
    Malay,
    Maori,
    Marathi,
    Mongolian,
    Nynorsk,
    Oromo,
    Persian,
    Polish,
    Portuguese,
    Punjabi,
    Romanian,
    Russian,
    Serbian,
    Shona,
    Sinhala,
    Slovak,
    Slovene,
    Somali,
    Sotho,
    Spanish,
    Swahili,
    Swedish,
    Tagalog,
    Tamil,
    Telugu,
    Thai,
    Tigrinya,
    Tsonga,
    Tswana,
    Turkish,
    Ukrainian,
    Urdu,
    Vietnamese,
    Welsh,
    Xhosa,
    Yoruba,
    Zulu,
    Unknown,
}

public static class LanguageExtensions
{
    private static readonly Dictionary<Language, LanguageProperties> LanguageProperties =
        new(Enum.GetValues(typeof(Language)).Length)
        {
            [Language.Afrikaans] = new(Api.IsoCode6391.Af, Api.IsoCode6393.Afr, [Alphabet.Latin]),
            [Language.Albanian] = new(Api.IsoCode6391.Sq, Api.IsoCode6393.Sqi, [Alphabet.Latin]),
            [Language.Amharic] = new(Api.IsoCode6391.Am, Api.IsoCode6393.Amh, [Alphabet.Ethiopic]),
            [Language.Arabic] = new(Api.IsoCode6391.Ar, Api.IsoCode6393.Ara, [Alphabet.Arabic]),
            [Language.Armenian] = new(Api.IsoCode6391.Hy, Api.IsoCode6393.Hye, [Alphabet.Armenian]),
            [Language.Azerbaijani] = new(Api.IsoCode6391.Az, Api.IsoCode6393.Aze, [Alphabet.Latin], "Əə"),
            [Language.Basque] = new(Api.IsoCode6391.Eu, Api.IsoCode6393.Eus, [Alphabet.Latin]),
            [Language.Belarusian] = new(Api.IsoCode6391.Be, Api.IsoCode6393.Bel, [Alphabet.Cyrillic]),
            [Language.Bengali] = new(Api.IsoCode6391.Bn, Api.IsoCode6393.Ben, [Alphabet.Bengali]),
            [Language.Bokmal] = new(Api.IsoCode6391.Nb, Api.IsoCode6393.Nob, [Alphabet.Latin]),
            [Language.Bosnian] = new(Api.IsoCode6391.Bs, Api.IsoCode6393.Bos, [Alphabet.Latin]),
            [Language.Bulgarian] = new(Api.IsoCode6391.Bg, Api.IsoCode6393.Bul, [Alphabet.Cyrillic]),
            [Language.Catalan] = new(Api.IsoCode6391.Ca, Api.IsoCode6393.Cat, [Alphabet.Latin], "Ïï"),
            [Language.Chinese] = new(Api.IsoCode6391.Zh, Api.IsoCode6393.Zho, [Alphabet.Han]),
            [Language.Croatian] = new(Api.IsoCode6391.Hr, Api.IsoCode6393.Hrv, [Alphabet.Latin]),
            [Language.Czech] = new(Api.IsoCode6391.Cs, Api.IsoCode6393.Ces, [Alphabet.Latin], "ĚěŘřŮů"),
            [Language.Danish] = new(Api.IsoCode6391.Da, Api.IsoCode6393.Dan, [Alphabet.Latin]),
            [Language.Dutch] = new(Api.IsoCode6391.Nl, Api.IsoCode6393.Nld, [Alphabet.Latin]),
            [Language.English] = new(Api.IsoCode6391.En, Api.IsoCode6393.Eng, [Alphabet.Latin]),
            [Language.Esperanto] = new(Api.IsoCode6391.Eo, Api.IsoCode6393.Epo, [Alphabet.Latin], "ĈĉĜĝĤĥĴĵŜŝŬŭ"),
            [Language.Estonian] = new(Api.IsoCode6391.Et, Api.IsoCode6393.Est, [Alphabet.Latin]),
            [Language.Finnish] = new(Api.IsoCode6391.Fi, Api.IsoCode6393.Fin, [Alphabet.Latin]),
            [Language.French] = new(Api.IsoCode6391.Fr, Api.IsoCode6393.Fra, [Alphabet.Latin]),
            [Language.Ganda] = new(Api.IsoCode6391.Lg, Api.IsoCode6393.Lug, [Alphabet.Latin]),
            [Language.Georgian] = new(Api.IsoCode6391.Ka, Api.IsoCode6393.Kat, [Alphabet.Georgian]),
            [Language.German] = new(Api.IsoCode6391.De, Api.IsoCode6393.Deu, [Alphabet.Latin], "ß"),
            [Language.Greek] = new(Api.IsoCode6391.El, Api.IsoCode6393.Ell, [Alphabet.Greek]),
            [Language.Gujarati] = new(Api.IsoCode6391.Gu, Api.IsoCode6393.Guj, [Alphabet.Gujarati]),
            [Language.Hebrew] = new(Api.IsoCode6391.He, Api.IsoCode6393.Heb, [Alphabet.Hebrew]),
            [Language.Hindi] = new(Api.IsoCode6391.Hi, Api.IsoCode6393.Hin, [Alphabet.Devanagari]),
            [Language.Hungarian] = new(Api.IsoCode6391.Hu, Api.IsoCode6393.Hun, [Alphabet.Latin], "ŐőŰű"),
            [Language.Icelandic] = new(Api.IsoCode6391.Is, Api.IsoCode6393.Isl, [Alphabet.Latin]),
            [Language.Indonesian] = new(Api.IsoCode6391.Id, Api.IsoCode6393.Ind, [Alphabet.Latin]),
            [Language.Irish] = new(Api.IsoCode6391.Ga, Api.IsoCode6393.Gle, [Alphabet.Latin]),
            [Language.Italian] = new(Api.IsoCode6391.It, Api.IsoCode6393.Ita, [Alphabet.Latin]),
            [Language.Japanese] = new(Api.IsoCode6391.Ja, Api.IsoCode6393.Jpn, [Alphabet.Hiragana, Alphabet.Katakana, Alphabet.Han]),
            [Language.Kazakh] = new(Api.IsoCode6391.Kk, Api.IsoCode6393.Kaz, [Alphabet.Cyrillic], "ӘәҒғҚқҢңҰұ"),
            [Language.Korean] = new(Api.IsoCode6391.Ko, Api.IsoCode6393.Kor, [Alphabet.Hangul]),
            [Language.Latin] = new(Api.IsoCode6391.La, Api.IsoCode6393.Lat, [Alphabet.Latin]),
            [Language.Latvian] = new(Api.IsoCode6391.Lv, Api.IsoCode6393.Lav, [Alphabet.Latin], "ĢģĶķĻļŅņ"),
            [Language.Lithuanian] = new(Api.IsoCode6391.Lt, Api.IsoCode6393.Lit, [Alphabet.Latin], "ĖėĮįŲų"),
            [Language.Macedonian] = new(Api.IsoCode6391.Mk, Api.IsoCode6393.Mkd, [Alphabet.Cyrillic], "ЃѓЅѕЌќЏџ"),
            [Language.Malay] = new(Api.IsoCode6391.Ms, Api.IsoCode6393.Msa, [Alphabet.Latin]),
            [Language.Maori] = new(Api.IsoCode6391.Mi, Api.IsoCode6393.Mri, [Alphabet.Latin]),
            [Language.Marathi] = new(Api.IsoCode6391.Mr, Api.IsoCode6393.Mar, [Alphabet.Devanagari], "ळ"),
            [Language.Mongolian] = new(Api.IsoCode6391.Mn, Api.IsoCode6393.Mon, [Alphabet.Cyrillic], "ӨөҮү"),
            [Language.Nynorsk] = new(Api.IsoCode6391.Nn, Api.IsoCode6393.Nno, [Alphabet.Latin]),
            [Language.Oromo] = new(Api.IsoCode6391.Om, Api.IsoCode6393.Orm, [Alphabet.Latin]),
            [Language.Persian] = new(Api.IsoCode6391.Fa, Api.IsoCode6393.Fas, [Alphabet.Arabic]),
            [Language.Polish] = new(Api.IsoCode6391.Pl, Api.IsoCode6393.Pol, [Alphabet.Latin], "ŁłŃńŚśŹź"),
            [Language.Portuguese] = new(Api.IsoCode6391.Pt, Api.IsoCode6393.Por, [Alphabet.Latin]),
            [Language.Punjabi] = new(Api.IsoCode6391.Pa, Api.IsoCode6393.Pan, [Alphabet.Gurmukhi]),
            [Language.Romanian] = new(Api.IsoCode6391.Ro, Api.IsoCode6393.Ron, [Alphabet.Latin], "Țţ"),
            [Language.Russian] = new(Api.IsoCode6391.Ru, Api.IsoCode6393.Rus, [Alphabet.Cyrillic]),
            [Language.Serbian] = new(Api.IsoCode6391.Sr, Api.IsoCode6393.Srp, [Alphabet.Cyrillic], "ЂђЋћ"),
            [Language.Shona] = new(Api.IsoCode6391.Sn, Api.IsoCode6393.Sna, [Alphabet.Latin]),
            [Language.Sinhala] = new(Api.IsoCode6391.Si, Api.IsoCode6393.Sin, [Alphabet.Sinhala]),
            [Language.Slovak] = new(Api.IsoCode6391.Sk, Api.IsoCode6393.Slk, [Alphabet.Latin], "ĹĺĽľŔŕ"),
            [Language.Slovene] = new(Api.IsoCode6391.Sl, Api.IsoCode6393.Slv, [Alphabet.Latin]),
            [Language.Somali] = new(Api.IsoCode6391.So, Api.IsoCode6393.Som, [Alphabet.Latin]),
            [Language.Sotho] = new(Api.IsoCode6391.St, Api.IsoCode6393.Sot, [Alphabet.Latin]),
            [Language.Spanish] = new(Api.IsoCode6391.Es, Api.IsoCode6393.Spa, [Alphabet.Latin], "¿¡"),
            [Language.Swahili] = new(Api.IsoCode6391.Sw, Api.IsoCode6393.Swa, [Alphabet.Latin]),
            [Language.Swedish] = new(Api.IsoCode6391.Sv, Api.IsoCode6393.Swe, [Alphabet.Latin]),
            [Language.Tagalog] = new(Api.IsoCode6391.Tl, Api.IsoCode6393.Tgl, [Alphabet.Latin]),
            [Language.Tamil] = new(Api.IsoCode6391.Ta, Api.IsoCode6393.Tam, [Alphabet.Tamil]),
            [Language.Telugu] = new(Api.IsoCode6391.Te, Api.IsoCode6393.Tel, [Alphabet.Telugu]),
            [Language.Thai] = new(Api.IsoCode6391.Th, Api.IsoCode6393.Tha, [Alphabet.Thai]),
            [Language.Tigrinya] = new(Api.IsoCode6391.Ti, Api.IsoCode6393.Tir, [Alphabet.Ethiopic]),
            [Language.Tsonga] = new(Api.IsoCode6391.Ts, Api.IsoCode6393.Tso, [Alphabet.Latin]),
            [Language.Tswana] = new(Api.IsoCode6391.Tn, Api.IsoCode6393.Tsn, [Alphabet.Latin]),
            [Language.Turkish] = new(Api.IsoCode6391.Tr, Api.IsoCode6393.Tur, [Alphabet.Latin]),
            [Language.Ukrainian] = new(Api.IsoCode6391.Uk, Api.IsoCode6393.Ukr, [Alphabet.Cyrillic], "ҐґЄєЇї"),
            [Language.Urdu] = new(Api.IsoCode6391.Ur, Api.IsoCode6393.Urd, [Alphabet.Arabic]),
            [Language.Vietnamese] = new(Api.IsoCode6391.Vi, Api.IsoCode6393.Vie, [Alphabet.Latin],"ẰằẦầẲẳẨẩẴẵẪẫẮắẤấẠạẶặẬậỀềẺẻỂểẼẽỄễẾếỆệỈỉĨĩỊịƠơỒồỜờỎỏỔổỞởỖỗỠỡỐốỚớỘộỢợƯưỪừỦủỬửŨũỮữỨứỤụỰựỲỳỶỷỸỹỴỵ"),
            [Language.Welsh] = new(Api.IsoCode6391.Cy, Api.IsoCode6393.Cym, [Alphabet.Latin]),
            [Language.Xhosa] = new(Api.IsoCode6391.Xh, Api.IsoCode6393.Xho, [Alphabet.Latin]),
            // TODO for YORUBA: "E̩e̩Ẹ́ẹ́É̩é̩Ẹ̀ẹ̀È̩è̩Ẹ̄ẹ̄Ē̩ē̩ŌōO̩o̩Ọ́ọ́Ó̩ó̩Ọ̀ọ̀Ò̩ò̩Ọ̄ọ̄Ō̩ō̩ṢṣS̩s̩"
            [Language.Yoruba] = new(Api.IsoCode6391.Yo, Api.IsoCode6393.Yor, [Alphabet.Latin], "Ṣṣ"),
            [Language.Zulu] = new(Api.IsoCode6391.Zu, Api.IsoCode6393.Zul, [Alphabet.Latin]),
            [Language.Unknown] = new(Api.IsoCode6391.None, Api.IsoCode6393.None, [Alphabet.None])
        };

    public static IsoCode6391 IsoCode6391(this Language language) => LanguageProperties[language].IsoCode6391;

    public static IsoCode6393 IsoCode6393(this Language language) => LanguageProperties[language].IsoCode6393;

    public static IReadOnlySet<Alphabet> Alphabets(this Language language) => LanguageProperties[language].Alphabets;

    public static string? UniqueCharacters(this Language language) => LanguageProperties[language].UniqueCharacters;

    public static IList<Language> All() => LanguageProperties.Keys
        .Where(l => l is not Language.Unknown).ToList();

    public static IList<Language> AllSpokenOnes() => LanguageProperties.Keys
        .Where(l => l is not (Language.Unknown or Language.Latin)).ToList();

    public static IList<Language> AllWithArabicScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.Arabic))
        .Select(l => l.Key)
        .ToList();

    public static IList<Language> AllWithCyrillicScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.Cyrillic))
        .Select(l => l.Key)
        .ToList();

    public static IList<Language> AllWithDevangariScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.Devanagari))
        .Select(l => l.Key)
        .ToList();

    public static IList<Language> AllWithEthiopicScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.Ethiopic))
        .Select(l => l.Key)
        .ToList();

    public static IList<Language> AllWithLatinScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.Latin))
        .Select(l => l.Key)
        .ToList();

    public static Language GetByIsoCode6391(IsoCode6391 isoCode6391) =>
        LanguageProperties.Single(l => l.Value.IsoCode6391 == isoCode6391).Key;

    public static Language GetByIsoCode6393(IsoCode6393 isoCode6393) =>
        LanguageProperties.Single(l => l.Value.IsoCode6393 == isoCode6393).Key;

    public static readonly IReadOnlySet<Language> LanguagesSupportingLogograms =
        new HashSet<Language>{ Language.Chinese, Language.Japanese, Language.Korean };
}

internal record LanguageProperties(IsoCode6391 IsoCode6391, IsoCode6393 IsoCode6393, HashSet<Alphabet> Alphabets, string? UniqueCharacters = null)
{
}
