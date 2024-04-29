namespace Lingua.Api;

/// <summary>
/// The supported detectable languages.
/// </summary>
public enum Language
{
    AFRIKAANS,
    ALBANIAN,
    AMHARIC,
    ARABIC,
    ARMENIAN,
    AZERBAIJANI,
    BASQUE,
    BELARUSIAN,
    BENGALI,
    BOKMAL,
    BOSNIAN,
    BULGARIAN,
    CATALAN,
    CHINESE,
    CROATIAN,
    CZECH,
    DANISH,
    DUTCH,
    ENGLISH,
    ESPERANTO,
    ESTONIAN,
    FINNISH,
    FRENCH,
    GANDA,
    GEORGIAN,
    GERMAN,
    GREEK,
    GUJARATI,
    HEBREW,
    HINDI,
    HUNGARIAN,
    ICELANDIC,
    INDONESIAN,
    IRISH,
    ITALIAN,
    JAPANESE,
    KAZAKH,
    KOREAN,
    LATIN,
    LATVIAN,
    LITHUANIAN,
    MACEDONIAN,
    MALAY,
    MAORI,
    MARATHI,
    MONGOLIAN,
    NYNORSK,
    OROMO,
    PERSIAN,
    POLISH,
    PORTUGUESE,
    PUNJABI,
    ROMANIAN,
    RUSSIAN,
    SERBIAN,
    SHONA,
    SINHALA,
    SLOVAK,
    SLOVENE,
    SOMALI,
    SOTHO,
    SPANISH,
    SWAHILI,
    SWEDISH,
    TAGALOG,
    TAMIL,
    TELUGU,
    THAI,
    TIGRINYA,
    TSONGA,
    TSWANA,
    TURKISH,
    UKRAINIAN,
    URDU,
    VIETNAMESE,
    WELSH,
    XHOSA,
    YORUBA,
    ZULU,
    UNKNOWN,
}

public static class LanguageExtensions
{
    private static readonly Dictionary<Language, LanguageProperties> LanguageProperties =
        new(Enum.GetValues(typeof(Language)).Length)
        {
            [Language.AFRIKAANS] = new(Api.IsoCode6391.AF, Api.IsoCode6393.AFR, [Alphabet.LATIN]),
            [Language.ALBANIAN] = new(Api.IsoCode6391.SQ, Api.IsoCode6393.SQI, [Alphabet.LATIN]),
            [Language.AMHARIC] = new(Api.IsoCode6391.AM, Api.IsoCode6393.AMH, [Alphabet.ETHIOPIC]),
            [Language.ARABIC] = new(Api.IsoCode6391.AR, Api.IsoCode6393.ARA, [Alphabet.ARABIC]),
            [Language.ARMENIAN] = new(Api.IsoCode6391.HY, Api.IsoCode6393.HYE, [Alphabet.ARMENIAN]),
            [Language.AZERBAIJANI] = new(Api.IsoCode6391.AZ, Api.IsoCode6393.AZE, [Alphabet.LATIN], "Əə"),
            [Language.BASQUE] = new(Api.IsoCode6391.EU, Api.IsoCode6393.EUS, [Alphabet.LATIN]),
            [Language.BELARUSIAN] = new(Api.IsoCode6391.BE, Api.IsoCode6393.BEL, [Alphabet.CYRILLIC]),
            [Language.BENGALI] = new(Api.IsoCode6391.BN, Api.IsoCode6393.BEN, [Alphabet.BENGALI]),
            [Language.BOKMAL] = new(Api.IsoCode6391.NB, Api.IsoCode6393.NOB, [Alphabet.LATIN]),
            [Language.BOSNIAN] = new(Api.IsoCode6391.BS, Api.IsoCode6393.BOS, [Alphabet.LATIN]),
            [Language.BULGARIAN] = new(Api.IsoCode6391.BG, Api.IsoCode6393.BUL, [Alphabet.CYRILLIC]),
            [Language.CATALAN] = new(Api.IsoCode6391.CA, Api.IsoCode6393.CAT, [Alphabet.LATIN], "Ïï"),
            [Language.CHINESE] = new(Api.IsoCode6391.ZH, Api.IsoCode6393.ZHO, [Alphabet.HAN]),
            [Language.CROATIAN] = new(Api.IsoCode6391.HR, Api.IsoCode6393.HRV, [Alphabet.LATIN]),
            [Language.CZECH] = new(Api.IsoCode6391.CS, Api.IsoCode6393.CES, [Alphabet.LATIN], "ĚěŘřŮů"),
            [Language.DANISH] = new(Api.IsoCode6391.DA, Api.IsoCode6393.DAN, [Alphabet.LATIN]),
            [Language.DUTCH] = new(Api.IsoCode6391.NL, Api.IsoCode6393.NLD, [Alphabet.LATIN]),
            [Language.ENGLISH] = new(Api.IsoCode6391.EN, Api.IsoCode6393.ENG, [Alphabet.LATIN]),
            [Language.ESPERANTO] = new(Api.IsoCode6391.EO, Api.IsoCode6393.EPO, [Alphabet.LATIN], "ĈĉĜĝĤĥĴĵŜŝŬŭ"),
            [Language.ESTONIAN] = new(Api.IsoCode6391.ET, Api.IsoCode6393.EST, [Alphabet.LATIN]),
            [Language.FINNISH] = new(Api.IsoCode6391.FI, Api.IsoCode6393.FIN, [Alphabet.LATIN]),
            [Language.FRENCH] = new(Api.IsoCode6391.FR, Api.IsoCode6393.FRA, [Alphabet.LATIN]),
            [Language.GANDA] = new(Api.IsoCode6391.LG, Api.IsoCode6393.LUG, [Alphabet.LATIN]),
            [Language.GEORGIAN] = new(Api.IsoCode6391.KA, Api.IsoCode6393.KAT, [Alphabet.GEORGIAN]),
            [Language.GERMAN] = new(Api.IsoCode6391.DE, Api.IsoCode6393.DEU, [Alphabet.LATIN], "ß"),
            [Language.GREEK] = new(Api.IsoCode6391.EL, Api.IsoCode6393.ELL, [Alphabet.GREEK]),
            [Language.GUJARATI] = new(Api.IsoCode6391.GU, Api.IsoCode6393.GUJ, [Alphabet.GUJARATI]),
            [Language.HEBREW] = new(Api.IsoCode6391.HE, Api.IsoCode6393.HEB, [Alphabet.HEBREW]),
            [Language.HINDI] = new(Api.IsoCode6391.HI, Api.IsoCode6393.HIN, [Alphabet.DEVANAGARI]),
            [Language.HUNGARIAN] = new(Api.IsoCode6391.HU, Api.IsoCode6393.HUN, [Alphabet.LATIN], "ŐőŰű"),
            [Language.ICELANDIC] = new(Api.IsoCode6391.IS, Api.IsoCode6393.ISL, [Alphabet.LATIN]),
            [Language.INDONESIAN] = new(Api.IsoCode6391.ID, Api.IsoCode6393.IND, [Alphabet.LATIN]),
            [Language.IRISH] = new(Api.IsoCode6391.GA, Api.IsoCode6393.GLE, [Alphabet.LATIN]),
            [Language.ITALIAN] = new(Api.IsoCode6391.IT, Api.IsoCode6393.ITA, [Alphabet.LATIN]),
            [Language.JAPANESE] = new(Api.IsoCode6391.JA, Api.IsoCode6393.JPN, [Alphabet.HIRAGANA, Alphabet.KATAKANA, Alphabet.HAN]),
            [Language.KAZAKH] = new(Api.IsoCode6391.KK, Api.IsoCode6393.KAZ, [Alphabet.CYRILLIC], "ӘәҒғҚқҢңҰұ"),
            [Language.KOREAN] = new(Api.IsoCode6391.KO, Api.IsoCode6393.KOR, [Alphabet.HANGUL]),
            [Language.LATIN] = new(Api.IsoCode6391.LA, Api.IsoCode6393.LAT, [Alphabet.LATIN]),
            [Language.LATVIAN] = new(Api.IsoCode6391.LV, Api.IsoCode6393.LAV, [Alphabet.LATIN], "ĢģĶķĻļŅņ"),
            [Language.LITHUANIAN] = new(Api.IsoCode6391.LT, Api.IsoCode6393.LIT, [Alphabet.LATIN], "ĖėĮįŲų"),
            [Language.MACEDONIAN] = new(Api.IsoCode6391.MK, Api.IsoCode6393.MKD, [Alphabet.CYRILLIC], "ЃѓЅѕЌќЏџ"),
            [Language.MALAY] = new(Api.IsoCode6391.MS, Api.IsoCode6393.MSA, [Alphabet.LATIN]),
            [Language.MAORI] = new(Api.IsoCode6391.MI, Api.IsoCode6393.MRI, [Alphabet.LATIN]),
            [Language.MARATHI] = new(Api.IsoCode6391.MR, Api.IsoCode6393.MAR, [Alphabet.DEVANAGARI], "ळ"),
            [Language.MONGOLIAN] = new(Api.IsoCode6391.MN, Api.IsoCode6393.MON, [Alphabet.CYRILLIC], "ӨөҮү"),
            [Language.NYNORSK] = new(Api.IsoCode6391.NN, Api.IsoCode6393.NNO, [Alphabet.LATIN]),
            [Language.OROMO] = new(Api.IsoCode6391.OM, Api.IsoCode6393.ORM, [Alphabet.LATIN]),
            [Language.PERSIAN] = new(Api.IsoCode6391.FA, Api.IsoCode6393.FAS, [Alphabet.ARABIC]),
            [Language.POLISH] = new(Api.IsoCode6391.PL, Api.IsoCode6393.POL, [Alphabet.LATIN], "ŁłŃńŚśŹź"),
            [Language.PORTUGUESE] = new(Api.IsoCode6391.PT, Api.IsoCode6393.POR, [Alphabet.LATIN]),
            [Language.PUNJABI] = new(Api.IsoCode6391.PA, Api.IsoCode6393.PAN, [Alphabet.GURMUKHI]),
            [Language.ROMANIAN] = new(Api.IsoCode6391.RO, Api.IsoCode6393.RON, [Alphabet.LATIN], "Țţ"),
            [Language.RUSSIAN] = new(Api.IsoCode6391.RU, Api.IsoCode6393.RUS, [Alphabet.CYRILLIC]),
            [Language.SERBIAN] = new(Api.IsoCode6391.SR, Api.IsoCode6393.SRP, [Alphabet.CYRILLIC], "ЂђЋћ"),
            [Language.SHONA] = new(Api.IsoCode6391.SN, Api.IsoCode6393.SNA, [Alphabet.LATIN]),
            [Language.SINHALA] = new(Api.IsoCode6391.SI, Api.IsoCode6393.SIN, [Alphabet.SINHALA]),
            [Language.SLOVAK] = new(Api.IsoCode6391.SK, Api.IsoCode6393.SLK, [Alphabet.LATIN], "ĹĺĽľŔŕ"),
            [Language.SLOVENE] = new(Api.IsoCode6391.SL, Api.IsoCode6393.SLV, [Alphabet.LATIN]),
            [Language.SOMALI] = new(Api.IsoCode6391.SO, Api.IsoCode6393.SOM, [Alphabet.LATIN]),
            [Language.SOTHO] = new(Api.IsoCode6391.ST, Api.IsoCode6393.SOT, [Alphabet.LATIN]),
            [Language.SPANISH] = new(Api.IsoCode6391.ES, Api.IsoCode6393.SPA, [Alphabet.LATIN], "¿¡"),
            [Language.SWAHILI] = new(Api.IsoCode6391.SW, Api.IsoCode6393.SWA, [Alphabet.LATIN]),
            [Language.SWEDISH] = new(Api.IsoCode6391.SV, Api.IsoCode6393.SWE, [Alphabet.LATIN]),
            [Language.TAGALOG] = new(Api.IsoCode6391.TL, Api.IsoCode6393.TGL, [Alphabet.LATIN]),
            [Language.TAMIL] = new(Api.IsoCode6391.TA, Api.IsoCode6393.TAM, [Alphabet.TAMIL]),
            [Language.TELUGU] = new(Api.IsoCode6391.TE, Api.IsoCode6393.TEL, [Alphabet.TELUGU]),
            [Language.THAI] = new(Api.IsoCode6391.TH, Api.IsoCode6393.THA, [Alphabet.THAI]),
            [Language.TIGRINYA] = new(Api.IsoCode6391.TI, Api.IsoCode6393.TIR, [Alphabet.ETHIOPIC]),
            [Language.TSONGA] = new(Api.IsoCode6391.TS, Api.IsoCode6393.TSO, [Alphabet.LATIN]),
            [Language.TSWANA] = new(Api.IsoCode6391.TN, Api.IsoCode6393.TSN, [Alphabet.LATIN]),
            [Language.TURKISH] = new(Api.IsoCode6391.TR, Api.IsoCode6393.TUR, [Alphabet.LATIN]),
            [Language.UKRAINIAN] = new(Api.IsoCode6391.UK, Api.IsoCode6393.UKR, [Alphabet.CYRILLIC], "ҐґЄєЇї"),
            [Language.URDU] = new(Api.IsoCode6391.UR, Api.IsoCode6393.URD, [Alphabet.ARABIC]),
            [Language.VIETNAMESE] = new(Api.IsoCode6391.VI, Api.IsoCode6393.VIE, [Alphabet.LATIN],"ẰằẦầẲẳẨẩẴẵẪẫẮắẤấẠạẶặẬậỀềẺẻỂểẼẽỄễẾếỆệỈỉĨĩỊịƠơỒồỜờỎỏỔổỞởỖỗỠỡỐốỚớỘộỢợƯưỪừỦủỬửŨũỮữỨứỤụỰựỲỳỶỷỸỹỴỵ"),
            [Language.WELSH] = new(Api.IsoCode6391.CY, Api.IsoCode6393.CYM, [Alphabet.LATIN]),
            [Language.XHOSA] = new(Api.IsoCode6391.XH, Api.IsoCode6393.XHO, [Alphabet.LATIN]),
            // TODO for YORUBA: "E̩e̩Ẹ́ẹ́É̩é̩Ẹ̀ẹ̀È̩è̩Ẹ̄ẹ̄Ē̩ē̩ŌōO̩o̩Ọ́ọ́Ó̩ó̩Ọ̀ọ̀Ò̩ò̩Ọ̄ọ̄Ō̩ō̩ṢṣS̩s̩"
            [Language.YORUBA] = new(Api.IsoCode6391.YO, Api.IsoCode6393.YOR, [Alphabet.LATIN], "Ṣṣ"),
            [Language.ZULU] = new(Api.IsoCode6391.ZU, Api.IsoCode6393.ZUL, [Alphabet.LATIN]),
            [Language.UNKNOWN] = new(Api.IsoCode6391.NONE, Api.IsoCode6393.NONE, [Alphabet.NONE])
        };

    public static IsoCode6391 IsoCode6391(this Language language) => LanguageProperties[language].IsoCode6391;
    
    public static IsoCode6393 IsoCode6393(this Language language) => LanguageProperties[language].IsoCode6393;

    public static IReadOnlySet<Alphabet> Alphabets(this Language language) => LanguageProperties[language].Alphabets;
    
    public static string? UniqueCharacters(this Language language) => LanguageProperties[language].UniqueCharacters;

    public static IList<Language> All() => LanguageProperties.Keys
        .Where(l => l is not Language.UNKNOWN).ToList();
    
    public static IList<Language> AllSpokenOnes() => LanguageProperties.Keys
        .Where(l => l is not (Language.UNKNOWN or Language.LATIN)).ToList();
    
    public static IList<Language> AllWithArabicScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.ARABIC))
        .Select(l => l.Key)
        .ToList();
    
    public static IList<Language> AllWithCyrillicScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.CYRILLIC))
        .Select(l => l.Key)
        .ToList();
    
    public static IList<Language> AllWithDevangariScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.DEVANAGARI))
        .Select(l => l.Key)
        .ToList();   
    
    public static IList<Language> AllWithEthiopicScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.ETHIOPIC))
        .Select(l => l.Key)
        .ToList();
    
    public static IList<Language> AllWithLatinScript() => LanguageProperties
        .Where(l => l.Value.Alphabets.Contains(Alphabet.LATIN))
        .Select(l => l.Key)
        .ToList();

    public static Language GetByIsoCode6391(IsoCode6391 isoCode6391) =>
        LanguageProperties.Single(l => l.Value.IsoCode6391 == isoCode6391).Key;
    
    public static Language GetByIsoCode6393(IsoCode6393 isoCode6393) =>
        LanguageProperties.Single(l => l.Value.IsoCode6393 == isoCode6393).Key;

    public static IReadOnlySet<Language> LanguagesSupportingLogograms =
        new HashSet<Language>{ Language.CHINESE, Language.JAPANESE, Language.KOREAN };
}

internal record LanguageProperties(IsoCode6391 IsoCode6391, IsoCode6393 IsoCode6393, HashSet<Alphabet> Alphabets, string? UniqueCharacters = null)
{
}