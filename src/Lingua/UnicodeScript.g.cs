using System.Globalization;
using static Lingua.UnicodeScript;

namespace Lingua;

///<summary>Unicode script information, version 15.1.0</summary>
public enum UnicodeScript
{
    ///<summary>Unicode script for "Common"</summary>
    Common,
    ///<summary>Unicode script for "Latin"</summary>
    Latin,
    ///<summary>Unicode script for "Greek"</summary>
    Greek,
    ///<summary>Unicode script for "Cyrillic"</summary>
    Cyrillic,
    ///<summary>Unicode script for "Armenian"</summary>
    Armenian,
    ///<summary>Unicode script for "Hebrew"</summary>
    Hebrew,
    ///<summary>Unicode script for "Arabic"</summary>
    Arabic,
    ///<summary>Unicode script for "Syriac"</summary>
    Syriac,
    ///<summary>Unicode script for "Thaana"</summary>
    Thaana,
    ///<summary>Unicode script for "Devanagari"</summary>
    Devanagari,
    ///<summary>Unicode script for "Bengali"</summary>
    Bengali,
    ///<summary>Unicode script for "Gurmukhi"</summary>
    Gurmukhi,
    ///<summary>Unicode script for "Gujarati"</summary>
    Gujarati,
    ///<summary>Unicode script for "Oriya"</summary>
    Oriya,
    ///<summary>Unicode script for "Tamil"</summary>
    Tamil,
    ///<summary>Unicode script for "Telugu"</summary>
    Telugu,
    ///<summary>Unicode script for "Kannada"</summary>
    Kannada,
    ///<summary>Unicode script for "Malayalam"</summary>
    Malayalam,
    ///<summary>Unicode script for "Sinhala"</summary>
    Sinhala,
    ///<summary>Unicode script for "Thai"</summary>
    Thai,
    ///<summary>Unicode script for "Lao"</summary>
    Lao,
    ///<summary>Unicode script for "Tibetan"</summary>
    Tibetan,
    ///<summary>Unicode script for "Myanmar"</summary>
    Myanmar,
    ///<summary>Unicode script for "Georgian"</summary>
    Georgian,
    ///<summary>Unicode script for "Hangul"</summary>
    Hangul,
    ///<summary>Unicode script for "Ethiopic"</summary>
    Ethiopic,
    ///<summary>Unicode script for "Cherokee"</summary>
    Cherokee,
    ///<summary>Unicode script for "Canadian Aboriginal"</summary>
    CanadianAboriginal,
    ///<summary>Unicode script for "Ogham"</summary>
    Ogham,
    ///<summary>Unicode script for "Runic"</summary>
    Runic,
    ///<summary>Unicode script for "Khmer"</summary>
    Khmer,
    ///<summary>Unicode script for "Mongolian"</summary>
    Mongolian,
    ///<summary>Unicode script for "Hiragana"</summary>
    Hiragana,
    ///<summary>Unicode script for "Katakana"</summary>
    Katakana,
    ///<summary>Unicode script for "Bopomofo"</summary>
    Bopomofo,
    ///<summary>Unicode script for "Han"</summary>
    Han,
    ///<summary>Unicode script for "Yi"</summary>
    Yi,
    ///<summary>Unicode script for "Old Italic"</summary>
    OldItalic,
    ///<summary>Unicode script for "Gothic"</summary>
    Gothic,
    ///<summary>Unicode script for "Deseret"</summary>
    Deseret,
    ///<summary>Unicode script for "Inherited"</summary>
    Inherited,
    ///<summary>Unicode script for "Tagalog"</summary>
    Tagalog,
    ///<summary>Unicode script for "Hanunoo"</summary>
    Hanunoo,
    ///<summary>Unicode script for "Buhid"</summary>
    Buhid,
    ///<summary>Unicode script for "Tagbanwa"</summary>
    Tagbanwa,
    ///<summary>Unicode script for "Limbu"</summary>
    Limbu,
    ///<summary>Unicode script for "Tai Le"</summary>
    TaiLe,
    ///<summary>Unicode script for "Linear B"</summary>
    LinearB,
    ///<summary>Unicode script for "Ugaritic"</summary>
    Ugaritic,
    ///<summary>Unicode script for "Shavian"</summary>
    Shavian,
    ///<summary>Unicode script for "Osmanya"</summary>
    Osmanya,
    ///<summary>Unicode script for "Cypriot"</summary>
    Cypriot,
    ///<summary>Unicode script for "Braille"</summary>
    Braille,
    ///<summary>Unicode script for "Buginese"</summary>
    Buginese,
    ///<summary>Unicode script for "Coptic"</summary>
    Coptic,
    ///<summary>Unicode script for "New Tai Lue"</summary>
    NewTaiLue,
    ///<summary>Unicode script for "Glagolitic"</summary>
    Glagolitic,
    ///<summary>Unicode script for "Tifinagh"</summary>
    Tifinagh,
    ///<summary>Unicode script for "Syloti Nagri"</summary>
    SylotiNagri,
    ///<summary>Unicode script for "Old Persian"</summary>
    OldPersian,
    ///<summary>Unicode script for "Kharoshthi"</summary>
    Kharoshthi,
    ///<summary>Unicode script for "Balinese"</summary>
    Balinese,
    ///<summary>Unicode script for "Cuneiform"</summary>
    Cuneiform,
    ///<summary>Unicode script for "Phoenician"</summary>
    Phoenician,
    ///<summary>Unicode script for "Phags Pa"</summary>
    PhagsPa,
    ///<summary>Unicode script for "Nko"</summary>
    Nko,
    ///<summary>Unicode script for "Sundanese"</summary>
    Sundanese,
    ///<summary>Unicode script for "Lepcha"</summary>
    Lepcha,
    ///<summary>Unicode script for "Ol Chiki"</summary>
    OlChiki,
    ///<summary>Unicode script for "Vai"</summary>
    Vai,
    ///<summary>Unicode script for "Saurashtra"</summary>
    Saurashtra,
    ///<summary>Unicode script for "Kayah Li"</summary>
    KayahLi,
    ///<summary>Unicode script for "Rejang"</summary>
    Rejang,
    ///<summary>Unicode script for "Lycian"</summary>
    Lycian,
    ///<summary>Unicode script for "Carian"</summary>
    Carian,
    ///<summary>Unicode script for "Lydian"</summary>
    Lydian,
    ///<summary>Unicode script for "Cham"</summary>
    Cham,
    ///<summary>Unicode script for "Tai Tham"</summary>
    TaiTham,
    ///<summary>Unicode script for "Tai Viet"</summary>
    TaiViet,
    ///<summary>Unicode script for "Avestan"</summary>
    Avestan,
    ///<summary>Unicode script for "Egyptian Hieroglyphs"</summary>
    EgyptianHieroglyphs,
    ///<summary>Unicode script for "Samaritan"</summary>
    Samaritan,
    ///<summary>Unicode script for "Lisu"</summary>
    Lisu,
    ///<summary>Unicode script for "Bamum"</summary>
    Bamum,
    ///<summary>Unicode script for "Javanese"</summary>
    Javanese,
    ///<summary>Unicode script for "Meetei Mayek"</summary>
    MeeteiMayek,
    ///<summary>Unicode script for "Imperial Aramaic"</summary>
    ImperialAramaic,
    ///<summary>Unicode script for "Old South Arabian"</summary>
    OldSouthArabian,
    ///<summary>Unicode script for "Inscriptional Parthian"</summary>
    InscriptionalParthian,
    ///<summary>Unicode script for "Inscriptional Pahlavi"</summary>
    InscriptionalPahlavi,
    ///<summary>Unicode script for "Old Turkic"</summary>
    OldTurkic,
    ///<summary>Unicode script for "Kaithi"</summary>
    Kaithi,
    ///<summary>Unicode script for "Batak"</summary>
    Batak,
    ///<summary>Unicode script for "Brahmi"</summary>
    Brahmi,
    ///<summary>Unicode script for "Mandaic"</summary>
    Mandaic,
    ///<summary>Unicode script for "Chakma"</summary>
    Chakma,
    ///<summary>Unicode script for "Meroitic Cursive"</summary>
    MeroiticCursive,
    ///<summary>Unicode script for "Meroitic Hieroglyphs"</summary>
    MeroiticHieroglyphs,
    ///<summary>Unicode script for "Miao"</summary>
    Miao,
    ///<summary>Unicode script for "Sharada"</summary>
    Sharada,
    ///<summary>Unicode script for "Sora Sompeng"</summary>
    SoraSompeng,
    ///<summary>Unicode script for "Takri"</summary>
    Takri,
    ///<summary>Unicode script for "Caucasian Albanian"</summary>
    CaucasianAlbanian,
    ///<summary>Unicode script for "Bassa Vah"</summary>
    BassaVah,
    ///<summary>Unicode script for "Duployan"</summary>
    Duployan,
    ///<summary>Unicode script for "Elbasan"</summary>
    Elbasan,
    ///<summary>Unicode script for "Grantha"</summary>
    Grantha,
    ///<summary>Unicode script for "Pahawh Hmong"</summary>
    PahawhHmong,
    ///<summary>Unicode script for "Khojki"</summary>
    Khojki,
    ///<summary>Unicode script for "Linear A"</summary>
    LinearA,
    ///<summary>Unicode script for "Mahajani"</summary>
    Mahajani,
    ///<summary>Unicode script for "Manichaean"</summary>
    Manichaean,
    ///<summary>Unicode script for "Mende Kikakui"</summary>
    MendeKikakui,
    ///<summary>Unicode script for "Modi"</summary>
    Modi,
    ///<summary>Unicode script for "Mro"</summary>
    Mro,
    ///<summary>Unicode script for "Old North Arabian"</summary>
    OldNorthArabian,
    ///<summary>Unicode script for "Nabataean"</summary>
    Nabataean,
    ///<summary>Unicode script for "Palmyrene"</summary>
    Palmyrene,
    ///<summary>Unicode script for "Pau Cin Hau"</summary>
    PauCinHau,
    ///<summary>Unicode script for "Old Permic"</summary>
    OldPermic,
    ///<summary>Unicode script for "Psalter Pahlavi"</summary>
    PsalterPahlavi,
    ///<summary>Unicode script for "Siddham"</summary>
    Siddham,
    ///<summary>Unicode script for "Khudawadi"</summary>
    Khudawadi,
    ///<summary>Unicode script for "Tirhuta"</summary>
    Tirhuta,
    ///<summary>Unicode script for "Warang Citi"</summary>
    WarangCiti,
    ///<summary>Unicode script for "Ahom"</summary>
    Ahom,
    ///<summary>Unicode script for "Anatolian Hieroglyphs"</summary>
    AnatolianHieroglyphs,
    ///<summary>Unicode script for "Hatran"</summary>
    Hatran,
    ///<summary>Unicode script for "Multani"</summary>
    Multani,
    ///<summary>Unicode script for "Old Hungarian"</summary>
    OldHungarian,
    ///<summary>Unicode script for "SignWriting"</summary>
    SignWriting,
    ///<summary>Unicode script for "Adlam"</summary>
    Adlam,
    ///<summary>Unicode script for "Bhaiksuki"</summary>
    Bhaiksuki,
    ///<summary>Unicode script for "Marchen"</summary>
    Marchen,
    ///<summary>Unicode script for "Newa"</summary>
    Newa,
    ///<summary>Unicode script for "Osage"</summary>
    Osage,
    ///<summary>Unicode script for "Tangut"</summary>
    Tangut,
    ///<summary>Unicode script for "Masaram Gondi"</summary>
    MasaramGondi,
    ///<summary>Unicode script for "Nushu"</summary>
    Nushu,
    ///<summary>Unicode script for "Soyombo"</summary>
    Soyombo,
    ///<summary>Unicode script for "Zanabazar Square"</summary>
    ZanabazarSquare,
    ///<summary>Unicode script for "Dogra"</summary>
    Dogra,
    ///<summary>Unicode script for "Gunjala Gondi"</summary>
    GunjalaGondi,
    ///<summary>Unicode script for "Makasar"</summary>
    Makasar,
    ///<summary>Unicode script for "Medefaidrin"</summary>
    Medefaidrin,
    ///<summary>Unicode script for "Hanifi Rohingya"</summary>
    HanifiRohingya,
    ///<summary>Unicode script for "Sogdian"</summary>
    Sogdian,
    ///<summary>Unicode script for "Old Sogdian"</summary>
    OldSogdian,
    ///<summary>Unicode script for "Elymaic"</summary>
    Elymaic,
    ///<summary>Unicode script for "Nandinagari"</summary>
    Nandinagari,
    ///<summary>Unicode script for "Nyiakeng Puachue Hmong"</summary>
    NyiakengPuachueHmong,
    ///<summary>Unicode script for "Wancho"</summary>
    Wancho,
    ///<summary>Unicode script for "Chorasmian"</summary>
    Chorasmian,
    ///<summary>Unicode script for "Dives Akuru"</summary>
    DivesAkuru,
    ///<summary>Unicode script for "Khitan Small Script"</summary>
    KhitanSmallScript,
    ///<summary>Unicode script for "Yezidi"</summary>
    Yezidi,
    ///<summary>Unicode script for "Cypro Minoan"</summary>
    CyproMinoan,
    ///<summary>Unicode script for "Old Uyghur"</summary>
    OldUyghur,
    ///<summary>Unicode script for "Tangsa"</summary>
    Tangsa,
    ///<summary>Unicode script for "Toto"</summary>
    Toto,
    ///<summary>Unicode script for "Vithkuqi"</summary>
    Vithkuqi,
    ///<summary>Unicode script for "Kawi"</summary>
    Kawi,
    ///<summary>Unicode script for "Nag Mundari"</summary>
    NagMundari,
    ///<summary>Unicode script for "Unknown"</summary>
    Unknown,
}

/// <summary>
/// Extension method for <see cref="char"/> to determine its <see cref="UnicodeScript"/> property.
/// </summary>
public static class UnicodeScriptInfo
{
    private static readonly int[] ScriptStarts =
    {
        0x0000, // 0000..0040; Common
        0x0041, // 0041..005A; Latin
        0x005B, // 005B..0060; Common
        0x0061, // 0061..007A; Latin
        0x007B, // 007B..00A9; Common
        0x00AA, // 00AA; Latin
        0x00AB, // 00AB..00B9; Common
        0x00BA, // 00BA; Latin
        0x00BB, // 00BB..00BF; Common
        0x00C0, // 00C0..00D6; Latin
        0x00D7, // 00D7; Common
        0x00D8, // 00D8..00F6; Latin
        0x00F7, // 00F7; Common
        0x00F8, // 00F8..02B8; Latin
        0x02B9, // 02B9..02DF; Common
        0x02E0, // 02E0..02E4; Latin
        0x02E5, // 02E5..02E9; Common
        0x02EA, // 02EA..02EB; Bopomofo
        0x02EC, // 02EC..02FF; Common
        0x0300, // 0300..036F; Inherited
        0x0370, // 0370..0373; Greek
        0x0374, // 0374; Common
        0x0375, // 0375..0377; Greek
        0x0378, // 0378..0379; Unknown
        0x037A, // 037A..037D; Greek
        0x037E, // 037E; Common
        0x037F, // 037F; Greek
        0x0380, // 0380..0383; Unknown
        0x0384, // 0384; Greek
        0x0385, // 0385; Common
        0x0386, // 0386; Greek
        0x0387, // 0387; Common
        0x0388, // 0388..038A; Greek
        0x038B, // 038B; Unknown
        0x038C, // 038C; Greek
        0x038D, // 038D; Unknown
        0x038E, // 038E..03A1; Greek
        0x03A2, // 03A2; Unknown
        0x03A3, // 03A3..03E1; Greek
        0x03E2, // 03E2..03EF; Coptic
        0x03F0, // 03F0..03FF; Greek
        0x0400, // 0400..0484; Cyrillic
        0x0485, // 0485..0486; Inherited
        0x0487, // 0487..052F; Cyrillic
        0x0530, // 0530; Unknown
        0x0531, // 0531..0556; Armenian
        0x0557, // 0557..0558; Unknown
        0x0559, // 0559..058A; Armenian
        0x058B, // 058B..058C; Unknown
        0x058D, // 058D..058F; Armenian
        0x0590, // 0590; Unknown
        0x0591, // 0591..05C7; Hebrew
        0x05C8, // 05C8..05CF; Unknown
        0x05D0, // 05D0..05EA; Hebrew
        0x05EB, // 05EB..05EE; Unknown
        0x05EF, // 05EF..05F4; Hebrew
        0x05F5, // 05F5..05FF; Unknown
        0x0600, // 0600..0604; Arabic
        0x0605, // 0605; Common
        0x0606, // 0606..060B; Arabic
        0x060C, // 060C; Common
        0x060D, // 060D..061A; Arabic
        0x061B, // 061B; Common
        0x061C, // 061C..061E; Arabic
        0x061F, // 061F; Common
        0x0620, // 0620..063F; Arabic
        0x0640, // 0640; Common
        0x0641, // 0641..064A; Arabic
        0x064B, // 064B..0655; Inherited
        0x0656, // 0656..066F; Arabic
        0x0670, // 0670; Inherited
        0x0671, // 0671..06DC; Arabic
        0x06DD, // 06DD; Common
        0x06DE, // 06DE..06FF; Arabic
        0x0700, // 0700..070D; Syriac
        0x070E, // 070E; Unknown
        0x070F, // 070F..074A; Syriac
        0x074B, // 074B..074C; Unknown
        0x074D, // 074D..074F; Syriac
        0x0750, // 0750..077F; Arabic
        0x0780, // 0780..07B1; Thaana
        0x07B2, // 07B2..07BF; Unknown
        0x07C0, // 07C0..07FA; Nko
        0x07FB, // 07FB..07FC; Unknown
        0x07FD, // 07FD..07FF; Nko
        0x0800, // 0800..082D; Samaritan
        0x082E, // 082E..082F; Unknown
        0x0830, // 0830..083E; Samaritan
        0x083F, // 083F; Unknown
        0x0840, // 0840..085B; Mandaic
        0x085C, // 085C..085D; Unknown
        0x085E, // 085E; Mandaic
        0x085F, // 085F; Unknown
        0x0860, // 0860..086A; Syriac
        0x086B, // 086B..086F; Unknown
        0x0870, // 0870..088E; Arabic
        0x088F, // 088F; Unknown
        0x0890, // 0890..0891; Arabic
        0x0892, // 0892..0897; Unknown
        0x0898, // 0898..08E1; Arabic
        0x08E2, // 08E2; Common
        0x08E3, // 08E3..08FF; Arabic
        0x0900, // 0900..0950; Devanagari
        0x0951, // 0951..0954; Inherited
        0x0955, // 0955..0963; Devanagari
        0x0964, // 0964..0965; Common
        0x0966, // 0966..097F; Devanagari
        0x0980, // 0980..0983; Bengali
        0x0984, // 0984; Unknown
        0x0985, // 0985..098C; Bengali
        0x098D, // 098D..098E; Unknown
        0x098F, // 098F..0990; Bengali
        0x0991, // 0991..0992; Unknown
        0x0993, // 0993..09A8; Bengali
        0x09A9, // 09A9; Unknown
        0x09AA, // 09AA..09B0; Bengali
        0x09B1, // 09B1; Unknown
        0x09B2, // 09B2; Bengali
        0x09B3, // 09B3..09B5; Unknown
        0x09B6, // 09B6..09B9; Bengali
        0x09BA, // 09BA..09BB; Unknown
        0x09BC, // 09BC..09C4; Bengali
        0x09C5, // 09C5..09C6; Unknown
        0x09C7, // 09C7..09C8; Bengali
        0x09C9, // 09C9..09CA; Unknown
        0x09CB, // 09CB..09CE; Bengali
        0x09CF, // 09CF..09D6; Unknown
        0x09D7, // 09D7; Bengali
        0x09D8, // 09D8..09DB; Unknown
        0x09DC, // 09DC..09DD; Bengali
        0x09DE, // 09DE; Unknown
        0x09DF, // 09DF..09E3; Bengali
        0x09E4, // 09E4..09E5; Unknown
        0x09E6, // 09E6..09FE; Bengali
        0x09FF, // 09FF..0A00; Unknown
        0x0A01, // 0A01..0A03; Gurmukhi
        0x0A04, // 0A04; Unknown
        0x0A05, // 0A05..0A0A; Gurmukhi
        0x0A0B, // 0A0B..0A0E; Unknown
        0x0A0F, // 0A0F..0A10; Gurmukhi
        0x0A11, // 0A11..0A12; Unknown
        0x0A13, // 0A13..0A28; Gurmukhi
        0x0A29, // 0A29; Unknown
        0x0A2A, // 0A2A..0A30; Gurmukhi
        0x0A31, // 0A31; Unknown
        0x0A32, // 0A32..0A33; Gurmukhi
        0x0A34, // 0A34; Unknown
        0x0A35, // 0A35..0A36; Gurmukhi
        0x0A37, // 0A37; Unknown
        0x0A38, // 0A38..0A39; Gurmukhi
        0x0A3A, // 0A3A..0A3B; Unknown
        0x0A3C, // 0A3C; Gurmukhi
        0x0A3D, // 0A3D; Unknown
        0x0A3E, // 0A3E..0A42; Gurmukhi
        0x0A43, // 0A43..0A46; Unknown
        0x0A47, // 0A47..0A48; Gurmukhi
        0x0A49, // 0A49..0A4A; Unknown
        0x0A4B, // 0A4B..0A4D; Gurmukhi
        0x0A4E, // 0A4E..0A50; Unknown
        0x0A51, // 0A51; Gurmukhi
        0x0A52, // 0A52..0A58; Unknown
        0x0A59, // 0A59..0A5C; Gurmukhi
        0x0A5D, // 0A5D; Unknown
        0x0A5E, // 0A5E; Gurmukhi
        0x0A5F, // 0A5F..0A65; Unknown
        0x0A66, // 0A66..0A76; Gurmukhi
        0x0A77, // 0A77..0A80; Unknown
        0x0A81, // 0A81..0A83; Gujarati
        0x0A84, // 0A84; Unknown
        0x0A85, // 0A85..0A8D; Gujarati
        0x0A8E, // 0A8E; Unknown
        0x0A8F, // 0A8F..0A91; Gujarati
        0x0A92, // 0A92; Unknown
        0x0A93, // 0A93..0AA8; Gujarati
        0x0AA9, // 0AA9; Unknown
        0x0AAA, // 0AAA..0AB0; Gujarati
        0x0AB1, // 0AB1; Unknown
        0x0AB2, // 0AB2..0AB3; Gujarati
        0x0AB4, // 0AB4; Unknown
        0x0AB5, // 0AB5..0AB9; Gujarati
        0x0ABA, // 0ABA..0ABB; Unknown
        0x0ABC, // 0ABC..0AC5; Gujarati
        0x0AC6, // 0AC6; Unknown
        0x0AC7, // 0AC7..0AC9; Gujarati
        0x0ACA, // 0ACA; Unknown
        0x0ACB, // 0ACB..0ACD; Gujarati
        0x0ACE, // 0ACE..0ACF; Unknown
        0x0AD0, // 0AD0; Gujarati
        0x0AD1, // 0AD1..0ADF; Unknown
        0x0AE0, // 0AE0..0AE3; Gujarati
        0x0AE4, // 0AE4..0AE5; Unknown
        0x0AE6, // 0AE6..0AF1; Gujarati
        0x0AF2, // 0AF2..0AF8; Unknown
        0x0AF9, // 0AF9..0AFF; Gujarati
        0x0B00, // 0B00; Unknown
        0x0B01, // 0B01..0B03; Oriya
        0x0B04, // 0B04; Unknown
        0x0B05, // 0B05..0B0C; Oriya
        0x0B0D, // 0B0D..0B0E; Unknown
        0x0B0F, // 0B0F..0B10; Oriya
        0x0B11, // 0B11..0B12; Unknown
        0x0B13, // 0B13..0B28; Oriya
        0x0B29, // 0B29; Unknown
        0x0B2A, // 0B2A..0B30; Oriya
        0x0B31, // 0B31; Unknown
        0x0B32, // 0B32..0B33; Oriya
        0x0B34, // 0B34; Unknown
        0x0B35, // 0B35..0B39; Oriya
        0x0B3A, // 0B3A..0B3B; Unknown
        0x0B3C, // 0B3C..0B44; Oriya
        0x0B45, // 0B45..0B46; Unknown
        0x0B47, // 0B47..0B48; Oriya
        0x0B49, // 0B49..0B4A; Unknown
        0x0B4B, // 0B4B..0B4D; Oriya
        0x0B4E, // 0B4E..0B54; Unknown
        0x0B55, // 0B55..0B57; Oriya
        0x0B58, // 0B58..0B5B; Unknown
        0x0B5C, // 0B5C..0B5D; Oriya
        0x0B5E, // 0B5E; Unknown
        0x0B5F, // 0B5F..0B63; Oriya
        0x0B64, // 0B64..0B65; Unknown
        0x0B66, // 0B66..0B77; Oriya
        0x0B78, // 0B78..0B81; Unknown
        0x0B82, // 0B82..0B83; Tamil
        0x0B84, // 0B84; Unknown
        0x0B85, // 0B85..0B8A; Tamil
        0x0B8B, // 0B8B..0B8D; Unknown
        0x0B8E, // 0B8E..0B90; Tamil
        0x0B91, // 0B91; Unknown
        0x0B92, // 0B92..0B95; Tamil
        0x0B96, // 0B96..0B98; Unknown
        0x0B99, // 0B99..0B9A; Tamil
        0x0B9B, // 0B9B; Unknown
        0x0B9C, // 0B9C; Tamil
        0x0B9D, // 0B9D; Unknown
        0x0B9E, // 0B9E..0B9F; Tamil
        0x0BA0, // 0BA0..0BA2; Unknown
        0x0BA3, // 0BA3..0BA4; Tamil
        0x0BA5, // 0BA5..0BA7; Unknown
        0x0BA8, // 0BA8..0BAA; Tamil
        0x0BAB, // 0BAB..0BAD; Unknown
        0x0BAE, // 0BAE..0BB9; Tamil
        0x0BBA, // 0BBA..0BBD; Unknown
        0x0BBE, // 0BBE..0BC2; Tamil
        0x0BC3, // 0BC3..0BC5; Unknown
        0x0BC6, // 0BC6..0BC8; Tamil
        0x0BC9, // 0BC9; Unknown
        0x0BCA, // 0BCA..0BCD; Tamil
        0x0BCE, // 0BCE..0BCF; Unknown
        0x0BD0, // 0BD0; Tamil
        0x0BD1, // 0BD1..0BD6; Unknown
        0x0BD7, // 0BD7; Tamil
        0x0BD8, // 0BD8..0BE5; Unknown
        0x0BE6, // 0BE6..0BFA; Tamil
        0x0BFB, // 0BFB..0BFF; Unknown
        0x0C00, // 0C00..0C0C; Telugu
        0x0C0D, // 0C0D; Unknown
        0x0C0E, // 0C0E..0C10; Telugu
        0x0C11, // 0C11; Unknown
        0x0C12, // 0C12..0C28; Telugu
        0x0C29, // 0C29; Unknown
        0x0C2A, // 0C2A..0C39; Telugu
        0x0C3A, // 0C3A..0C3B; Unknown
        0x0C3C, // 0C3C..0C44; Telugu
        0x0C45, // 0C45; Unknown
        0x0C46, // 0C46..0C48; Telugu
        0x0C49, // 0C49; Unknown
        0x0C4A, // 0C4A..0C4D; Telugu
        0x0C4E, // 0C4E..0C54; Unknown
        0x0C55, // 0C55..0C56; Telugu
        0x0C57, // 0C57; Unknown
        0x0C58, // 0C58..0C5A; Telugu
        0x0C5B, // 0C5B..0C5C; Unknown
        0x0C5D, // 0C5D; Telugu
        0x0C5E, // 0C5E..0C5F; Unknown
        0x0C60, // 0C60..0C63; Telugu
        0x0C64, // 0C64..0C65; Unknown
        0x0C66, // 0C66..0C6F; Telugu
        0x0C70, // 0C70..0C76; Unknown
        0x0C77, // 0C77..0C7F; Telugu
        0x0C80, // 0C80..0C8C; Kannada
        0x0C8D, // 0C8D; Unknown
        0x0C8E, // 0C8E..0C90; Kannada
        0x0C91, // 0C91; Unknown
        0x0C92, // 0C92..0CA8; Kannada
        0x0CA9, // 0CA9; Unknown
        0x0CAA, // 0CAA..0CB3; Kannada
        0x0CB4, // 0CB4; Unknown
        0x0CB5, // 0CB5..0CB9; Kannada
        0x0CBA, // 0CBA..0CBB; Unknown
        0x0CBC, // 0CBC..0CC4; Kannada
        0x0CC5, // 0CC5; Unknown
        0x0CC6, // 0CC6..0CC8; Kannada
        0x0CC9, // 0CC9; Unknown
        0x0CCA, // 0CCA..0CCD; Kannada
        0x0CCE, // 0CCE..0CD4; Unknown
        0x0CD5, // 0CD5..0CD6; Kannada
        0x0CD7, // 0CD7..0CDC; Unknown
        0x0CDD, // 0CDD..0CDE; Kannada
        0x0CDF, // 0CDF; Unknown
        0x0CE0, // 0CE0..0CE3; Kannada
        0x0CE4, // 0CE4..0CE5; Unknown
        0x0CE6, // 0CE6..0CEF; Kannada
        0x0CF0, // 0CF0; Unknown
        0x0CF1, // 0CF1..0CF3; Kannada
        0x0CF4, // 0CF4..0CFF; Unknown
        0x0D00, // 0D00..0D0C; Malayalam
        0x0D0D, // 0D0D; Unknown
        0x0D0E, // 0D0E..0D10; Malayalam
        0x0D11, // 0D11; Unknown
        0x0D12, // 0D12..0D44; Malayalam
        0x0D45, // 0D45; Unknown
        0x0D46, // 0D46..0D48; Malayalam
        0x0D49, // 0D49; Unknown
        0x0D4A, // 0D4A..0D4F; Malayalam
        0x0D50, // 0D50..0D53; Unknown
        0x0D54, // 0D54..0D63; Malayalam
        0x0D64, // 0D64..0D65; Unknown
        0x0D66, // 0D66..0D7F; Malayalam
        0x0D80, // 0D80; Unknown
        0x0D81, // 0D81..0D83; Sinhala
        0x0D84, // 0D84; Unknown
        0x0D85, // 0D85..0D96; Sinhala
        0x0D97, // 0D97..0D99; Unknown
        0x0D9A, // 0D9A..0DB1; Sinhala
        0x0DB2, // 0DB2; Unknown
        0x0DB3, // 0DB3..0DBB; Sinhala
        0x0DBC, // 0DBC; Unknown
        0x0DBD, // 0DBD; Sinhala
        0x0DBE, // 0DBE..0DBF; Unknown
        0x0DC0, // 0DC0..0DC6; Sinhala
        0x0DC7, // 0DC7..0DC9; Unknown
        0x0DCA, // 0DCA; Sinhala
        0x0DCB, // 0DCB..0DCE; Unknown
        0x0DCF, // 0DCF..0DD4; Sinhala
        0x0DD5, // 0DD5; Unknown
        0x0DD6, // 0DD6; Sinhala
        0x0DD7, // 0DD7; Unknown
        0x0DD8, // 0DD8..0DDF; Sinhala
        0x0DE0, // 0DE0..0DE5; Unknown
        0x0DE6, // 0DE6..0DEF; Sinhala
        0x0DF0, // 0DF0..0DF1; Unknown
        0x0DF2, // 0DF2..0DF4; Sinhala
        0x0DF5, // 0DF5..0E00; Unknown
        0x0E01, // 0E01..0E3A; Thai
        0x0E3B, // 0E3B..0E3E; Unknown
        0x0E3F, // 0E3F; Common
        0x0E40, // 0E40..0E5B; Thai
        0x0E5C, // 0E5C..0E80; Unknown
        0x0E81, // 0E81..0E82; Lao
        0x0E83, // 0E83; Unknown
        0x0E84, // 0E84; Lao
        0x0E85, // 0E85; Unknown
        0x0E86, // 0E86..0E8A; Lao
        0x0E8B, // 0E8B; Unknown
        0x0E8C, // 0E8C..0EA3; Lao
        0x0EA4, // 0EA4; Unknown
        0x0EA5, // 0EA5; Lao
        0x0EA6, // 0EA6; Unknown
        0x0EA7, // 0EA7..0EBD; Lao
        0x0EBE, // 0EBE..0EBF; Unknown
        0x0EC0, // 0EC0..0EC4; Lao
        0x0EC5, // 0EC5; Unknown
        0x0EC6, // 0EC6; Lao
        0x0EC7, // 0EC7; Unknown
        0x0EC8, // 0EC8..0ECE; Lao
        0x0ECF, // 0ECF; Unknown
        0x0ED0, // 0ED0..0ED9; Lao
        0x0EDA, // 0EDA..0EDB; Unknown
        0x0EDC, // 0EDC..0EDF; Lao
        0x0EE0, // 0EE0..0EFF; Unknown
        0x0F00, // 0F00..0F47; Tibetan
        0x0F48, // 0F48; Unknown
        0x0F49, // 0F49..0F6C; Tibetan
        0x0F6D, // 0F6D..0F70; Unknown
        0x0F71, // 0F71..0F97; Tibetan
        0x0F98, // 0F98; Unknown
        0x0F99, // 0F99..0FBC; Tibetan
        0x0FBD, // 0FBD; Unknown
        0x0FBE, // 0FBE..0FCC; Tibetan
        0x0FCD, // 0FCD; Unknown
        0x0FCE, // 0FCE..0FD4; Tibetan
        0x0FD5, // 0FD5..0FD8; Common
        0x0FD9, // 0FD9..0FDA; Tibetan
        0x0FDB, // 0FDB..0FFF; Unknown
        0x1000, // 1000..109F; Myanmar
        0x10A0, // 10A0..10C5; Georgian
        0x10C6, // 10C6; Unknown
        0x10C7, // 10C7; Georgian
        0x10C8, // 10C8..10CC; Unknown
        0x10CD, // 10CD; Georgian
        0x10CE, // 10CE..10CF; Unknown
        0x10D0, // 10D0..10FA; Georgian
        0x10FB, // 10FB; Common
        0x10FC, // 10FC..10FF; Georgian
        0x1100, // 1100..11FF; Hangul
        0x1200, // 1200..1248; Ethiopic
        0x1249, // 1249; Unknown
        0x124A, // 124A..124D; Ethiopic
        0x124E, // 124E..124F; Unknown
        0x1250, // 1250..1256; Ethiopic
        0x1257, // 1257; Unknown
        0x1258, // 1258; Ethiopic
        0x1259, // 1259; Unknown
        0x125A, // 125A..125D; Ethiopic
        0x125E, // 125E..125F; Unknown
        0x1260, // 1260..1288; Ethiopic
        0x1289, // 1289; Unknown
        0x128A, // 128A..128D; Ethiopic
        0x128E, // 128E..128F; Unknown
        0x1290, // 1290..12B0; Ethiopic
        0x12B1, // 12B1; Unknown
        0x12B2, // 12B2..12B5; Ethiopic
        0x12B6, // 12B6..12B7; Unknown
        0x12B8, // 12B8..12BE; Ethiopic
        0x12BF, // 12BF; Unknown
        0x12C0, // 12C0; Ethiopic
        0x12C1, // 12C1; Unknown
        0x12C2, // 12C2..12C5; Ethiopic
        0x12C6, // 12C6..12C7; Unknown
        0x12C8, // 12C8..12D6; Ethiopic
        0x12D7, // 12D7; Unknown
        0x12D8, // 12D8..1310; Ethiopic
        0x1311, // 1311; Unknown
        0x1312, // 1312..1315; Ethiopic
        0x1316, // 1316..1317; Unknown
        0x1318, // 1318..135A; Ethiopic
        0x135B, // 135B..135C; Unknown
        0x135D, // 135D..137C; Ethiopic
        0x137D, // 137D..137F; Unknown
        0x1380, // 1380..1399; Ethiopic
        0x139A, // 139A..139F; Unknown
        0x13A0, // 13A0..13F5; Cherokee
        0x13F6, // 13F6..13F7; Unknown
        0x13F8, // 13F8..13FD; Cherokee
        0x13FE, // 13FE..13FF; Unknown
        0x1400, // 1400..167F; Canadian_Aboriginal
        0x1680, // 1680..169C; Ogham
        0x169D, // 169D..169F; Unknown
        0x16A0, // 16A0..16EA; Runic
        0x16EB, // 16EB..16ED; Common
        0x16EE, // 16EE..16F8; Runic
        0x16F9, // 16F9..16FF; Unknown
        0x1700, // 1700..1715; Tagalog
        0x1716, // 1716..171E; Unknown
        0x171F, // 171F; Tagalog
        0x1720, // 1720..1734; Hanunoo
        0x1735, // 1735..1736; Common
        0x1737, // 1737..173F; Unknown
        0x1740, // 1740..1753; Buhid
        0x1754, // 1754..175F; Unknown
        0x1760, // 1760..176C; Tagbanwa
        0x176D, // 176D; Unknown
        0x176E, // 176E..1770; Tagbanwa
        0x1771, // 1771; Unknown
        0x1772, // 1772..1773; Tagbanwa
        0x1774, // 1774..177F; Unknown
        0x1780, // 1780..17DD; Khmer
        0x17DE, // 17DE..17DF; Unknown
        0x17E0, // 17E0..17E9; Khmer
        0x17EA, // 17EA..17EF; Unknown
        0x17F0, // 17F0..17F9; Khmer
        0x17FA, // 17FA..17FF; Unknown
        0x1800, // 1800..1801; Mongolian
        0x1802, // 1802..1803; Common
        0x1804, // 1804; Mongolian
        0x1805, // 1805; Common
        0x1806, // 1806..1819; Mongolian
        0x181A, // 181A..181F; Unknown
        0x1820, // 1820..1878; Mongolian
        0x1879, // 1879..187F; Unknown
        0x1880, // 1880..18AA; Mongolian
        0x18AB, // 18AB..18AF; Unknown
        0x18B0, // 18B0..18F5; Canadian_Aboriginal
        0x18F6, // 18F6..18FF; Unknown
        0x1900, // 1900..191E; Limbu
        0x191F, // 191F; Unknown
        0x1920, // 1920..192B; Limbu
        0x192C, // 192C..192F; Unknown
        0x1930, // 1930..193B; Limbu
        0x193C, // 193C..193F; Unknown
        0x1940, // 1940; Limbu
        0x1941, // 1941..1943; Unknown
        0x1944, // 1944..194F; Limbu
        0x1950, // 1950..196D; Tai_Le
        0x196E, // 196E..196F; Unknown
        0x1970, // 1970..1974; Tai_Le
        0x1975, // 1975..197F; Unknown
        0x1980, // 1980..19AB; New_Tai_Lue
        0x19AC, // 19AC..19AF; Unknown
        0x19B0, // 19B0..19C9; New_Tai_Lue
        0x19CA, // 19CA..19CF; Unknown
        0x19D0, // 19D0..19DA; New_Tai_Lue
        0x19DB, // 19DB..19DD; Unknown
        0x19DE, // 19DE..19DF; New_Tai_Lue
        0x19E0, // 19E0..19FF; Khmer
        0x1A00, // 1A00..1A1B; Buginese
        0x1A1C, // 1A1C..1A1D; Unknown
        0x1A1E, // 1A1E..1A1F; Buginese
        0x1A20, // 1A20..1A5E; Tai_Tham
        0x1A5F, // 1A5F; Unknown
        0x1A60, // 1A60..1A7C; Tai_Tham
        0x1A7D, // 1A7D..1A7E; Unknown
        0x1A7F, // 1A7F..1A89; Tai_Tham
        0x1A8A, // 1A8A..1A8F; Unknown
        0x1A90, // 1A90..1A99; Tai_Tham
        0x1A9A, // 1A9A..1A9F; Unknown
        0x1AA0, // 1AA0..1AAD; Tai_Tham
        0x1AAE, // 1AAE..1AAF; Unknown
        0x1AB0, // 1AB0..1ACE; Inherited
        0x1ACF, // 1ACF..1AFF; Unknown
        0x1B00, // 1B00..1B4C; Balinese
        0x1B4D, // 1B4D..1B4F; Unknown
        0x1B50, // 1B50..1B7E; Balinese
        0x1B7F, // 1B7F; Unknown
        0x1B80, // 1B80..1BBF; Sundanese
        0x1BC0, // 1BC0..1BF3; Batak
        0x1BF4, // 1BF4..1BFB; Unknown
        0x1BFC, // 1BFC..1BFF; Batak
        0x1C00, // 1C00..1C37; Lepcha
        0x1C38, // 1C38..1C3A; Unknown
        0x1C3B, // 1C3B..1C49; Lepcha
        0x1C4A, // 1C4A..1C4C; Unknown
        0x1C4D, // 1C4D..1C4F; Lepcha
        0x1C50, // 1C50..1C7F; Ol_Chiki
        0x1C80, // 1C80..1C88; Cyrillic
        0x1C89, // 1C89..1C8F; Unknown
        0x1C90, // 1C90..1CBA; Georgian
        0x1CBB, // 1CBB..1CBC; Unknown
        0x1CBD, // 1CBD..1CBF; Georgian
        0x1CC0, // 1CC0..1CC7; Sundanese
        0x1CC8, // 1CC8..1CCF; Unknown
        0x1CD0, // 1CD0..1CD2; Inherited
        0x1CD3, // 1CD3; Common
        0x1CD4, // 1CD4..1CE0; Inherited
        0x1CE1, // 1CE1; Common
        0x1CE2, // 1CE2..1CE8; Inherited
        0x1CE9, // 1CE9..1CEC; Common
        0x1CED, // 1CED; Inherited
        0x1CEE, // 1CEE..1CF3; Common
        0x1CF4, // 1CF4; Inherited
        0x1CF5, // 1CF5..1CF7; Common
        0x1CF8, // 1CF8..1CF9; Inherited
        0x1CFA, // 1CFA; Common
        0x1CFB, // 1CFB..1CFF; Unknown
        0x1D00, // 1D00..1D25; Latin
        0x1D26, // 1D26..1D2A; Greek
        0x1D2B, // 1D2B; Cyrillic
        0x1D2C, // 1D2C..1D5C; Latin
        0x1D5D, // 1D5D..1D61; Greek
        0x1D62, // 1D62..1D65; Latin
        0x1D66, // 1D66..1D6A; Greek
        0x1D6B, // 1D6B..1D77; Latin
        0x1D78, // 1D78; Cyrillic
        0x1D79, // 1D79..1DBE; Latin
        0x1DBF, // 1DBF; Greek
        0x1DC0, // 1DC0..1DFF; Inherited
        0x1E00, // 1E00..1EFF; Latin
        0x1F00, // 1F00..1F15; Greek
        0x1F16, // 1F16..1F17; Unknown
        0x1F18, // 1F18..1F1D; Greek
        0x1F1E, // 1F1E..1F1F; Unknown
        0x1F20, // 1F20..1F45; Greek
        0x1F46, // 1F46..1F47; Unknown
        0x1F48, // 1F48..1F4D; Greek
        0x1F4E, // 1F4E..1F4F; Unknown
        0x1F50, // 1F50..1F57; Greek
        0x1F58, // 1F58; Unknown
        0x1F59, // 1F59; Greek
        0x1F5A, // 1F5A; Unknown
        0x1F5B, // 1F5B; Greek
        0x1F5C, // 1F5C; Unknown
        0x1F5D, // 1F5D; Greek
        0x1F5E, // 1F5E; Unknown
        0x1F5F, // 1F5F..1F7D; Greek
        0x1F7E, // 1F7E..1F7F; Unknown
        0x1F80, // 1F80..1FB4; Greek
        0x1FB5, // 1FB5; Unknown
        0x1FB6, // 1FB6..1FC4; Greek
        0x1FC5, // 1FC5; Unknown
        0x1FC6, // 1FC6..1FD3; Greek
        0x1FD4, // 1FD4..1FD5; Unknown
        0x1FD6, // 1FD6..1FDB; Greek
        0x1FDC, // 1FDC; Unknown
        0x1FDD, // 1FDD..1FEF; Greek
        0x1FF0, // 1FF0..1FF1; Unknown
        0x1FF2, // 1FF2..1FF4; Greek
        0x1FF5, // 1FF5; Unknown
        0x1FF6, // 1FF6..1FFE; Greek
        0x1FFF, // 1FFF; Unknown
        0x2000, // 2000..200B; Common
        0x200C, // 200C..200D; Inherited
        0x200E, // 200E..2064; Common
        0x2065, // 2065; Unknown
        0x2066, // 2066..2070; Common
        0x2071, // 2071; Latin
        0x2072, // 2072..2073; Unknown
        0x2074, // 2074..207E; Common
        0x207F, // 207F; Latin
        0x2080, // 2080..208E; Common
        0x208F, // 208F; Unknown
        0x2090, // 2090..209C; Latin
        0x209D, // 209D..209F; Unknown
        0x20A0, // 20A0..20C0; Common
        0x20C1, // 20C1..20CF; Unknown
        0x20D0, // 20D0..20F0; Inherited
        0x20F1, // 20F1..20FF; Unknown
        0x2100, // 2100..2125; Common
        0x2126, // 2126; Greek
        0x2127, // 2127..2129; Common
        0x212A, // 212A..212B; Latin
        0x212C, // 212C..2131; Common
        0x2132, // 2132; Latin
        0x2133, // 2133..214D; Common
        0x214E, // 214E; Latin
        0x214F, // 214F..215F; Common
        0x2160, // 2160..2188; Latin
        0x2189, // 2189..218B; Common
        0x218C, // 218C..218F; Unknown
        0x2190, // 2190..2426; Common
        0x2427, // 2427..243F; Unknown
        0x2440, // 2440..244A; Common
        0x244B, // 244B..245F; Unknown
        0x2460, // 2460..27FF; Common
        0x2800, // 2800..28FF; Braille
        0x2900, // 2900..2B73; Common
        0x2B74, // 2B74..2B75; Unknown
        0x2B76, // 2B76..2B95; Common
        0x2B96, // 2B96; Unknown
        0x2B97, // 2B97..2BFF; Common
        0x2C00, // 2C00..2C5F; Glagolitic
        0x2C60, // 2C60..2C7F; Latin
        0x2C80, // 2C80..2CF3; Coptic
        0x2CF4, // 2CF4..2CF8; Unknown
        0x2CF9, // 2CF9..2CFF; Coptic
        0x2D00, // 2D00..2D25; Georgian
        0x2D26, // 2D26; Unknown
        0x2D27, // 2D27; Georgian
        0x2D28, // 2D28..2D2C; Unknown
        0x2D2D, // 2D2D; Georgian
        0x2D2E, // 2D2E..2D2F; Unknown
        0x2D30, // 2D30..2D67; Tifinagh
        0x2D68, // 2D68..2D6E; Unknown
        0x2D6F, // 2D6F..2D70; Tifinagh
        0x2D71, // 2D71..2D7E; Unknown
        0x2D7F, // 2D7F; Tifinagh
        0x2D80, // 2D80..2D96; Ethiopic
        0x2D97, // 2D97..2D9F; Unknown
        0x2DA0, // 2DA0..2DA6; Ethiopic
        0x2DA7, // 2DA7; Unknown
        0x2DA8, // 2DA8..2DAE; Ethiopic
        0x2DAF, // 2DAF; Unknown
        0x2DB0, // 2DB0..2DB6; Ethiopic
        0x2DB7, // 2DB7; Unknown
        0x2DB8, // 2DB8..2DBE; Ethiopic
        0x2DBF, // 2DBF; Unknown
        0x2DC0, // 2DC0..2DC6; Ethiopic
        0x2DC7, // 2DC7; Unknown
        0x2DC8, // 2DC8..2DCE; Ethiopic
        0x2DCF, // 2DCF; Unknown
        0x2DD0, // 2DD0..2DD6; Ethiopic
        0x2DD7, // 2DD7; Unknown
        0x2DD8, // 2DD8..2DDE; Ethiopic
        0x2DDF, // 2DDF; Unknown
        0x2DE0, // 2DE0..2DFF; Cyrillic
        0x2E00, // 2E00..2E5D; Common
        0x2E5E, // 2E5E..2E7F; Unknown
        0x2E80, // 2E80..2E99; Han
        0x2E9A, // 2E9A; Unknown
        0x2E9B, // 2E9B..2EF3; Han
        0x2EF4, // 2EF4..2EFF; Unknown
        0x2F00, // 2F00..2FD5; Han
        0x2FD6, // 2FD6..2FEF; Unknown
        0x2FF0, // 2FF0..3004; Common
        0x3005, // 3005; Han
        0x3006, // 3006; Common
        0x3007, // 3007; Han
        0x3008, // 3008..3020; Common
        0x3021, // 3021..3029; Han
        0x302A, // 302A..302D; Inherited
        0x302E, // 302E..302F; Hangul
        0x3030, // 3030..3037; Common
        0x3038, // 3038..303B; Han
        0x303C, // 303C..303F; Common
        0x3040, // 3040; Unknown
        0x3041, // 3041..3096; Hiragana
        0x3097, // 3097..3098; Unknown
        0x3099, // 3099..309A; Inherited
        0x309B, // 309B..309C; Common
        0x309D, // 309D..309F; Hiragana
        0x30A0, // 30A0; Common
        0x30A1, // 30A1..30FA; Katakana
        0x30FB, // 30FB..30FC; Common
        0x30FD, // 30FD..30FF; Katakana
        0x3100, // 3100..3104; Unknown
        0x3105, // 3105..312F; Bopomofo
        0x3130, // 3130; Unknown
        0x3131, // 3131..318E; Hangul
        0x318F, // 318F; Unknown
        0x3190, // 3190..319F; Common
        0x31A0, // 31A0..31BF; Bopomofo
        0x31C0, // 31C0..31E3; Common
        0x31E4, // 31E4..31EE; Unknown
        0x31EF, // 31EF; Common
        0x31F0, // 31F0..31FF; Katakana
        0x3200, // 3200..321E; Hangul
        0x321F, // 321F; Unknown
        0x3220, // 3220..325F; Common
        0x3260, // 3260..327E; Hangul
        0x327F, // 327F..32CF; Common
        0x32D0, // 32D0..32FE; Katakana
        0x32FF, // 32FF; Common
        0x3300, // 3300..3357; Katakana
        0x3358, // 3358..33FF; Common
        0x3400, // 3400..4DBF; Han
        0x4DC0, // 4DC0..4DFF; Common
        0x4E00, // 4E00..9FFF; Han
        0xA000, // A000..A48C; Yi
        0xA48D, // A48D..A48F; Unknown
        0xA490, // A490..A4C6; Yi
        0xA4C7, // A4C7..A4CF; Unknown
        0xA4D0, // A4D0..A4FF; Lisu
        0xA500, // A500..A62B; Vai
        0xA62C, // A62C..A63F; Unknown
        0xA640, // A640..A69F; Cyrillic
        0xA6A0, // A6A0..A6F7; Bamum
        0xA6F8, // A6F8..A6FF; Unknown
        0xA700, // A700..A721; Common
        0xA722, // A722..A787; Latin
        0xA788, // A788..A78A; Common
        0xA78B, // A78B..A7CA; Latin
        0xA7CB, // A7CB..A7CF; Unknown
        0xA7D0, // A7D0..A7D1; Latin
        0xA7D2, // A7D2; Unknown
        0xA7D3, // A7D3; Latin
        0xA7D4, // A7D4; Unknown
        0xA7D5, // A7D5..A7D9; Latin
        0xA7DA, // A7DA..A7F1; Unknown
        0xA7F2, // A7F2..A7FF; Latin
        0xA800, // A800..A82C; Syloti_Nagri
        0xA82D, // A82D..A82F; Unknown
        0xA830, // A830..A839; Common
        0xA83A, // A83A..A83F; Unknown
        0xA840, // A840..A877; Phags_Pa
        0xA878, // A878..A87F; Unknown
        0xA880, // A880..A8C5; Saurashtra
        0xA8C6, // A8C6..A8CD; Unknown
        0xA8CE, // A8CE..A8D9; Saurashtra
        0xA8DA, // A8DA..A8DF; Unknown
        0xA8E0, // A8E0..A8FF; Devanagari
        0xA900, // A900..A92D; Kayah_Li
        0xA92E, // A92E; Common
        0xA92F, // A92F; Kayah_Li
        0xA930, // A930..A953; Rejang
        0xA954, // A954..A95E; Unknown
        0xA95F, // A95F; Rejang
        0xA960, // A960..A97C; Hangul
        0xA97D, // A97D..A97F; Unknown
        0xA980, // A980..A9CD; Javanese
        0xA9CE, // A9CE; Unknown
        0xA9CF, // A9CF; Common
        0xA9D0, // A9D0..A9D9; Javanese
        0xA9DA, // A9DA..A9DD; Unknown
        0xA9DE, // A9DE..A9DF; Javanese
        0xA9E0, // A9E0..A9FE; Myanmar
        0xA9FF, // A9FF; Unknown
        0xAA00, // AA00..AA36; Cham
        0xAA37, // AA37..AA3F; Unknown
        0xAA40, // AA40..AA4D; Cham
        0xAA4E, // AA4E..AA4F; Unknown
        0xAA50, // AA50..AA59; Cham
        0xAA5A, // AA5A..AA5B; Unknown
        0xAA5C, // AA5C..AA5F; Cham
        0xAA60, // AA60..AA7F; Myanmar
        0xAA80, // AA80..AAC2; Tai_Viet
        0xAAC3, // AAC3..AADA; Unknown
        0xAADB, // AADB..AADF; Tai_Viet
        0xAAE0, // AAE0..AAF6; Meetei_Mayek
        0xAAF7, // AAF7..AB00; Unknown
        0xAB01, // AB01..AB06; Ethiopic
        0xAB07, // AB07..AB08; Unknown
        0xAB09, // AB09..AB0E; Ethiopic
        0xAB0F, // AB0F..AB10; Unknown
        0xAB11, // AB11..AB16; Ethiopic
        0xAB17, // AB17..AB1F; Unknown
        0xAB20, // AB20..AB26; Ethiopic
        0xAB27, // AB27; Unknown
        0xAB28, // AB28..AB2E; Ethiopic
        0xAB2F, // AB2F; Unknown
        0xAB30, // AB30..AB5A; Latin
        0xAB5B, // AB5B; Common
        0xAB5C, // AB5C..AB64; Latin
        0xAB65, // AB65; Greek
        0xAB66, // AB66..AB69; Latin
        0xAB6A, // AB6A..AB6B; Common
        0xAB6C, // AB6C..AB6F; Unknown
        0xAB70, // AB70..ABBF; Cherokee
        0xABC0, // ABC0..ABED; Meetei_Mayek
        0xABEE, // ABEE..ABEF; Unknown
        0xABF0, // ABF0..ABF9; Meetei_Mayek
        0xABFA, // ABFA..ABFF; Unknown
        0xAC00, // AC00..D7A3; Hangul
        0xD7A4, // D7A4..D7AF; Unknown
        0xD7B0, // D7B0..D7C6; Hangul
        0xD7C7, // D7C7..D7CA; Unknown
        0xD7CB, // D7CB..D7FB; Hangul
        0xD7FC, // D7FC..F8FF; Unknown
        0xF900, // F900..FA6D; Han
        0xFA6E, // FA6E..FA6F; Unknown
        0xFA70, // FA70..FAD9; Han
        0xFADA, // FADA..FAFF; Unknown
        0xFB00, // FB00..FB06; Latin
        0xFB07, // FB07..FB12; Unknown
        0xFB13, // FB13..FB17; Armenian
        0xFB18, // FB18..FB1C; Unknown
        0xFB1D, // FB1D..FB36; Hebrew
        0xFB37, // FB37; Unknown
        0xFB38, // FB38..FB3C; Hebrew
        0xFB3D, // FB3D; Unknown
        0xFB3E, // FB3E; Hebrew
        0xFB3F, // FB3F; Unknown
        0xFB40, // FB40..FB41; Hebrew
        0xFB42, // FB42; Unknown
        0xFB43, // FB43..FB44; Hebrew
        0xFB45, // FB45; Unknown
        0xFB46, // FB46..FB4F; Hebrew
        0xFB50, // FB50..FBC2; Arabic
        0xFBC3, // FBC3..FBD2; Unknown
        0xFBD3, // FBD3..FD3D; Arabic
        0xFD3E, // FD3E..FD3F; Common
        0xFD40, // FD40..FD8F; Arabic
        0xFD90, // FD90..FD91; Unknown
        0xFD92, // FD92..FDC7; Arabic
        0xFDC8, // FDC8..FDCE; Unknown
        0xFDCF, // FDCF; Arabic
        0xFDD0, // FDD0..FDEF; Unknown
        0xFDF0, // FDF0..FDFF; Arabic
        0xFE00, // FE00..FE0F; Inherited
        0xFE10, // FE10..FE19; Common
        0xFE1A, // FE1A..FE1F; Unknown
        0xFE20, // FE20..FE2D; Inherited
        0xFE2E, // FE2E..FE2F; Cyrillic
        0xFE30, // FE30..FE52; Common
        0xFE53, // FE53; Unknown
        0xFE54, // FE54..FE66; Common
        0xFE67, // FE67; Unknown
        0xFE68, // FE68..FE6B; Common
        0xFE6C, // FE6C..FE6F; Unknown
        0xFE70, // FE70..FE74; Arabic
        0xFE75, // FE75; Unknown
        0xFE76, // FE76..FEFC; Arabic
        0xFEFD, // FEFD..FEFE; Unknown
        0xFEFF, // FEFF; Common
        0xFF00, // FF00; Unknown
        0xFF01, // FF01..FF20; Common
        0xFF21, // FF21..FF3A; Latin
        0xFF3B, // FF3B..FF40; Common
        0xFF41, // FF41..FF5A; Latin
        0xFF5B, // FF5B..FF65; Common
        0xFF66, // FF66..FF6F; Katakana
        0xFF70, // FF70; Common
        0xFF71, // FF71..FF9D; Katakana
        0xFF9E, // FF9E..FF9F; Common
        0xFFA0, // FFA0..FFBE; Hangul
        0xFFBF, // FFBF..FFC1; Unknown
        0xFFC2, // FFC2..FFC7; Hangul
        0xFFC8, // FFC8..FFC9; Unknown
        0xFFCA, // FFCA..FFCF; Hangul
        0xFFD0, // FFD0..FFD1; Unknown
        0xFFD2, // FFD2..FFD7; Hangul
        0xFFD8, // FFD8..FFD9; Unknown
        0xFFDA, // FFDA..FFDC; Hangul
        0xFFDD, // FFDD..FFDF; Unknown
        0xFFE0, // FFE0..FFE6; Common
        0xFFE7, // FFE7; Unknown
        0xFFE8, // FFE8..FFEE; Common
        0xFFEF, // FFEF..FFF8; Unknown
        0xFFF9, // FFF9..FFFD; Common
        0xFFFE, // FFFE..FFFF; Unknown
        0x10000, // 10000..1000B; Linear_B
        0x1000C, // 1000C; Unknown
        0x1000D, // 1000D..10026; Linear_B
        0x10027, // 10027; Unknown
        0x10028, // 10028..1003A; Linear_B
        0x1003B, // 1003B; Unknown
        0x1003C, // 1003C..1003D; Linear_B
        0x1003E, // 1003E; Unknown
        0x1003F, // 1003F..1004D; Linear_B
        0x1004E, // 1004E..1004F; Unknown
        0x10050, // 10050..1005D; Linear_B
        0x1005E, // 1005E..1007F; Unknown
        0x10080, // 10080..100FA; Linear_B
        0x100FB, // 100FB..100FF; Unknown
        0x10100, // 10100..10102; Common
        0x10103, // 10103..10106; Unknown
        0x10107, // 10107..10133; Common
        0x10134, // 10134..10136; Unknown
        0x10137, // 10137..1013F; Common
        0x10140, // 10140..1018E; Greek
        0x1018F, // 1018F; Unknown
        0x10190, // 10190..1019C; Common
        0x1019D, // 1019D..1019F; Unknown
        0x101A0, // 101A0; Greek
        0x101A1, // 101A1..101CF; Unknown
        0x101D0, // 101D0..101FC; Common
        0x101FD, // 101FD; Inherited
        0x101FE, // 101FE..1027F; Unknown
        0x10280, // 10280..1029C; Lycian
        0x1029D, // 1029D..1029F; Unknown
        0x102A0, // 102A0..102D0; Carian
        0x102D1, // 102D1..102DF; Unknown
        0x102E0, // 102E0; Inherited
        0x102E1, // 102E1..102FB; Common
        0x102FC, // 102FC..102FF; Unknown
        0x10300, // 10300..10323; Old_Italic
        0x10324, // 10324..1032C; Unknown
        0x1032D, // 1032D..1032F; Old_Italic
        0x10330, // 10330..1034A; Gothic
        0x1034B, // 1034B..1034F; Unknown
        0x10350, // 10350..1037A; Old_Permic
        0x1037B, // 1037B..1037F; Unknown
        0x10380, // 10380..1039D; Ugaritic
        0x1039E, // 1039E; Unknown
        0x1039F, // 1039F; Ugaritic
        0x103A0, // 103A0..103C3; Old_Persian
        0x103C4, // 103C4..103C7; Unknown
        0x103C8, // 103C8..103D5; Old_Persian
        0x103D6, // 103D6..103FF; Unknown
        0x10400, // 10400..1044F; Deseret
        0x10450, // 10450..1047F; Shavian
        0x10480, // 10480..1049D; Osmanya
        0x1049E, // 1049E..1049F; Unknown
        0x104A0, // 104A0..104A9; Osmanya
        0x104AA, // 104AA..104AF; Unknown
        0x104B0, // 104B0..104D3; Osage
        0x104D4, // 104D4..104D7; Unknown
        0x104D8, // 104D8..104FB; Osage
        0x104FC, // 104FC..104FF; Unknown
        0x10500, // 10500..10527; Elbasan
        0x10528, // 10528..1052F; Unknown
        0x10530, // 10530..10563; Caucasian_Albanian
        0x10564, // 10564..1056E; Unknown
        0x1056F, // 1056F; Caucasian_Albanian
        0x10570, // 10570..1057A; Vithkuqi
        0x1057B, // 1057B; Unknown
        0x1057C, // 1057C..1058A; Vithkuqi
        0x1058B, // 1058B; Unknown
        0x1058C, // 1058C..10592; Vithkuqi
        0x10593, // 10593; Unknown
        0x10594, // 10594..10595; Vithkuqi
        0x10596, // 10596; Unknown
        0x10597, // 10597..105A1; Vithkuqi
        0x105A2, // 105A2; Unknown
        0x105A3, // 105A3..105B1; Vithkuqi
        0x105B2, // 105B2; Unknown
        0x105B3, // 105B3..105B9; Vithkuqi
        0x105BA, // 105BA; Unknown
        0x105BB, // 105BB..105BC; Vithkuqi
        0x105BD, // 105BD..105FF; Unknown
        0x10600, // 10600..10736; Linear_A
        0x10737, // 10737..1073F; Unknown
        0x10740, // 10740..10755; Linear_A
        0x10756, // 10756..1075F; Unknown
        0x10760, // 10760..10767; Linear_A
        0x10768, // 10768..1077F; Unknown
        0x10780, // 10780..10785; Latin
        0x10786, // 10786; Unknown
        0x10787, // 10787..107B0; Latin
        0x107B1, // 107B1; Unknown
        0x107B2, // 107B2..107BA; Latin
        0x107BB, // 107BB..107FF; Unknown
        0x10800, // 10800..10805; Cypriot
        0x10806, // 10806..10807; Unknown
        0x10808, // 10808; Cypriot
        0x10809, // 10809; Unknown
        0x1080A, // 1080A..10835; Cypriot
        0x10836, // 10836; Unknown
        0x10837, // 10837..10838; Cypriot
        0x10839, // 10839..1083B; Unknown
        0x1083C, // 1083C; Cypriot
        0x1083D, // 1083D..1083E; Unknown
        0x1083F, // 1083F; Cypriot
        0x10840, // 10840..10855; Imperial_Aramaic
        0x10856, // 10856; Unknown
        0x10857, // 10857..1085F; Imperial_Aramaic
        0x10860, // 10860..1087F; Palmyrene
        0x10880, // 10880..1089E; Nabataean
        0x1089F, // 1089F..108A6; Unknown
        0x108A7, // 108A7..108AF; Nabataean
        0x108B0, // 108B0..108DF; Unknown
        0x108E0, // 108E0..108F2; Hatran
        0x108F3, // 108F3; Unknown
        0x108F4, // 108F4..108F5; Hatran
        0x108F6, // 108F6..108FA; Unknown
        0x108FB, // 108FB..108FF; Hatran
        0x10900, // 10900..1091B; Phoenician
        0x1091C, // 1091C..1091E; Unknown
        0x1091F, // 1091F; Phoenician
        0x10920, // 10920..10939; Lydian
        0x1093A, // 1093A..1093E; Unknown
        0x1093F, // 1093F; Lydian
        0x10940, // 10940..1097F; Unknown
        0x10980, // 10980..1099F; Meroitic_Hieroglyphs
        0x109A0, // 109A0..109B7; Meroitic_Cursive
        0x109B8, // 109B8..109BB; Unknown
        0x109BC, // 109BC..109CF; Meroitic_Cursive
        0x109D0, // 109D0..109D1; Unknown
        0x109D2, // 109D2..109FF; Meroitic_Cursive
        0x10A00, // 10A00..10A03; Kharoshthi
        0x10A04, // 10A04; Unknown
        0x10A05, // 10A05..10A06; Kharoshthi
        0x10A07, // 10A07..10A0B; Unknown
        0x10A0C, // 10A0C..10A13; Kharoshthi
        0x10A14, // 10A14; Unknown
        0x10A15, // 10A15..10A17; Kharoshthi
        0x10A18, // 10A18; Unknown
        0x10A19, // 10A19..10A35; Kharoshthi
        0x10A36, // 10A36..10A37; Unknown
        0x10A38, // 10A38..10A3A; Kharoshthi
        0x10A3B, // 10A3B..10A3E; Unknown
        0x10A3F, // 10A3F..10A48; Kharoshthi
        0x10A49, // 10A49..10A4F; Unknown
        0x10A50, // 10A50..10A58; Kharoshthi
        0x10A59, // 10A59..10A5F; Unknown
        0x10A60, // 10A60..10A7F; Old_South_Arabian
        0x10A80, // 10A80..10A9F; Old_North_Arabian
        0x10AA0, // 10AA0..10ABF; Unknown
        0x10AC0, // 10AC0..10AE6; Manichaean
        0x10AE7, // 10AE7..10AEA; Unknown
        0x10AEB, // 10AEB..10AF6; Manichaean
        0x10AF7, // 10AF7..10AFF; Unknown
        0x10B00, // 10B00..10B35; Avestan
        0x10B36, // 10B36..10B38; Unknown
        0x10B39, // 10B39..10B3F; Avestan
        0x10B40, // 10B40..10B55; Inscriptional_Parthian
        0x10B56, // 10B56..10B57; Unknown
        0x10B58, // 10B58..10B5F; Inscriptional_Parthian
        0x10B60, // 10B60..10B72; Inscriptional_Pahlavi
        0x10B73, // 10B73..10B77; Unknown
        0x10B78, // 10B78..10B7F; Inscriptional_Pahlavi
        0x10B80, // 10B80..10B91; Psalter_Pahlavi
        0x10B92, // 10B92..10B98; Unknown
        0x10B99, // 10B99..10B9C; Psalter_Pahlavi
        0x10B9D, // 10B9D..10BA8; Unknown
        0x10BA9, // 10BA9..10BAF; Psalter_Pahlavi
        0x10BB0, // 10BB0..10BFF; Unknown
        0x10C00, // 10C00..10C48; Old_Turkic
        0x10C49, // 10C49..10C7F; Unknown
        0x10C80, // 10C80..10CB2; Old_Hungarian
        0x10CB3, // 10CB3..10CBF; Unknown
        0x10CC0, // 10CC0..10CF2; Old_Hungarian
        0x10CF3, // 10CF3..10CF9; Unknown
        0x10CFA, // 10CFA..10CFF; Old_Hungarian
        0x10D00, // 10D00..10D27; Hanifi_Rohingya
        0x10D28, // 10D28..10D2F; Unknown
        0x10D30, // 10D30..10D39; Hanifi_Rohingya
        0x10D3A, // 10D3A..10E5F; Unknown
        0x10E60, // 10E60..10E7E; Arabic
        0x10E7F, // 10E7F; Unknown
        0x10E80, // 10E80..10EA9; Yezidi
        0x10EAA, // 10EAA; Unknown
        0x10EAB, // 10EAB..10EAD; Yezidi
        0x10EAE, // 10EAE..10EAF; Unknown
        0x10EB0, // 10EB0..10EB1; Yezidi
        0x10EB2, // 10EB2..10EFC; Unknown
        0x10EFD, // 10EFD..10EFF; Arabic
        0x10F00, // 10F00..10F27; Old_Sogdian
        0x10F28, // 10F28..10F2F; Unknown
        0x10F30, // 10F30..10F59; Sogdian
        0x10F5A, // 10F5A..10F6F; Unknown
        0x10F70, // 10F70..10F89; Old_Uyghur
        0x10F8A, // 10F8A..10FAF; Unknown
        0x10FB0, // 10FB0..10FCB; Chorasmian
        0x10FCC, // 10FCC..10FDF; Unknown
        0x10FE0, // 10FE0..10FF6; Elymaic
        0x10FF7, // 10FF7..10FFF; Unknown
        0x11000, // 11000..1104D; Brahmi
        0x1104E, // 1104E..11051; Unknown
        0x11052, // 11052..11075; Brahmi
        0x11076, // 11076..1107E; Unknown
        0x1107F, // 1107F; Brahmi
        0x11080, // 11080..110C2; Kaithi
        0x110C3, // 110C3..110CC; Unknown
        0x110CD, // 110CD; Kaithi
        0x110CE, // 110CE..110CF; Unknown
        0x110D0, // 110D0..110E8; Sora_Sompeng
        0x110E9, // 110E9..110EF; Unknown
        0x110F0, // 110F0..110F9; Sora_Sompeng
        0x110FA, // 110FA..110FF; Unknown
        0x11100, // 11100..11134; Chakma
        0x11135, // 11135; Unknown
        0x11136, // 11136..11147; Chakma
        0x11148, // 11148..1114F; Unknown
        0x11150, // 11150..11176; Mahajani
        0x11177, // 11177..1117F; Unknown
        0x11180, // 11180..111DF; Sharada
        0x111E0, // 111E0; Unknown
        0x111E1, // 111E1..111F4; Sinhala
        0x111F5, // 111F5..111FF; Unknown
        0x11200, // 11200..11211; Khojki
        0x11212, // 11212; Unknown
        0x11213, // 11213..11241; Khojki
        0x11242, // 11242..1127F; Unknown
        0x11280, // 11280..11286; Multani
        0x11287, // 11287; Unknown
        0x11288, // 11288; Multani
        0x11289, // 11289; Unknown
        0x1128A, // 1128A..1128D; Multani
        0x1128E, // 1128E; Unknown
        0x1128F, // 1128F..1129D; Multani
        0x1129E, // 1129E; Unknown
        0x1129F, // 1129F..112A9; Multani
        0x112AA, // 112AA..112AF; Unknown
        0x112B0, // 112B0..112EA; Khudawadi
        0x112EB, // 112EB..112EF; Unknown
        0x112F0, // 112F0..112F9; Khudawadi
        0x112FA, // 112FA..112FF; Unknown
        0x11300, // 11300..11303; Grantha
        0x11304, // 11304; Unknown
        0x11305, // 11305..1130C; Grantha
        0x1130D, // 1130D..1130E; Unknown
        0x1130F, // 1130F..11310; Grantha
        0x11311, // 11311..11312; Unknown
        0x11313, // 11313..11328; Grantha
        0x11329, // 11329; Unknown
        0x1132A, // 1132A..11330; Grantha
        0x11331, // 11331; Unknown
        0x11332, // 11332..11333; Grantha
        0x11334, // 11334; Unknown
        0x11335, // 11335..11339; Grantha
        0x1133A, // 1133A; Unknown
        0x1133B, // 1133B; Inherited
        0x1133C, // 1133C..11344; Grantha
        0x11345, // 11345..11346; Unknown
        0x11347, // 11347..11348; Grantha
        0x11349, // 11349..1134A; Unknown
        0x1134B, // 1134B..1134D; Grantha
        0x1134E, // 1134E..1134F; Unknown
        0x11350, // 11350; Grantha
        0x11351, // 11351..11356; Unknown
        0x11357, // 11357; Grantha
        0x11358, // 11358..1135C; Unknown
        0x1135D, // 1135D..11363; Grantha
        0x11364, // 11364..11365; Unknown
        0x11366, // 11366..1136C; Grantha
        0x1136D, // 1136D..1136F; Unknown
        0x11370, // 11370..11374; Grantha
        0x11375, // 11375..113FF; Unknown
        0x11400, // 11400..1145B; Newa
        0x1145C, // 1145C; Unknown
        0x1145D, // 1145D..11461; Newa
        0x11462, // 11462..1147F; Unknown
        0x11480, // 11480..114C7; Tirhuta
        0x114C8, // 114C8..114CF; Unknown
        0x114D0, // 114D0..114D9; Tirhuta
        0x114DA, // 114DA..1157F; Unknown
        0x11580, // 11580..115B5; Siddham
        0x115B6, // 115B6..115B7; Unknown
        0x115B8, // 115B8..115DD; Siddham
        0x115DE, // 115DE..115FF; Unknown
        0x11600, // 11600..11644; Modi
        0x11645, // 11645..1164F; Unknown
        0x11650, // 11650..11659; Modi
        0x1165A, // 1165A..1165F; Unknown
        0x11660, // 11660..1166C; Mongolian
        0x1166D, // 1166D..1167F; Unknown
        0x11680, // 11680..116B9; Takri
        0x116BA, // 116BA..116BF; Unknown
        0x116C0, // 116C0..116C9; Takri
        0x116CA, // 116CA..116FF; Unknown
        0x11700, // 11700..1171A; Ahom
        0x1171B, // 1171B..1171C; Unknown
        0x1171D, // 1171D..1172B; Ahom
        0x1172C, // 1172C..1172F; Unknown
        0x11730, // 11730..11746; Ahom
        0x11747, // 11747..117FF; Unknown
        0x11800, // 11800..1183B; Dogra
        0x1183C, // 1183C..1189F; Unknown
        0x118A0, // 118A0..118F2; Warang_Citi
        0x118F3, // 118F3..118FE; Unknown
        0x118FF, // 118FF; Warang_Citi
        0x11900, // 11900..11906; Dives_Akuru
        0x11907, // 11907..11908; Unknown
        0x11909, // 11909; Dives_Akuru
        0x1190A, // 1190A..1190B; Unknown
        0x1190C, // 1190C..11913; Dives_Akuru
        0x11914, // 11914; Unknown
        0x11915, // 11915..11916; Dives_Akuru
        0x11917, // 11917; Unknown
        0x11918, // 11918..11935; Dives_Akuru
        0x11936, // 11936; Unknown
        0x11937, // 11937..11938; Dives_Akuru
        0x11939, // 11939..1193A; Unknown
        0x1193B, // 1193B..11946; Dives_Akuru
        0x11947, // 11947..1194F; Unknown
        0x11950, // 11950..11959; Dives_Akuru
        0x1195A, // 1195A..1199F; Unknown
        0x119A0, // 119A0..119A7; Nandinagari
        0x119A8, // 119A8..119A9; Unknown
        0x119AA, // 119AA..119D7; Nandinagari
        0x119D8, // 119D8..119D9; Unknown
        0x119DA, // 119DA..119E4; Nandinagari
        0x119E5, // 119E5..119FF; Unknown
        0x11A00, // 11A00..11A47; Zanabazar_Square
        0x11A48, // 11A48..11A4F; Unknown
        0x11A50, // 11A50..11AA2; Soyombo
        0x11AA3, // 11AA3..11AAF; Unknown
        0x11AB0, // 11AB0..11ABF; Canadian_Aboriginal
        0x11AC0, // 11AC0..11AF8; Pau_Cin_Hau
        0x11AF9, // 11AF9..11AFF; Unknown
        0x11B00, // 11B00..11B09; Devanagari
        0x11B0A, // 11B0A..11BFF; Unknown
        0x11C00, // 11C00..11C08; Bhaiksuki
        0x11C09, // 11C09; Unknown
        0x11C0A, // 11C0A..11C36; Bhaiksuki
        0x11C37, // 11C37; Unknown
        0x11C38, // 11C38..11C45; Bhaiksuki
        0x11C46, // 11C46..11C4F; Unknown
        0x11C50, // 11C50..11C6C; Bhaiksuki
        0x11C6D, // 11C6D..11C6F; Unknown
        0x11C70, // 11C70..11C8F; Marchen
        0x11C90, // 11C90..11C91; Unknown
        0x11C92, // 11C92..11CA7; Marchen
        0x11CA8, // 11CA8; Unknown
        0x11CA9, // 11CA9..11CB6; Marchen
        0x11CB7, // 11CB7..11CFF; Unknown
        0x11D00, // 11D00..11D06; Masaram_Gondi
        0x11D07, // 11D07; Unknown
        0x11D08, // 11D08..11D09; Masaram_Gondi
        0x11D0A, // 11D0A; Unknown
        0x11D0B, // 11D0B..11D36; Masaram_Gondi
        0x11D37, // 11D37..11D39; Unknown
        0x11D3A, // 11D3A; Masaram_Gondi
        0x11D3B, // 11D3B; Unknown
        0x11D3C, // 11D3C..11D3D; Masaram_Gondi
        0x11D3E, // 11D3E; Unknown
        0x11D3F, // 11D3F..11D47; Masaram_Gondi
        0x11D48, // 11D48..11D4F; Unknown
        0x11D50, // 11D50..11D59; Masaram_Gondi
        0x11D5A, // 11D5A..11D5F; Unknown
        0x11D60, // 11D60..11D65; Gunjala_Gondi
        0x11D66, // 11D66; Unknown
        0x11D67, // 11D67..11D68; Gunjala_Gondi
        0x11D69, // 11D69; Unknown
        0x11D6A, // 11D6A..11D8E; Gunjala_Gondi
        0x11D8F, // 11D8F; Unknown
        0x11D90, // 11D90..11D91; Gunjala_Gondi
        0x11D92, // 11D92; Unknown
        0x11D93, // 11D93..11D98; Gunjala_Gondi
        0x11D99, // 11D99..11D9F; Unknown
        0x11DA0, // 11DA0..11DA9; Gunjala_Gondi
        0x11DAA, // 11DAA..11EDF; Unknown
        0x11EE0, // 11EE0..11EF8; Makasar
        0x11EF9, // 11EF9..11EFF; Unknown
        0x11F00, // 11F00..11F10; Kawi
        0x11F11, // 11F11; Unknown
        0x11F12, // 11F12..11F3A; Kawi
        0x11F3B, // 11F3B..11F3D; Unknown
        0x11F3E, // 11F3E..11F59; Kawi
        0x11F5A, // 11F5A..11FAF; Unknown
        0x11FB0, // 11FB0; Lisu
        0x11FB1, // 11FB1..11FBF; Unknown
        0x11FC0, // 11FC0..11FF1; Tamil
        0x11FF2, // 11FF2..11FFE; Unknown
        0x11FFF, // 11FFF; Tamil
        0x12000, // 12000..12399; Cuneiform
        0x1239A, // 1239A..123FF; Unknown
        0x12400, // 12400..1246E; Cuneiform
        0x1246F, // 1246F; Unknown
        0x12470, // 12470..12474; Cuneiform
        0x12475, // 12475..1247F; Unknown
        0x12480, // 12480..12543; Cuneiform
        0x12544, // 12544..12F8F; Unknown
        0x12F90, // 12F90..12FF2; Cypro_Minoan
        0x12FF3, // 12FF3..12FFF; Unknown
        0x13000, // 13000..13455; Egyptian_Hieroglyphs
        0x13456, // 13456..143FF; Unknown
        0x14400, // 14400..14646; Anatolian_Hieroglyphs
        0x14647, // 14647..167FF; Unknown
        0x16800, // 16800..16A38; Bamum
        0x16A39, // 16A39..16A3F; Unknown
        0x16A40, // 16A40..16A5E; Mro
        0x16A5F, // 16A5F; Unknown
        0x16A60, // 16A60..16A69; Mro
        0x16A6A, // 16A6A..16A6D; Unknown
        0x16A6E, // 16A6E..16A6F; Mro
        0x16A70, // 16A70..16ABE; Tangsa
        0x16ABF, // 16ABF; Unknown
        0x16AC0, // 16AC0..16AC9; Tangsa
        0x16ACA, // 16ACA..16ACF; Unknown
        0x16AD0, // 16AD0..16AED; Bassa_Vah
        0x16AEE, // 16AEE..16AEF; Unknown
        0x16AF0, // 16AF0..16AF5; Bassa_Vah
        0x16AF6, // 16AF6..16AFF; Unknown
        0x16B00, // 16B00..16B45; Pahawh_Hmong
        0x16B46, // 16B46..16B4F; Unknown
        0x16B50, // 16B50..16B59; Pahawh_Hmong
        0x16B5A, // 16B5A; Unknown
        0x16B5B, // 16B5B..16B61; Pahawh_Hmong
        0x16B62, // 16B62; Unknown
        0x16B63, // 16B63..16B77; Pahawh_Hmong
        0x16B78, // 16B78..16B7C; Unknown
        0x16B7D, // 16B7D..16B8F; Pahawh_Hmong
        0x16B90, // 16B90..16E3F; Unknown
        0x16E40, // 16E40..16E9A; Medefaidrin
        0x16E9B, // 16E9B..16EFF; Unknown
        0x16F00, // 16F00..16F4A; Miao
        0x16F4B, // 16F4B..16F4E; Unknown
        0x16F4F, // 16F4F..16F87; Miao
        0x16F88, // 16F88..16F8E; Unknown
        0x16F8F, // 16F8F..16F9F; Miao
        0x16FA0, // 16FA0..16FDF; Unknown
        0x16FE0, // 16FE0; Tangut
        0x16FE1, // 16FE1; Nushu
        0x16FE2, // 16FE2..16FE3; Han
        0x16FE4, // 16FE4; Khitan_Small_Script
        0x16FE5, // 16FE5..16FEF; Unknown
        0x16FF0, // 16FF0..16FF1; Han
        0x16FF2, // 16FF2..16FFF; Unknown
        0x17000, // 17000..187F7; Tangut
        0x187F8, // 187F8..187FF; Unknown
        0x18800, // 18800..18AFF; Tangut
        0x18B00, // 18B00..18CD5; Khitan_Small_Script
        0x18CD6, // 18CD6..18CFF; Unknown
        0x18D00, // 18D00..18D08; Tangut
        0x18D09, // 18D09..1AFEF; Unknown
        0x1AFF0, // 1AFF0..1AFF3; Katakana
        0x1AFF4, // 1AFF4; Unknown
        0x1AFF5, // 1AFF5..1AFFB; Katakana
        0x1AFFC, // 1AFFC; Unknown
        0x1AFFD, // 1AFFD..1AFFE; Katakana
        0x1AFFF, // 1AFFF; Unknown
        0x1B000, // 1B000; Katakana
        0x1B001, // 1B001..1B11F; Hiragana
        0x1B120, // 1B120..1B122; Katakana
        0x1B123, // 1B123..1B131; Unknown
        0x1B132, // 1B132; Hiragana
        0x1B133, // 1B133..1B14F; Unknown
        0x1B150, // 1B150..1B152; Hiragana
        0x1B153, // 1B153..1B154; Unknown
        0x1B155, // 1B155; Katakana
        0x1B156, // 1B156..1B163; Unknown
        0x1B164, // 1B164..1B167; Katakana
        0x1B168, // 1B168..1B16F; Unknown
        0x1B170, // 1B170..1B2FB; Nushu
        0x1B2FC, // 1B2FC..1BBFF; Unknown
        0x1BC00, // 1BC00..1BC6A; Duployan
        0x1BC6B, // 1BC6B..1BC6F; Unknown
        0x1BC70, // 1BC70..1BC7C; Duployan
        0x1BC7D, // 1BC7D..1BC7F; Unknown
        0x1BC80, // 1BC80..1BC88; Duployan
        0x1BC89, // 1BC89..1BC8F; Unknown
        0x1BC90, // 1BC90..1BC99; Duployan
        0x1BC9A, // 1BC9A..1BC9B; Unknown
        0x1BC9C, // 1BC9C..1BC9F; Duployan
        0x1BCA0, // 1BCA0..1BCA3; Common
        0x1BCA4, // 1BCA4..1CEFF; Unknown
        0x1CF00, // 1CF00..1CF2D; Inherited
        0x1CF2E, // 1CF2E..1CF2F; Unknown
        0x1CF30, // 1CF30..1CF46; Inherited
        0x1CF47, // 1CF47..1CF4F; Unknown
        0x1CF50, // 1CF50..1CFC3; Common
        0x1CFC4, // 1CFC4..1CFFF; Unknown
        0x1D000, // 1D000..1D0F5; Common
        0x1D0F6, // 1D0F6..1D0FF; Unknown
        0x1D100, // 1D100..1D126; Common
        0x1D127, // 1D127..1D128; Unknown
        0x1D129, // 1D129..1D166; Common
        0x1D167, // 1D167..1D169; Inherited
        0x1D16A, // 1D16A..1D17A; Common
        0x1D17B, // 1D17B..1D182; Inherited
        0x1D183, // 1D183..1D184; Common
        0x1D185, // 1D185..1D18B; Inherited
        0x1D18C, // 1D18C..1D1A9; Common
        0x1D1AA, // 1D1AA..1D1AD; Inherited
        0x1D1AE, // 1D1AE..1D1EA; Common
        0x1D1EB, // 1D1EB..1D1FF; Unknown
        0x1D200, // 1D200..1D245; Greek
        0x1D246, // 1D246..1D2BF; Unknown
        0x1D2C0, // 1D2C0..1D2D3; Common
        0x1D2D4, // 1D2D4..1D2DF; Unknown
        0x1D2E0, // 1D2E0..1D2F3; Common
        0x1D2F4, // 1D2F4..1D2FF; Unknown
        0x1D300, // 1D300..1D356; Common
        0x1D357, // 1D357..1D35F; Unknown
        0x1D360, // 1D360..1D378; Common
        0x1D379, // 1D379..1D3FF; Unknown
        0x1D400, // 1D400..1D454; Common
        0x1D455, // 1D455; Unknown
        0x1D456, // 1D456..1D49C; Common
        0x1D49D, // 1D49D; Unknown
        0x1D49E, // 1D49E..1D49F; Common
        0x1D4A0, // 1D4A0..1D4A1; Unknown
        0x1D4A2, // 1D4A2; Common
        0x1D4A3, // 1D4A3..1D4A4; Unknown
        0x1D4A5, // 1D4A5..1D4A6; Common
        0x1D4A7, // 1D4A7..1D4A8; Unknown
        0x1D4A9, // 1D4A9..1D4AC; Common
        0x1D4AD, // 1D4AD; Unknown
        0x1D4AE, // 1D4AE..1D4B9; Common
        0x1D4BA, // 1D4BA; Unknown
        0x1D4BB, // 1D4BB; Common
        0x1D4BC, // 1D4BC; Unknown
        0x1D4BD, // 1D4BD..1D4C3; Common
        0x1D4C4, // 1D4C4; Unknown
        0x1D4C5, // 1D4C5..1D505; Common
        0x1D506, // 1D506; Unknown
        0x1D507, // 1D507..1D50A; Common
        0x1D50B, // 1D50B..1D50C; Unknown
        0x1D50D, // 1D50D..1D514; Common
        0x1D515, // 1D515; Unknown
        0x1D516, // 1D516..1D51C; Common
        0x1D51D, // 1D51D; Unknown
        0x1D51E, // 1D51E..1D539; Common
        0x1D53A, // 1D53A; Unknown
        0x1D53B, // 1D53B..1D53E; Common
        0x1D53F, // 1D53F; Unknown
        0x1D540, // 1D540..1D544; Common
        0x1D545, // 1D545; Unknown
        0x1D546, // 1D546; Common
        0x1D547, // 1D547..1D549; Unknown
        0x1D54A, // 1D54A..1D550; Common
        0x1D551, // 1D551; Unknown
        0x1D552, // 1D552..1D6A5; Common
        0x1D6A6, // 1D6A6..1D6A7; Unknown
        0x1D6A8, // 1D6A8..1D7CB; Common
        0x1D7CC, // 1D7CC..1D7CD; Unknown
        0x1D7CE, // 1D7CE..1D7FF; Common
        0x1D800, // 1D800..1DA8B; SignWriting
        0x1DA8C, // 1DA8C..1DA9A; Unknown
        0x1DA9B, // 1DA9B..1DA9F; SignWriting
        0x1DAA0, // 1DAA0; Unknown
        0x1DAA1, // 1DAA1..1DAAF; SignWriting
        0x1DAB0, // 1DAB0..1DEFF; Unknown
        0x1DF00, // 1DF00..1DF1E; Latin
        0x1DF1F, // 1DF1F..1DF24; Unknown
        0x1DF25, // 1DF25..1DF2A; Latin
        0x1DF2B, // 1DF2B..1DFFF; Unknown
        0x1E000, // 1E000..1E006; Glagolitic
        0x1E007, // 1E007; Unknown
        0x1E008, // 1E008..1E018; Glagolitic
        0x1E019, // 1E019..1E01A; Unknown
        0x1E01B, // 1E01B..1E021; Glagolitic
        0x1E022, // 1E022; Unknown
        0x1E023, // 1E023..1E024; Glagolitic
        0x1E025, // 1E025; Unknown
        0x1E026, // 1E026..1E02A; Glagolitic
        0x1E02B, // 1E02B..1E02F; Unknown
        0x1E030, // 1E030..1E06D; Cyrillic
        0x1E06E, // 1E06E..1E08E; Unknown
        0x1E08F, // 1E08F; Cyrillic
        0x1E090, // 1E090..1E0FF; Unknown
        0x1E100, // 1E100..1E12C; Nyiakeng_Puachue_Hmong
        0x1E12D, // 1E12D..1E12F; Unknown
        0x1E130, // 1E130..1E13D; Nyiakeng_Puachue_Hmong
        0x1E13E, // 1E13E..1E13F; Unknown
        0x1E140, // 1E140..1E149; Nyiakeng_Puachue_Hmong
        0x1E14A, // 1E14A..1E14D; Unknown
        0x1E14E, // 1E14E..1E14F; Nyiakeng_Puachue_Hmong
        0x1E150, // 1E150..1E28F; Unknown
        0x1E290, // 1E290..1E2AE; Toto
        0x1E2AF, // 1E2AF..1E2BF; Unknown
        0x1E2C0, // 1E2C0..1E2F9; Wancho
        0x1E2FA, // 1E2FA..1E2FE; Unknown
        0x1E2FF, // 1E2FF; Wancho
        0x1E300, // 1E300..1E4CF; Unknown
        0x1E4D0, // 1E4D0..1E4F9; Nag_Mundari
        0x1E4FA, // 1E4FA..1E7DF; Unknown
        0x1E7E0, // 1E7E0..1E7E6; Ethiopic
        0x1E7E7, // 1E7E7; Unknown
        0x1E7E8, // 1E7E8..1E7EB; Ethiopic
        0x1E7EC, // 1E7EC; Unknown
        0x1E7ED, // 1E7ED..1E7EE; Ethiopic
        0x1E7EF, // 1E7EF; Unknown
        0x1E7F0, // 1E7F0..1E7FE; Ethiopic
        0x1E7FF, // 1E7FF; Unknown
        0x1E800, // 1E800..1E8C4; Mende_Kikakui
        0x1E8C5, // 1E8C5..1E8C6; Unknown
        0x1E8C7, // 1E8C7..1E8D6; Mende_Kikakui
        0x1E8D7, // 1E8D7..1E8FF; Unknown
        0x1E900, // 1E900..1E94B; Adlam
        0x1E94C, // 1E94C..1E94F; Unknown
        0x1E950, // 1E950..1E959; Adlam
        0x1E95A, // 1E95A..1E95D; Unknown
        0x1E95E, // 1E95E..1E95F; Adlam
        0x1E960, // 1E960..1EC70; Unknown
        0x1EC71, // 1EC71..1ECB4; Common
        0x1ECB5, // 1ECB5..1ED00; Unknown
        0x1ED01, // 1ED01..1ED3D; Common
        0x1ED3E, // 1ED3E..1EDFF; Unknown
        0x1EE00, // 1EE00..1EE03; Arabic
        0x1EE04, // 1EE04; Unknown
        0x1EE05, // 1EE05..1EE1F; Arabic
        0x1EE20, // 1EE20; Unknown
        0x1EE21, // 1EE21..1EE22; Arabic
        0x1EE23, // 1EE23; Unknown
        0x1EE24, // 1EE24; Arabic
        0x1EE25, // 1EE25..1EE26; Unknown
        0x1EE27, // 1EE27; Arabic
        0x1EE28, // 1EE28; Unknown
        0x1EE29, // 1EE29..1EE32; Arabic
        0x1EE33, // 1EE33; Unknown
        0x1EE34, // 1EE34..1EE37; Arabic
        0x1EE38, // 1EE38; Unknown
        0x1EE39, // 1EE39; Arabic
        0x1EE3A, // 1EE3A; Unknown
        0x1EE3B, // 1EE3B; Arabic
        0x1EE3C, // 1EE3C..1EE41; Unknown
        0x1EE42, // 1EE42; Arabic
        0x1EE43, // 1EE43..1EE46; Unknown
        0x1EE47, // 1EE47; Arabic
        0x1EE48, // 1EE48; Unknown
        0x1EE49, // 1EE49; Arabic
        0x1EE4A, // 1EE4A; Unknown
        0x1EE4B, // 1EE4B; Arabic
        0x1EE4C, // 1EE4C; Unknown
        0x1EE4D, // 1EE4D..1EE4F; Arabic
        0x1EE50, // 1EE50; Unknown
        0x1EE51, // 1EE51..1EE52; Arabic
        0x1EE53, // 1EE53; Unknown
        0x1EE54, // 1EE54; Arabic
        0x1EE55, // 1EE55..1EE56; Unknown
        0x1EE57, // 1EE57; Arabic
        0x1EE58, // 1EE58; Unknown
        0x1EE59, // 1EE59; Arabic
        0x1EE5A, // 1EE5A; Unknown
        0x1EE5B, // 1EE5B; Arabic
        0x1EE5C, // 1EE5C; Unknown
        0x1EE5D, // 1EE5D; Arabic
        0x1EE5E, // 1EE5E; Unknown
        0x1EE5F, // 1EE5F; Arabic
        0x1EE60, // 1EE60; Unknown
        0x1EE61, // 1EE61..1EE62; Arabic
        0x1EE63, // 1EE63; Unknown
        0x1EE64, // 1EE64; Arabic
        0x1EE65, // 1EE65..1EE66; Unknown
        0x1EE67, // 1EE67..1EE6A; Arabic
        0x1EE6B, // 1EE6B; Unknown
        0x1EE6C, // 1EE6C..1EE72; Arabic
        0x1EE73, // 1EE73; Unknown
        0x1EE74, // 1EE74..1EE77; Arabic
        0x1EE78, // 1EE78; Unknown
        0x1EE79, // 1EE79..1EE7C; Arabic
        0x1EE7D, // 1EE7D; Unknown
        0x1EE7E, // 1EE7E; Arabic
        0x1EE7F, // 1EE7F; Unknown
        0x1EE80, // 1EE80..1EE89; Arabic
        0x1EE8A, // 1EE8A; Unknown
        0x1EE8B, // 1EE8B..1EE9B; Arabic
        0x1EE9C, // 1EE9C..1EEA0; Unknown
        0x1EEA1, // 1EEA1..1EEA3; Arabic
        0x1EEA4, // 1EEA4; Unknown
        0x1EEA5, // 1EEA5..1EEA9; Arabic
        0x1EEAA, // 1EEAA; Unknown
        0x1EEAB, // 1EEAB..1EEBB; Arabic
        0x1EEBC, // 1EEBC..1EEEF; Unknown
        0x1EEF0, // 1EEF0..1EEF1; Arabic
        0x1EEF2, // 1EEF2..1EFFF; Unknown
        0x1F000, // 1F000..1F02B; Common
        0x1F02C, // 1F02C..1F02F; Unknown
        0x1F030, // 1F030..1F093; Common
        0x1F094, // 1F094..1F09F; Unknown
        0x1F0A0, // 1F0A0..1F0AE; Common
        0x1F0AF, // 1F0AF..1F0B0; Unknown
        0x1F0B1, // 1F0B1..1F0BF; Common
        0x1F0C0, // 1F0C0; Unknown
        0x1F0C1, // 1F0C1..1F0CF; Common
        0x1F0D0, // 1F0D0; Unknown
        0x1F0D1, // 1F0D1..1F0F5; Common
        0x1F0F6, // 1F0F6..1F0FF; Unknown
        0x1F100, // 1F100..1F1AD; Common
        0x1F1AE, // 1F1AE..1F1E5; Unknown
        0x1F1E6, // 1F1E6..1F1FF; Common
        0x1F200, // 1F200; Hiragana
        0x1F201, // 1F201..1F202; Common
        0x1F203, // 1F203..1F20F; Unknown
        0x1F210, // 1F210..1F23B; Common
        0x1F23C, // 1F23C..1F23F; Unknown
        0x1F240, // 1F240..1F248; Common
        0x1F249, // 1F249..1F24F; Unknown
        0x1F250, // 1F250..1F251; Common
        0x1F252, // 1F252..1F25F; Unknown
        0x1F260, // 1F260..1F265; Common
        0x1F266, // 1F266..1F2FF; Unknown
        0x1F300, // 1F300..1F6D7; Common
        0x1F6D8, // 1F6D8..1F6DB; Unknown
        0x1F6DC, // 1F6DC..1F6EC; Common
        0x1F6ED, // 1F6ED..1F6EF; Unknown
        0x1F6F0, // 1F6F0..1F6FC; Common
        0x1F6FD, // 1F6FD..1F6FF; Unknown
        0x1F700, // 1F700..1F776; Common
        0x1F777, // 1F777..1F77A; Unknown
        0x1F77B, // 1F77B..1F7D9; Common
        0x1F7DA, // 1F7DA..1F7DF; Unknown
        0x1F7E0, // 1F7E0..1F7EB; Common
        0x1F7EC, // 1F7EC..1F7EF; Unknown
        0x1F7F0, // 1F7F0; Common
        0x1F7F1, // 1F7F1..1F7FF; Unknown
        0x1F800, // 1F800..1F80B; Common
        0x1F80C, // 1F80C..1F80F; Unknown
        0x1F810, // 1F810..1F847; Common
        0x1F848, // 1F848..1F84F; Unknown
        0x1F850, // 1F850..1F859; Common
        0x1F85A, // 1F85A..1F85F; Unknown
        0x1F860, // 1F860..1F887; Common
        0x1F888, // 1F888..1F88F; Unknown
        0x1F890, // 1F890..1F8AD; Common
        0x1F8AE, // 1F8AE..1F8AF; Unknown
        0x1F8B0, // 1F8B0..1F8B1; Common
        0x1F8B2, // 1F8B2..1F8FF; Unknown
        0x1F900, // 1F900..1FA53; Common
        0x1FA54, // 1FA54..1FA5F; Unknown
        0x1FA60, // 1FA60..1FA6D; Common
        0x1FA6E, // 1FA6E..1FA6F; Unknown
        0x1FA70, // 1FA70..1FA7C; Common
        0x1FA7D, // 1FA7D..1FA7F; Unknown
        0x1FA80, // 1FA80..1FA88; Common
        0x1FA89, // 1FA89..1FA8F; Unknown
        0x1FA90, // 1FA90..1FABD; Common
        0x1FABE, // 1FABE; Unknown
        0x1FABF, // 1FABF..1FAC5; Common
        0x1FAC6, // 1FAC6..1FACD; Unknown
        0x1FACE, // 1FACE..1FADB; Common
        0x1FADC, // 1FADC..1FADF; Unknown
        0x1FAE0, // 1FAE0..1FAE8; Common
        0x1FAE9, // 1FAE9..1FAEF; Unknown
        0x1FAF0, // 1FAF0..1FAF8; Common
        0x1FAF9, // 1FAF9..1FAFF; Unknown
        0x1FB00, // 1FB00..1FB92; Common
        0x1FB93, // 1FB93; Unknown
        0x1FB94, // 1FB94..1FBCA; Common
        0x1FBCB, // 1FBCB..1FBEF; Unknown
        0x1FBF0, // 1FBF0..1FBF9; Common
        0x1FBFA, // 1FBFA..1FFFF; Unknown
        0x20000, // 20000..2A6DF; Han
        0x2A6E0, // 2A6E0..2A6FF; Unknown
        0x2A700, // 2A700..2B739; Han
        0x2B73A, // 2B73A..2B73F; Unknown
        0x2B740, // 2B740..2B81D; Han
        0x2B81E, // 2B81E..2B81F; Unknown
        0x2B820, // 2B820..2CEA1; Han
        0x2CEA2, // 2CEA2..2CEAF; Unknown
        0x2CEB0, // 2CEB0..2EBE0; Han
        0x2EBE1, // 2EBE1..2EBEF; Unknown
        0x2EBF0, // 2EBF0..2EE5D; Han
        0x2EE5E, // 2EE5E..2F7FF; Unknown
        0x2F800, // 2F800..2FA1D; Han
        0x2FA1E, // 2FA1E..2FFFF; Unknown
        0x30000, // 30000..3134A; Han
        0x3134B, // 3134B..3134F; Unknown
        0x31350, // 31350..323AF; Han
        0x323B0, // 323B0..E0000; Unknown
        0xE0001, // E0001; Common
        0xE0002, // E0002..E001F; Unknown
        0xE0020, // E0020..E007F; Common
        0xE0080, // E0080..E00FF; Unknown
        0xE0100, // E0100..E01EF; Inherited
        0xE01F0, // E01F0..10FFFF; Unknown
    };

    private static readonly UnicodeScript[] Scripts =
    {
        Common,	// 0000..0040
        Latin,	// 0041..005A
        Common,	// 005B..0060
        Latin,	// 0061..007A
        Common,	// 007B..00A9
        Latin,	// 00AA
        Common,	// 00AB..00B9
        Latin,	// 00BA
        Common,	// 00BB..00BF
        Latin,	// 00C0..00D6
        Common,	// 00D7
        Latin,	// 00D8..00F6
        Common,	// 00F7
        Latin,	// 00F8..02B8
        Common,	// 02B9..02DF
        Latin,	// 02E0..02E4
        Common,	// 02E5..02E9
        Bopomofo,	// 02EA..02EB
        Common,	// 02EC..02FF
        Inherited,	// 0300..036F
        Greek,	// 0370..0373
        Common,	// 0374
        Greek,	// 0375..0377
        Unknown,	// 0378..0379
        Greek,	// 037A..037D
        Common,	// 037E
        Greek,	// 037F
        Unknown,	// 0380..0383
        Greek,	// 0384
        Common,	// 0385
        Greek,	// 0386
        Common,	// 0387
        Greek,	// 0388..038A
        Unknown,	// 038B
        Greek,	// 038C
        Unknown,	// 038D
        Greek,	// 038E..03A1
        Unknown,	// 03A2
        Greek,	// 03A3..03E1
        Coptic,	// 03E2..03EF
        Greek,	// 03F0..03FF
        Cyrillic,	// 0400..0484
        Inherited,	// 0485..0486
        Cyrillic,	// 0487..052F
        Unknown,	// 0530
        Armenian,	// 0531..0556
        Unknown,	// 0557..0558
        Armenian,	// 0559..058A
        Unknown,	// 058B..058C
        Armenian,	// 058D..058F
        Unknown,	// 0590
        Hebrew,	// 0591..05C7
        Unknown,	// 05C8..05CF
        Hebrew,	// 05D0..05EA
        Unknown,	// 05EB..05EE
        Hebrew,	// 05EF..05F4
        Unknown,	// 05F5..05FF
        Arabic,	// 0600..0604
        Common,	// 0605
        Arabic,	// 0606..060B
        Common,	// 060C
        Arabic,	// 060D..061A
        Common,	// 061B
        Arabic,	// 061C..061E
        Common,	// 061F
        Arabic,	// 0620..063F
        Common,	// 0640
        Arabic,	// 0641..064A
        Inherited,	// 064B..0655
        Arabic,	// 0656..066F
        Inherited,	// 0670
        Arabic,	// 0671..06DC
        Common,	// 06DD
        Arabic,	// 06DE..06FF
        Syriac,	// 0700..070D
        Unknown,	// 070E
        Syriac,	// 070F..074A
        Unknown,	// 074B..074C
        Syriac,	// 074D..074F
        Arabic,	// 0750..077F
        Thaana,	// 0780..07B1
        Unknown,	// 07B2..07BF
        Nko,	// 07C0..07FA
        Unknown,	// 07FB..07FC
        Nko,	// 07FD..07FF
        Samaritan,	// 0800..082D
        Unknown,	// 082E..082F
        Samaritan,	// 0830..083E
        Unknown,	// 083F
        Mandaic,	// 0840..085B
        Unknown,	// 085C..085D
        Mandaic,	// 085E
        Unknown,	// 085F
        Syriac,	// 0860..086A
        Unknown,	// 086B..086F
        Arabic,	// 0870..088E
        Unknown,	// 088F
        Arabic,	// 0890..0891
        Unknown,	// 0892..0897
        Arabic,	// 0898..08E1
        Common,	// 08E2
        Arabic,	// 08E3..08FF
        Devanagari,	// 0900..0950
        Inherited,	// 0951..0954
        Devanagari,	// 0955..0963
        Common,	// 0964..0965
        Devanagari,	// 0966..097F
        Bengali,	// 0980..0983
        Unknown,	// 0984
        Bengali,	// 0985..098C
        Unknown,	// 098D..098E
        Bengali,	// 098F..0990
        Unknown,	// 0991..0992
        Bengali,	// 0993..09A8
        Unknown,	// 09A9
        Bengali,	// 09AA..09B0
        Unknown,	// 09B1
        Bengali,	// 09B2
        Unknown,	// 09B3..09B5
        Bengali,	// 09B6..09B9
        Unknown,	// 09BA..09BB
        Bengali,	// 09BC..09C4
        Unknown,	// 09C5..09C6
        Bengali,	// 09C7..09C8
        Unknown,	// 09C9..09CA
        Bengali,	// 09CB..09CE
        Unknown,	// 09CF..09D6
        Bengali,	// 09D7
        Unknown,	// 09D8..09DB
        Bengali,	// 09DC..09DD
        Unknown,	// 09DE
        Bengali,	// 09DF..09E3
        Unknown,	// 09E4..09E5
        Bengali,	// 09E6..09FE
        Unknown,	// 09FF..0A00
        Gurmukhi,	// 0A01..0A03
        Unknown,	// 0A04
        Gurmukhi,	// 0A05..0A0A
        Unknown,	// 0A0B..0A0E
        Gurmukhi,	// 0A0F..0A10
        Unknown,	// 0A11..0A12
        Gurmukhi,	// 0A13..0A28
        Unknown,	// 0A29
        Gurmukhi,	// 0A2A..0A30
        Unknown,	// 0A31
        Gurmukhi,	// 0A32..0A33
        Unknown,	// 0A34
        Gurmukhi,	// 0A35..0A36
        Unknown,	// 0A37
        Gurmukhi,	// 0A38..0A39
        Unknown,	// 0A3A..0A3B
        Gurmukhi,	// 0A3C
        Unknown,	// 0A3D
        Gurmukhi,	// 0A3E..0A42
        Unknown,	// 0A43..0A46
        Gurmukhi,	// 0A47..0A48
        Unknown,	// 0A49..0A4A
        Gurmukhi,	// 0A4B..0A4D
        Unknown,	// 0A4E..0A50
        Gurmukhi,	// 0A51
        Unknown,	// 0A52..0A58
        Gurmukhi,	// 0A59..0A5C
        Unknown,	// 0A5D
        Gurmukhi,	// 0A5E
        Unknown,	// 0A5F..0A65
        Gurmukhi,	// 0A66..0A76
        Unknown,	// 0A77..0A80
        Gujarati,	// 0A81..0A83
        Unknown,	// 0A84
        Gujarati,	// 0A85..0A8D
        Unknown,	// 0A8E
        Gujarati,	// 0A8F..0A91
        Unknown,	// 0A92
        Gujarati,	// 0A93..0AA8
        Unknown,	// 0AA9
        Gujarati,	// 0AAA..0AB0
        Unknown,	// 0AB1
        Gujarati,	// 0AB2..0AB3
        Unknown,	// 0AB4
        Gujarati,	// 0AB5..0AB9
        Unknown,	// 0ABA..0ABB
        Gujarati,	// 0ABC..0AC5
        Unknown,	// 0AC6
        Gujarati,	// 0AC7..0AC9
        Unknown,	// 0ACA
        Gujarati,	// 0ACB..0ACD
        Unknown,	// 0ACE..0ACF
        Gujarati,	// 0AD0
        Unknown,	// 0AD1..0ADF
        Gujarati,	// 0AE0..0AE3
        Unknown,	// 0AE4..0AE5
        Gujarati,	// 0AE6..0AF1
        Unknown,	// 0AF2..0AF8
        Gujarati,	// 0AF9..0AFF
        Unknown,	// 0B00
        Oriya,	// 0B01..0B03
        Unknown,	// 0B04
        Oriya,	// 0B05..0B0C
        Unknown,	// 0B0D..0B0E
        Oriya,	// 0B0F..0B10
        Unknown,	// 0B11..0B12
        Oriya,	// 0B13..0B28
        Unknown,	// 0B29
        Oriya,	// 0B2A..0B30
        Unknown,	// 0B31
        Oriya,	// 0B32..0B33
        Unknown,	// 0B34
        Oriya,	// 0B35..0B39
        Unknown,	// 0B3A..0B3B
        Oriya,	// 0B3C..0B44
        Unknown,	// 0B45..0B46
        Oriya,	// 0B47..0B48
        Unknown,	// 0B49..0B4A
        Oriya,	// 0B4B..0B4D
        Unknown,	// 0B4E..0B54
        Oriya,	// 0B55..0B57
        Unknown,	// 0B58..0B5B
        Oriya,	// 0B5C..0B5D
        Unknown,	// 0B5E
        Oriya,	// 0B5F..0B63
        Unknown,	// 0B64..0B65
        Oriya,	// 0B66..0B77
        Unknown,	// 0B78..0B81
        Tamil,	// 0B82..0B83
        Unknown,	// 0B84
        Tamil,	// 0B85..0B8A
        Unknown,	// 0B8B..0B8D
        Tamil,	// 0B8E..0B90
        Unknown,	// 0B91
        Tamil,	// 0B92..0B95
        Unknown,	// 0B96..0B98
        Tamil,	// 0B99..0B9A
        Unknown,	// 0B9B
        Tamil,	// 0B9C
        Unknown,	// 0B9D
        Tamil,	// 0B9E..0B9F
        Unknown,	// 0BA0..0BA2
        Tamil,	// 0BA3..0BA4
        Unknown,	// 0BA5..0BA7
        Tamil,	// 0BA8..0BAA
        Unknown,	// 0BAB..0BAD
        Tamil,	// 0BAE..0BB9
        Unknown,	// 0BBA..0BBD
        Tamil,	// 0BBE..0BC2
        Unknown,	// 0BC3..0BC5
        Tamil,	// 0BC6..0BC8
        Unknown,	// 0BC9
        Tamil,	// 0BCA..0BCD
        Unknown,	// 0BCE..0BCF
        Tamil,	// 0BD0
        Unknown,	// 0BD1..0BD6
        Tamil,	// 0BD7
        Unknown,	// 0BD8..0BE5
        Tamil,	// 0BE6..0BFA
        Unknown,	// 0BFB..0BFF
        Telugu,	// 0C00..0C0C
        Unknown,	// 0C0D
        Telugu,	// 0C0E..0C10
        Unknown,	// 0C11
        Telugu,	// 0C12..0C28
        Unknown,	// 0C29
        Telugu,	// 0C2A..0C39
        Unknown,	// 0C3A..0C3B
        Telugu,	// 0C3C..0C44
        Unknown,	// 0C45
        Telugu,	// 0C46..0C48
        Unknown,	// 0C49
        Telugu,	// 0C4A..0C4D
        Unknown,	// 0C4E..0C54
        Telugu,	// 0C55..0C56
        Unknown,	// 0C57
        Telugu,	// 0C58..0C5A
        Unknown,	// 0C5B..0C5C
        Telugu,	// 0C5D
        Unknown,	// 0C5E..0C5F
        Telugu,	// 0C60..0C63
        Unknown,	// 0C64..0C65
        Telugu,	// 0C66..0C6F
        Unknown,	// 0C70..0C76
        Telugu,	// 0C77..0C7F
        Kannada,	// 0C80..0C8C
        Unknown,	// 0C8D
        Kannada,	// 0C8E..0C90
        Unknown,	// 0C91
        Kannada,	// 0C92..0CA8
        Unknown,	// 0CA9
        Kannada,	// 0CAA..0CB3
        Unknown,	// 0CB4
        Kannada,	// 0CB5..0CB9
        Unknown,	// 0CBA..0CBB
        Kannada,	// 0CBC..0CC4
        Unknown,	// 0CC5
        Kannada,	// 0CC6..0CC8
        Unknown,	// 0CC9
        Kannada,	// 0CCA..0CCD
        Unknown,	// 0CCE..0CD4
        Kannada,	// 0CD5..0CD6
        Unknown,	// 0CD7..0CDC
        Kannada,	// 0CDD..0CDE
        Unknown,	// 0CDF
        Kannada,	// 0CE0..0CE3
        Unknown,	// 0CE4..0CE5
        Kannada,	// 0CE6..0CEF
        Unknown,	// 0CF0
        Kannada,	// 0CF1..0CF3
        Unknown,	// 0CF4..0CFF
        Malayalam,	// 0D00..0D0C
        Unknown,	// 0D0D
        Malayalam,	// 0D0E..0D10
        Unknown,	// 0D11
        Malayalam,	// 0D12..0D44
        Unknown,	// 0D45
        Malayalam,	// 0D46..0D48
        Unknown,	// 0D49
        Malayalam,	// 0D4A..0D4F
        Unknown,	// 0D50..0D53
        Malayalam,	// 0D54..0D63
        Unknown,	// 0D64..0D65
        Malayalam,	// 0D66..0D7F
        Unknown,	// 0D80
        Sinhala,	// 0D81..0D83
        Unknown,	// 0D84
        Sinhala,	// 0D85..0D96
        Unknown,	// 0D97..0D99
        Sinhala,	// 0D9A..0DB1
        Unknown,	// 0DB2
        Sinhala,	// 0DB3..0DBB
        Unknown,	// 0DBC
        Sinhala,	// 0DBD
        Unknown,	// 0DBE..0DBF
        Sinhala,	// 0DC0..0DC6
        Unknown,	// 0DC7..0DC9
        Sinhala,	// 0DCA
        Unknown,	// 0DCB..0DCE
        Sinhala,	// 0DCF..0DD4
        Unknown,	// 0DD5
        Sinhala,	// 0DD6
        Unknown,	// 0DD7
        Sinhala,	// 0DD8..0DDF
        Unknown,	// 0DE0..0DE5
        Sinhala,	// 0DE6..0DEF
        Unknown,	// 0DF0..0DF1
        Sinhala,	// 0DF2..0DF4
        Unknown,	// 0DF5..0E00
        Thai,	// 0E01..0E3A
        Unknown,	// 0E3B..0E3E
        Common,	// 0E3F
        Thai,	// 0E40..0E5B
        Unknown,	// 0E5C..0E80
        Lao,	// 0E81..0E82
        Unknown,	// 0E83
        Lao,	// 0E84
        Unknown,	// 0E85
        Lao,	// 0E86..0E8A
        Unknown,	// 0E8B
        Lao,	// 0E8C..0EA3
        Unknown,	// 0EA4
        Lao,	// 0EA5
        Unknown,	// 0EA6
        Lao,	// 0EA7..0EBD
        Unknown,	// 0EBE..0EBF
        Lao,	// 0EC0..0EC4
        Unknown,	// 0EC5
        Lao,	// 0EC6
        Unknown,	// 0EC7
        Lao,	// 0EC8..0ECE
        Unknown,	// 0ECF
        Lao,	// 0ED0..0ED9
        Unknown,	// 0EDA..0EDB
        Lao,	// 0EDC..0EDF
        Unknown,	// 0EE0..0EFF
        Tibetan,	// 0F00..0F47
        Unknown,	// 0F48
        Tibetan,	// 0F49..0F6C
        Unknown,	// 0F6D..0F70
        Tibetan,	// 0F71..0F97
        Unknown,	// 0F98
        Tibetan,	// 0F99..0FBC
        Unknown,	// 0FBD
        Tibetan,	// 0FBE..0FCC
        Unknown,	// 0FCD
        Tibetan,	// 0FCE..0FD4
        Common,	// 0FD5..0FD8
        Tibetan,	// 0FD9..0FDA
        Unknown,	// 0FDB..0FFF
        Myanmar,	// 1000..109F
        Georgian,	// 10A0..10C5
        Unknown,	// 10C6
        Georgian,	// 10C7
        Unknown,	// 10C8..10CC
        Georgian,	// 10CD
        Unknown,	// 10CE..10CF
        Georgian,	// 10D0..10FA
        Common,	// 10FB
        Georgian,	// 10FC..10FF
        Hangul,	// 1100..11FF
        Ethiopic,	// 1200..1248
        Unknown,	// 1249
        Ethiopic,	// 124A..124D
        Unknown,	// 124E..124F
        Ethiopic,	// 1250..1256
        Unknown,	// 1257
        Ethiopic,	// 1258
        Unknown,	// 1259
        Ethiopic,	// 125A..125D
        Unknown,	// 125E..125F
        Ethiopic,	// 1260..1288
        Unknown,	// 1289
        Ethiopic,	// 128A..128D
        Unknown,	// 128E..128F
        Ethiopic,	// 1290..12B0
        Unknown,	// 12B1
        Ethiopic,	// 12B2..12B5
        Unknown,	// 12B6..12B7
        Ethiopic,	// 12B8..12BE
        Unknown,	// 12BF
        Ethiopic,	// 12C0
        Unknown,	// 12C1
        Ethiopic,	// 12C2..12C5
        Unknown,	// 12C6..12C7
        Ethiopic,	// 12C8..12D6
        Unknown,	// 12D7
        Ethiopic,	// 12D8..1310
        Unknown,	// 1311
        Ethiopic,	// 1312..1315
        Unknown,	// 1316..1317
        Ethiopic,	// 1318..135A
        Unknown,	// 135B..135C
        Ethiopic,	// 135D..137C
        Unknown,	// 137D..137F
        Ethiopic,	// 1380..1399
        Unknown,	// 139A..139F
        Cherokee,	// 13A0..13F5
        Unknown,	// 13F6..13F7
        Cherokee,	// 13F8..13FD
        Unknown,	// 13FE..13FF
        CanadianAboriginal,	// 1400..167F
        Ogham,	// 1680..169C
        Unknown,	// 169D..169F
        Runic,	// 16A0..16EA
        Common,	// 16EB..16ED
        Runic,	// 16EE..16F8
        Unknown,	// 16F9..16FF
        Tagalog,	// 1700..1715
        Unknown,	// 1716..171E
        Tagalog,	// 171F
        Hanunoo,	// 1720..1734
        Common,	// 1735..1736
        Unknown,	// 1737..173F
        Buhid,	// 1740..1753
        Unknown,	// 1754..175F
        Tagbanwa,	// 1760..176C
        Unknown,	// 176D
        Tagbanwa,	// 176E..1770
        Unknown,	// 1771
        Tagbanwa,	// 1772..1773
        Unknown,	// 1774..177F
        Khmer,	// 1780..17DD
        Unknown,	// 17DE..17DF
        Khmer,	// 17E0..17E9
        Unknown,	// 17EA..17EF
        Khmer,	// 17F0..17F9
        Unknown,	// 17FA..17FF
        Mongolian,	// 1800..1801
        Common,	// 1802..1803
        Mongolian,	// 1804
        Common,	// 1805
        Mongolian,	// 1806..1819
        Unknown,	// 181A..181F
        Mongolian,	// 1820..1878
        Unknown,	// 1879..187F
        Mongolian,	// 1880..18AA
        Unknown,	// 18AB..18AF
        CanadianAboriginal,	// 18B0..18F5
        Unknown,	// 18F6..18FF
        Limbu,	// 1900..191E
        Unknown,	// 191F
        Limbu,	// 1920..192B
        Unknown,	// 192C..192F
        Limbu,	// 1930..193B
        Unknown,	// 193C..193F
        Limbu,	// 1940
        Unknown,	// 1941..1943
        Limbu,	// 1944..194F
        TaiLe,	// 1950..196D
        Unknown,	// 196E..196F
        TaiLe,	// 1970..1974
        Unknown,	// 1975..197F
        NewTaiLue,	// 1980..19AB
        Unknown,	// 19AC..19AF
        NewTaiLue,	// 19B0..19C9
        Unknown,	// 19CA..19CF
        NewTaiLue,	// 19D0..19DA
        Unknown,	// 19DB..19DD
        NewTaiLue,	// 19DE..19DF
        Khmer,	// 19E0..19FF
        Buginese,	// 1A00..1A1B
        Unknown,	// 1A1C..1A1D
        Buginese,	// 1A1E..1A1F
        TaiTham,	// 1A20..1A5E
        Unknown,	// 1A5F
        TaiTham,	// 1A60..1A7C
        Unknown,	// 1A7D..1A7E
        TaiTham,	// 1A7F..1A89
        Unknown,	// 1A8A..1A8F
        TaiTham,	// 1A90..1A99
        Unknown,	// 1A9A..1A9F
        TaiTham,	// 1AA0..1AAD
        Unknown,	// 1AAE..1AAF
        Inherited,	// 1AB0..1ACE
        Unknown,	// 1ACF..1AFF
        Balinese,	// 1B00..1B4C
        Unknown,	// 1B4D..1B4F
        Balinese,	// 1B50..1B7E
        Unknown,	// 1B7F
        Sundanese,	// 1B80..1BBF
        Batak,	// 1BC0..1BF3
        Unknown,	// 1BF4..1BFB
        Batak,	// 1BFC..1BFF
        Lepcha,	// 1C00..1C37
        Unknown,	// 1C38..1C3A
        Lepcha,	// 1C3B..1C49
        Unknown,	// 1C4A..1C4C
        Lepcha,	// 1C4D..1C4F
        OlChiki,	// 1C50..1C7F
        Cyrillic,	// 1C80..1C88
        Unknown,	// 1C89..1C8F
        Georgian,	// 1C90..1CBA
        Unknown,	// 1CBB..1CBC
        Georgian,	// 1CBD..1CBF
        Sundanese,	// 1CC0..1CC7
        Unknown,	// 1CC8..1CCF
        Inherited,	// 1CD0..1CD2
        Common,	// 1CD3
        Inherited,	// 1CD4..1CE0
        Common,	// 1CE1
        Inherited,	// 1CE2..1CE8
        Common,	// 1CE9..1CEC
        Inherited,	// 1CED
        Common,	// 1CEE..1CF3
        Inherited,	// 1CF4
        Common,	// 1CF5..1CF7
        Inherited,	// 1CF8..1CF9
        Common,	// 1CFA
        Unknown,	// 1CFB..1CFF
        Latin,	// 1D00..1D25
        Greek,	// 1D26..1D2A
        Cyrillic,	// 1D2B
        Latin,	// 1D2C..1D5C
        Greek,	// 1D5D..1D61
        Latin,	// 1D62..1D65
        Greek,	// 1D66..1D6A
        Latin,	// 1D6B..1D77
        Cyrillic,	// 1D78
        Latin,	// 1D79..1DBE
        Greek,	// 1DBF
        Inherited,	// 1DC0..1DFF
        Latin,	// 1E00..1EFF
        Greek,	// 1F00..1F15
        Unknown,	// 1F16..1F17
        Greek,	// 1F18..1F1D
        Unknown,	// 1F1E..1F1F
        Greek,	// 1F20..1F45
        Unknown,	// 1F46..1F47
        Greek,	// 1F48..1F4D
        Unknown,	// 1F4E..1F4F
        Greek,	// 1F50..1F57
        Unknown,	// 1F58
        Greek,	// 1F59
        Unknown,	// 1F5A
        Greek,	// 1F5B
        Unknown,	// 1F5C
        Greek,	// 1F5D
        Unknown,	// 1F5E
        Greek,	// 1F5F..1F7D
        Unknown,	// 1F7E..1F7F
        Greek,	// 1F80..1FB4
        Unknown,	// 1FB5
        Greek,	// 1FB6..1FC4
        Unknown,	// 1FC5
        Greek,	// 1FC6..1FD3
        Unknown,	// 1FD4..1FD5
        Greek,	// 1FD6..1FDB
        Unknown,	// 1FDC
        Greek,	// 1FDD..1FEF
        Unknown,	// 1FF0..1FF1
        Greek,	// 1FF2..1FF4
        Unknown,	// 1FF5
        Greek,	// 1FF6..1FFE
        Unknown,	// 1FFF
        Common,	// 2000..200B
        Inherited,	// 200C..200D
        Common,	// 200E..2064
        Unknown,	// 2065
        Common,	// 2066..2070
        Latin,	// 2071
        Unknown,	// 2072..2073
        Common,	// 2074..207E
        Latin,	// 207F
        Common,	// 2080..208E
        Unknown,	// 208F
        Latin,	// 2090..209C
        Unknown,	// 209D..209F
        Common,	// 20A0..20C0
        Unknown,	// 20C1..20CF
        Inherited,	// 20D0..20F0
        Unknown,	// 20F1..20FF
        Common,	// 2100..2125
        Greek,	// 2126
        Common,	// 2127..2129
        Latin,	// 212A..212B
        Common,	// 212C..2131
        Latin,	// 2132
        Common,	// 2133..214D
        Latin,	// 214E
        Common,	// 214F..215F
        Latin,	// 2160..2188
        Common,	// 2189..218B
        Unknown,	// 218C..218F
        Common,	// 2190..2426
        Unknown,	// 2427..243F
        Common,	// 2440..244A
        Unknown,	// 244B..245F
        Common,	// 2460..27FF
        Braille,	// 2800..28FF
        Common,	// 2900..2B73
        Unknown,	// 2B74..2B75
        Common,	// 2B76..2B95
        Unknown,	// 2B96
        Common,	// 2B97..2BFF
        Glagolitic,	// 2C00..2C5F
        Latin,	// 2C60..2C7F
        Coptic,	// 2C80..2CF3
        Unknown,	// 2CF4..2CF8
        Coptic,	// 2CF9..2CFF
        Georgian,	// 2D00..2D25
        Unknown,	// 2D26
        Georgian,	// 2D27
        Unknown,	// 2D28..2D2C
        Georgian,	// 2D2D
        Unknown,	// 2D2E..2D2F
        Tifinagh,	// 2D30..2D67
        Unknown,	// 2D68..2D6E
        Tifinagh,	// 2D6F..2D70
        Unknown,	// 2D71..2D7E
        Tifinagh,	// 2D7F
        Ethiopic,	// 2D80..2D96
        Unknown,	// 2D97..2D9F
        Ethiopic,	// 2DA0..2DA6
        Unknown,	// 2DA7
        Ethiopic,	// 2DA8..2DAE
        Unknown,	// 2DAF
        Ethiopic,	// 2DB0..2DB6
        Unknown,	// 2DB7
        Ethiopic,	// 2DB8..2DBE
        Unknown,	// 2DBF
        Ethiopic,	// 2DC0..2DC6
        Unknown,	// 2DC7
        Ethiopic,	// 2DC8..2DCE
        Unknown,	// 2DCF
        Ethiopic,	// 2DD0..2DD6
        Unknown,	// 2DD7
        Ethiopic,	// 2DD8..2DDE
        Unknown,	// 2DDF
        Cyrillic,	// 2DE0..2DFF
        Common,	// 2E00..2E5D
        Unknown,	// 2E5E..2E7F
        Han,	// 2E80..2E99
        Unknown,	// 2E9A
        Han,	// 2E9B..2EF3
        Unknown,	// 2EF4..2EFF
        Han,	// 2F00..2FD5
        Unknown,	// 2FD6..2FEF
        Common,	// 2FF0..3004
        Han,	// 3005
        Common,	// 3006
        Han,	// 3007
        Common,	// 3008..3020
        Han,	// 3021..3029
        Inherited,	// 302A..302D
        Hangul,	// 302E..302F
        Common,	// 3030..3037
        Han,	// 3038..303B
        Common,	// 303C..303F
        Unknown,	// 3040
        Hiragana,	// 3041..3096
        Unknown,	// 3097..3098
        Inherited,	// 3099..309A
        Common,	// 309B..309C
        Hiragana,	// 309D..309F
        Common,	// 30A0
        Katakana,	// 30A1..30FA
        Common,	// 30FB..30FC
        Katakana,	// 30FD..30FF
        Unknown,	// 3100..3104
        Bopomofo,	// 3105..312F
        Unknown,	// 3130
        Hangul,	// 3131..318E
        Unknown,	// 318F
        Common,	// 3190..319F
        Bopomofo,	// 31A0..31BF
        Common,	// 31C0..31E3
        Unknown,	// 31E4..31EE
        Common,	// 31EF
        Katakana,	// 31F0..31FF
        Hangul,	// 3200..321E
        Unknown,	// 321F
        Common,	// 3220..325F
        Hangul,	// 3260..327E
        Common,	// 327F..32CF
        Katakana,	// 32D0..32FE
        Common,	// 32FF
        Katakana,	// 3300..3357
        Common,	// 3358..33FF
        Han,	// 3400..4DBF
        Common,	// 4DC0..4DFF
        Han,	// 4E00..9FFF
        Yi,	// A000..A48C
        Unknown,	// A48D..A48F
        Yi,	// A490..A4C6
        Unknown,	// A4C7..A4CF
        Lisu,	// A4D0..A4FF
        Vai,	// A500..A62B
        Unknown,	// A62C..A63F
        Cyrillic,	// A640..A69F
        Bamum,	// A6A0..A6F7
        Unknown,	// A6F8..A6FF
        Common,	// A700..A721
        Latin,	// A722..A787
        Common,	// A788..A78A
        Latin,	// A78B..A7CA
        Unknown,	// A7CB..A7CF
        Latin,	// A7D0..A7D1
        Unknown,	// A7D2
        Latin,	// A7D3
        Unknown,	// A7D4
        Latin,	// A7D5..A7D9
        Unknown,	// A7DA..A7F1
        Latin,	// A7F2..A7FF
        SylotiNagri,	// A800..A82C
        Unknown,	// A82D..A82F
        Common,	// A830..A839
        Unknown,	// A83A..A83F
        PhagsPa,	// A840..A877
        Unknown,	// A878..A87F
        Saurashtra,	// A880..A8C5
        Unknown,	// A8C6..A8CD
        Saurashtra,	// A8CE..A8D9
        Unknown,	// A8DA..A8DF
        Devanagari,	// A8E0..A8FF
        KayahLi,	// A900..A92D
        Common,	// A92E
        KayahLi,	// A92F
        Rejang,	// A930..A953
        Unknown,	// A954..A95E
        Rejang,	// A95F
        Hangul,	// A960..A97C
        Unknown,	// A97D..A97F
        Javanese,	// A980..A9CD
        Unknown,	// A9CE
        Common,	// A9CF
        Javanese,	// A9D0..A9D9
        Unknown,	// A9DA..A9DD
        Javanese,	// A9DE..A9DF
        Myanmar,	// A9E0..A9FE
        Unknown,	// A9FF
        Cham,	// AA00..AA36
        Unknown,	// AA37..AA3F
        Cham,	// AA40..AA4D
        Unknown,	// AA4E..AA4F
        Cham,	// AA50..AA59
        Unknown,	// AA5A..AA5B
        Cham,	// AA5C..AA5F
        Myanmar,	// AA60..AA7F
        TaiViet,	// AA80..AAC2
        Unknown,	// AAC3..AADA
        TaiViet,	// AADB..AADF
        MeeteiMayek,	// AAE0..AAF6
        Unknown,	// AAF7..AB00
        Ethiopic,	// AB01..AB06
        Unknown,	// AB07..AB08
        Ethiopic,	// AB09..AB0E
        Unknown,	// AB0F..AB10
        Ethiopic,	// AB11..AB16
        Unknown,	// AB17..AB1F
        Ethiopic,	// AB20..AB26
        Unknown,	// AB27
        Ethiopic,	// AB28..AB2E
        Unknown,	// AB2F
        Latin,	// AB30..AB5A
        Common,	// AB5B
        Latin,	// AB5C..AB64
        Greek,	// AB65
        Latin,	// AB66..AB69
        Common,	// AB6A..AB6B
        Unknown,	// AB6C..AB6F
        Cherokee,	// AB70..ABBF
        MeeteiMayek,	// ABC0..ABED
        Unknown,	// ABEE..ABEF
        MeeteiMayek,	// ABF0..ABF9
        Unknown,	// ABFA..ABFF
        Hangul,	// AC00..D7A3
        Unknown,	// D7A4..D7AF
        Hangul,	// D7B0..D7C6
        Unknown,	// D7C7..D7CA
        Hangul,	// D7CB..D7FB
        Unknown,	// D7FC..F8FF
        Han,	// F900..FA6D
        Unknown,	// FA6E..FA6F
        Han,	// FA70..FAD9
        Unknown,	// FADA..FAFF
        Latin,	// FB00..FB06
        Unknown,	// FB07..FB12
        Armenian,	// FB13..FB17
        Unknown,	// FB18..FB1C
        Hebrew,	// FB1D..FB36
        Unknown,	// FB37
        Hebrew,	// FB38..FB3C
        Unknown,	// FB3D
        Hebrew,	// FB3E
        Unknown,	// FB3F
        Hebrew,	// FB40..FB41
        Unknown,	// FB42
        Hebrew,	// FB43..FB44
        Unknown,	// FB45
        Hebrew,	// FB46..FB4F
        Arabic,	// FB50..FBC2
        Unknown,	// FBC3..FBD2
        Arabic,	// FBD3..FD3D
        Common,	// FD3E..FD3F
        Arabic,	// FD40..FD8F
        Unknown,	// FD90..FD91
        Arabic,	// FD92..FDC7
        Unknown,	// FDC8..FDCE
        Arabic,	// FDCF
        Unknown,	// FDD0..FDEF
        Arabic,	// FDF0..FDFF
        Inherited,	// FE00..FE0F
        Common,	// FE10..FE19
        Unknown,	// FE1A..FE1F
        Inherited,	// FE20..FE2D
        Cyrillic,	// FE2E..FE2F
        Common,	// FE30..FE52
        Unknown,	// FE53
        Common,	// FE54..FE66
        Unknown,	// FE67
        Common,	// FE68..FE6B
        Unknown,	// FE6C..FE6F
        Arabic,	// FE70..FE74
        Unknown,	// FE75
        Arabic,	// FE76..FEFC
        Unknown,	// FEFD..FEFE
        Common,	// FEFF
        Unknown,	// FF00
        Common,	// FF01..FF20
        Latin,	// FF21..FF3A
        Common,	// FF3B..FF40
        Latin,	// FF41..FF5A
        Common,	// FF5B..FF65
        Katakana,	// FF66..FF6F
        Common,	// FF70
        Katakana,	// FF71..FF9D
        Common,	// FF9E..FF9F
        Hangul,	// FFA0..FFBE
        Unknown,	// FFBF..FFC1
        Hangul,	// FFC2..FFC7
        Unknown,	// FFC8..FFC9
        Hangul,	// FFCA..FFCF
        Unknown,	// FFD0..FFD1
        Hangul,	// FFD2..FFD7
        Unknown,	// FFD8..FFD9
        Hangul,	// FFDA..FFDC
        Unknown,	// FFDD..FFDF
        Common,	// FFE0..FFE6
        Unknown,	// FFE7
        Common,	// FFE8..FFEE
        Unknown,	// FFEF..FFF8
        Common,	// FFF9..FFFD
        Unknown,	// FFFE..FFFF
        LinearB,	// 10000..1000B
        Unknown,	// 1000C
        LinearB,	// 1000D..10026
        Unknown,	// 10027
        LinearB,	// 10028..1003A
        Unknown,	// 1003B
        LinearB,	// 1003C..1003D
        Unknown,	// 1003E
        LinearB,	// 1003F..1004D
        Unknown,	// 1004E..1004F
        LinearB,	// 10050..1005D
        Unknown,	// 1005E..1007F
        LinearB,	// 10080..100FA
        Unknown,	// 100FB..100FF
        Common,	// 10100..10102
        Unknown,	// 10103..10106
        Common,	// 10107..10133
        Unknown,	// 10134..10136
        Common,	// 10137..1013F
        Greek,	// 10140..1018E
        Unknown,	// 1018F
        Common,	// 10190..1019C
        Unknown,	// 1019D..1019F
        Greek,	// 101A0
        Unknown,	// 101A1..101CF
        Common,	// 101D0..101FC
        Inherited,	// 101FD
        Unknown,	// 101FE..1027F
        Lycian,	// 10280..1029C
        Unknown,	// 1029D..1029F
        Carian,	// 102A0..102D0
        Unknown,	// 102D1..102DF
        Inherited,	// 102E0
        Common,	// 102E1..102FB
        Unknown,	// 102FC..102FF
        OldItalic,	// 10300..10323
        Unknown,	// 10324..1032C
        OldItalic,	// 1032D..1032F
        Gothic,	// 10330..1034A
        Unknown,	// 1034B..1034F
        OldPermic,	// 10350..1037A
        Unknown,	// 1037B..1037F
        Ugaritic,	// 10380..1039D
        Unknown,	// 1039E
        Ugaritic,	// 1039F
        OldPersian,	// 103A0..103C3
        Unknown,	// 103C4..103C7
        OldPersian,	// 103C8..103D5
        Unknown,	// 103D6..103FF
        Deseret,	// 10400..1044F
        Shavian,	// 10450..1047F
        Osmanya,	// 10480..1049D
        Unknown,	// 1049E..1049F
        Osmanya,	// 104A0..104A9
        Unknown,	// 104AA..104AF
        Osage,	// 104B0..104D3
        Unknown,	// 104D4..104D7
        Osage,	// 104D8..104FB
        Unknown,	// 104FC..104FF
        Elbasan,	// 10500..10527
        Unknown,	// 10528..1052F
        CaucasianAlbanian,	// 10530..10563
        Unknown,	// 10564..1056E
        CaucasianAlbanian,	// 1056F
        Vithkuqi,	// 10570..1057A
        Unknown,	// 1057B
        Vithkuqi,	// 1057C..1058A
        Unknown,	// 1058B
        Vithkuqi,	// 1058C..10592
        Unknown,	// 10593
        Vithkuqi,	// 10594..10595
        Unknown,	// 10596
        Vithkuqi,	// 10597..105A1
        Unknown,	// 105A2
        Vithkuqi,	// 105A3..105B1
        Unknown,	// 105B2
        Vithkuqi,	// 105B3..105B9
        Unknown,	// 105BA
        Vithkuqi,	// 105BB..105BC
        Unknown,	// 105BD..105FF
        LinearA,	// 10600..10736
        Unknown,	// 10737..1073F
        LinearA,	// 10740..10755
        Unknown,	// 10756..1075F
        LinearA,	// 10760..10767
        Unknown,	// 10768..1077F
        Latin,	// 10780..10785
        Unknown,	// 10786
        Latin,	// 10787..107B0
        Unknown,	// 107B1
        Latin,	// 107B2..107BA
        Unknown,	// 107BB..107FF
        Cypriot,	// 10800..10805
        Unknown,	// 10806..10807
        Cypriot,	// 10808
        Unknown,	// 10809
        Cypriot,	// 1080A..10835
        Unknown,	// 10836
        Cypriot,	// 10837..10838
        Unknown,	// 10839..1083B
        Cypriot,	// 1083C
        Unknown,	// 1083D..1083E
        Cypriot,	// 1083F
        ImperialAramaic,	// 10840..10855
        Unknown,	// 10856
        ImperialAramaic,	// 10857..1085F
        Palmyrene,	// 10860..1087F
        Nabataean,	// 10880..1089E
        Unknown,	// 1089F..108A6
        Nabataean,	// 108A7..108AF
        Unknown,	// 108B0..108DF
        Hatran,	// 108E0..108F2
        Unknown,	// 108F3
        Hatran,	// 108F4..108F5
        Unknown,	// 108F6..108FA
        Hatran,	// 108FB..108FF
        Phoenician,	// 10900..1091B
        Unknown,	// 1091C..1091E
        Phoenician,	// 1091F
        Lydian,	// 10920..10939
        Unknown,	// 1093A..1093E
        Lydian,	// 1093F
        Unknown,	// 10940..1097F
        MeroiticHieroglyphs,	// 10980..1099F
        MeroiticCursive,	// 109A0..109B7
        Unknown,	// 109B8..109BB
        MeroiticCursive,	// 109BC..109CF
        Unknown,	// 109D0..109D1
        MeroiticCursive,	// 109D2..109FF
        Kharoshthi,	// 10A00..10A03
        Unknown,	// 10A04
        Kharoshthi,	// 10A05..10A06
        Unknown,	// 10A07..10A0B
        Kharoshthi,	// 10A0C..10A13
        Unknown,	// 10A14
        Kharoshthi,	// 10A15..10A17
        Unknown,	// 10A18
        Kharoshthi,	// 10A19..10A35
        Unknown,	// 10A36..10A37
        Kharoshthi,	// 10A38..10A3A
        Unknown,	// 10A3B..10A3E
        Kharoshthi,	// 10A3F..10A48
        Unknown,	// 10A49..10A4F
        Kharoshthi,	// 10A50..10A58
        Unknown,	// 10A59..10A5F
        OldSouthArabian,	// 10A60..10A7F
        OldNorthArabian,	// 10A80..10A9F
        Unknown,	// 10AA0..10ABF
        Manichaean,	// 10AC0..10AE6
        Unknown,	// 10AE7..10AEA
        Manichaean,	// 10AEB..10AF6
        Unknown,	// 10AF7..10AFF
        Avestan,	// 10B00..10B35
        Unknown,	// 10B36..10B38
        Avestan,	// 10B39..10B3F
        InscriptionalParthian,	// 10B40..10B55
        Unknown,	// 10B56..10B57
        InscriptionalParthian,	// 10B58..10B5F
        InscriptionalPahlavi,	// 10B60..10B72
        Unknown,	// 10B73..10B77
        InscriptionalPahlavi,	// 10B78..10B7F
        PsalterPahlavi,	// 10B80..10B91
        Unknown,	// 10B92..10B98
        PsalterPahlavi,	// 10B99..10B9C
        Unknown,	// 10B9D..10BA8
        PsalterPahlavi,	// 10BA9..10BAF
        Unknown,	// 10BB0..10BFF
        OldTurkic,	// 10C00..10C48
        Unknown,	// 10C49..10C7F
        OldHungarian,	// 10C80..10CB2
        Unknown,	// 10CB3..10CBF
        OldHungarian,	// 10CC0..10CF2
        Unknown,	// 10CF3..10CF9
        OldHungarian,	// 10CFA..10CFF
        HanifiRohingya,	// 10D00..10D27
        Unknown,	// 10D28..10D2F
        HanifiRohingya,	// 10D30..10D39
        Unknown,	// 10D3A..10E5F
        Arabic,	// 10E60..10E7E
        Unknown,	// 10E7F
        Yezidi,	// 10E80..10EA9
        Unknown,	// 10EAA
        Yezidi,	// 10EAB..10EAD
        Unknown,	// 10EAE..10EAF
        Yezidi,	// 10EB0..10EB1
        Unknown,	// 10EB2..10EFC
        Arabic,	// 10EFD..10EFF
        OldSogdian,	// 10F00..10F27
        Unknown,	// 10F28..10F2F
        Sogdian,	// 10F30..10F59
        Unknown,	// 10F5A..10F6F
        OldUyghur,	// 10F70..10F89
        Unknown,	// 10F8A..10FAF
        Chorasmian,	// 10FB0..10FCB
        Unknown,	// 10FCC..10FDF
        Elymaic,	// 10FE0..10FF6
        Unknown,	// 10FF7..10FFF
        Brahmi,	// 11000..1104D
        Unknown,	// 1104E..11051
        Brahmi,	// 11052..11075
        Unknown,	// 11076..1107E
        Brahmi,	// 1107F
        Kaithi,	// 11080..110C2
        Unknown,	// 110C3..110CC
        Kaithi,	// 110CD
        Unknown,	// 110CE..110CF
        SoraSompeng,	// 110D0..110E8
        Unknown,	// 110E9..110EF
        SoraSompeng,	// 110F0..110F9
        Unknown,	// 110FA..110FF
        Chakma,	// 11100..11134
        Unknown,	// 11135
        Chakma,	// 11136..11147
        Unknown,	// 11148..1114F
        Mahajani,	// 11150..11176
        Unknown,	// 11177..1117F
        Sharada,	// 11180..111DF
        Unknown,	// 111E0
        Sinhala,	// 111E1..111F4
        Unknown,	// 111F5..111FF
        Khojki,	// 11200..11211
        Unknown,	// 11212
        Khojki,	// 11213..11241
        Unknown,	// 11242..1127F
        Multani,	// 11280..11286
        Unknown,	// 11287
        Multani,	// 11288
        Unknown,	// 11289
        Multani,	// 1128A..1128D
        Unknown,	// 1128E
        Multani,	// 1128F..1129D
        Unknown,	// 1129E
        Multani,	// 1129F..112A9
        Unknown,	// 112AA..112AF
        Khudawadi,	// 112B0..112EA
        Unknown,	// 112EB..112EF
        Khudawadi,	// 112F0..112F9
        Unknown,	// 112FA..112FF
        Grantha,	// 11300..11303
        Unknown,	// 11304
        Grantha,	// 11305..1130C
        Unknown,	// 1130D..1130E
        Grantha,	// 1130F..11310
        Unknown,	// 11311..11312
        Grantha,	// 11313..11328
        Unknown,	// 11329
        Grantha,	// 1132A..11330
        Unknown,	// 11331
        Grantha,	// 11332..11333
        Unknown,	// 11334
        Grantha,	// 11335..11339
        Unknown,	// 1133A
        Inherited,	// 1133B
        Grantha,	// 1133C..11344
        Unknown,	// 11345..11346
        Grantha,	// 11347..11348
        Unknown,	// 11349..1134A
        Grantha,	// 1134B..1134D
        Unknown,	// 1134E..1134F
        Grantha,	// 11350
        Unknown,	// 11351..11356
        Grantha,	// 11357
        Unknown,	// 11358..1135C
        Grantha,	// 1135D..11363
        Unknown,	// 11364..11365
        Grantha,	// 11366..1136C
        Unknown,	// 1136D..1136F
        Grantha,	// 11370..11374
        Unknown,	// 11375..113FF
        Newa,	// 11400..1145B
        Unknown,	// 1145C
        Newa,	// 1145D..11461
        Unknown,	// 11462..1147F
        Tirhuta,	// 11480..114C7
        Unknown,	// 114C8..114CF
        Tirhuta,	// 114D0..114D9
        Unknown,	// 114DA..1157F
        Siddham,	// 11580..115B5
        Unknown,	// 115B6..115B7
        Siddham,	// 115B8..115DD
        Unknown,	// 115DE..115FF
        Modi,	// 11600..11644
        Unknown,	// 11645..1164F
        Modi,	// 11650..11659
        Unknown,	// 1165A..1165F
        Mongolian,	// 11660..1166C
        Unknown,	// 1166D..1167F
        Takri,	// 11680..116B9
        Unknown,	// 116BA..116BF
        Takri,	// 116C0..116C9
        Unknown,	// 116CA..116FF
        Ahom,	// 11700..1171A
        Unknown,	// 1171B..1171C
        Ahom,	// 1171D..1172B
        Unknown,	// 1172C..1172F
        Ahom,	// 11730..11746
        Unknown,	// 11747..117FF
        Dogra,	// 11800..1183B
        Unknown,	// 1183C..1189F
        WarangCiti,	// 118A0..118F2
        Unknown,	// 118F3..118FE
        WarangCiti,	// 118FF
        DivesAkuru,	// 11900..11906
        Unknown,	// 11907..11908
        DivesAkuru,	// 11909
        Unknown,	// 1190A..1190B
        DivesAkuru,	// 1190C..11913
        Unknown,	// 11914
        DivesAkuru,	// 11915..11916
        Unknown,	// 11917
        DivesAkuru,	// 11918..11935
        Unknown,	// 11936
        DivesAkuru,	// 11937..11938
        Unknown,	// 11939..1193A
        DivesAkuru,	// 1193B..11946
        Unknown,	// 11947..1194F
        DivesAkuru,	// 11950..11959
        Unknown,	// 1195A..1199F
        Nandinagari,	// 119A0..119A7
        Unknown,	// 119A8..119A9
        Nandinagari,	// 119AA..119D7
        Unknown,	// 119D8..119D9
        Nandinagari,	// 119DA..119E4
        Unknown,	// 119E5..119FF
        ZanabazarSquare,	// 11A00..11A47
        Unknown,	// 11A48..11A4F
        Soyombo,	// 11A50..11AA2
        Unknown,	// 11AA3..11AAF
        CanadianAboriginal,	// 11AB0..11ABF
        PauCinHau,	// 11AC0..11AF8
        Unknown,	// 11AF9..11AFF
        Devanagari,	// 11B00..11B09
        Unknown,	// 11B0A..11BFF
        Bhaiksuki,	// 11C00..11C08
        Unknown,	// 11C09
        Bhaiksuki,	// 11C0A..11C36
        Unknown,	// 11C37
        Bhaiksuki,	// 11C38..11C45
        Unknown,	// 11C46..11C4F
        Bhaiksuki,	// 11C50..11C6C
        Unknown,	// 11C6D..11C6F
        Marchen,	// 11C70..11C8F
        Unknown,	// 11C90..11C91
        Marchen,	// 11C92..11CA7
        Unknown,	// 11CA8
        Marchen,	// 11CA9..11CB6
        Unknown,	// 11CB7..11CFF
        MasaramGondi,	// 11D00..11D06
        Unknown,	// 11D07
        MasaramGondi,	// 11D08..11D09
        Unknown,	// 11D0A
        MasaramGondi,	// 11D0B..11D36
        Unknown,	// 11D37..11D39
        MasaramGondi,	// 11D3A
        Unknown,	// 11D3B
        MasaramGondi,	// 11D3C..11D3D
        Unknown,	// 11D3E
        MasaramGondi,	// 11D3F..11D47
        Unknown,	// 11D48..11D4F
        MasaramGondi,	// 11D50..11D59
        Unknown,	// 11D5A..11D5F
        GunjalaGondi,	// 11D60..11D65
        Unknown,	// 11D66
        GunjalaGondi,	// 11D67..11D68
        Unknown,	// 11D69
        GunjalaGondi,	// 11D6A..11D8E
        Unknown,	// 11D8F
        GunjalaGondi,	// 11D90..11D91
        Unknown,	// 11D92
        GunjalaGondi,	// 11D93..11D98
        Unknown,	// 11D99..11D9F
        GunjalaGondi,	// 11DA0..11DA9
        Unknown,	// 11DAA..11EDF
        Makasar,	// 11EE0..11EF8
        Unknown,	// 11EF9..11EFF
        Kawi,	// 11F00..11F10
        Unknown,	// 11F11
        Kawi,	// 11F12..11F3A
        Unknown,	// 11F3B..11F3D
        Kawi,	// 11F3E..11F59
        Unknown,	// 11F5A..11FAF
        Lisu,	// 11FB0
        Unknown,	// 11FB1..11FBF
        Tamil,	// 11FC0..11FF1
        Unknown,	// 11FF2..11FFE
        Tamil,	// 11FFF
        Cuneiform,	// 12000..12399
        Unknown,	// 1239A..123FF
        Cuneiform,	// 12400..1246E
        Unknown,	// 1246F
        Cuneiform,	// 12470..12474
        Unknown,	// 12475..1247F
        Cuneiform,	// 12480..12543
        Unknown,	// 12544..12F8F
        CyproMinoan,	// 12F90..12FF2
        Unknown,	// 12FF3..12FFF
        EgyptianHieroglyphs,	// 13000..13455
        Unknown,	// 13456..143FF
        AnatolianHieroglyphs,	// 14400..14646
        Unknown,	// 14647..167FF
        Bamum,	// 16800..16A38
        Unknown,	// 16A39..16A3F
        Mro,	// 16A40..16A5E
        Unknown,	// 16A5F
        Mro,	// 16A60..16A69
        Unknown,	// 16A6A..16A6D
        Mro,	// 16A6E..16A6F
        Tangsa,	// 16A70..16ABE
        Unknown,	// 16ABF
        Tangsa,	// 16AC0..16AC9
        Unknown,	// 16ACA..16ACF
        BassaVah,	// 16AD0..16AED
        Unknown,	// 16AEE..16AEF
        BassaVah,	// 16AF0..16AF5
        Unknown,	// 16AF6..16AFF
        PahawhHmong,	// 16B00..16B45
        Unknown,	// 16B46..16B4F
        PahawhHmong,	// 16B50..16B59
        Unknown,	// 16B5A
        PahawhHmong,	// 16B5B..16B61
        Unknown,	// 16B62
        PahawhHmong,	// 16B63..16B77
        Unknown,	// 16B78..16B7C
        PahawhHmong,	// 16B7D..16B8F
        Unknown,	// 16B90..16E3F
        Medefaidrin,	// 16E40..16E9A
        Unknown,	// 16E9B..16EFF
        Miao,	// 16F00..16F4A
        Unknown,	// 16F4B..16F4E
        Miao,	// 16F4F..16F87
        Unknown,	// 16F88..16F8E
        Miao,	// 16F8F..16F9F
        Unknown,	// 16FA0..16FDF
        Tangut,	// 16FE0
        Nushu,	// 16FE1
        Han,	// 16FE2..16FE3
        KhitanSmallScript,	// 16FE4
        Unknown,	// 16FE5..16FEF
        Han,	// 16FF0..16FF1
        Unknown,	// 16FF2..16FFF
        Tangut,	// 17000..187F7
        Unknown,	// 187F8..187FF
        Tangut,	// 18800..18AFF
        KhitanSmallScript,	// 18B00..18CD5
        Unknown,	// 18CD6..18CFF
        Tangut,	// 18D00..18D08
        Unknown,	// 18D09..1AFEF
        Katakana,	// 1AFF0..1AFF3
        Unknown,	// 1AFF4
        Katakana,	// 1AFF5..1AFFB
        Unknown,	// 1AFFC
        Katakana,	// 1AFFD..1AFFE
        Unknown,	// 1AFFF
        Katakana,	// 1B000
        Hiragana,	// 1B001..1B11F
        Katakana,	// 1B120..1B122
        Unknown,	// 1B123..1B131
        Hiragana,	// 1B132
        Unknown,	// 1B133..1B14F
        Hiragana,	// 1B150..1B152
        Unknown,	// 1B153..1B154
        Katakana,	// 1B155
        Unknown,	// 1B156..1B163
        Katakana,	// 1B164..1B167
        Unknown,	// 1B168..1B16F
        Nushu,	// 1B170..1B2FB
        Unknown,	// 1B2FC..1BBFF
        Duployan,	// 1BC00..1BC6A
        Unknown,	// 1BC6B..1BC6F
        Duployan,	// 1BC70..1BC7C
        Unknown,	// 1BC7D..1BC7F
        Duployan,	// 1BC80..1BC88
        Unknown,	// 1BC89..1BC8F
        Duployan,	// 1BC90..1BC99
        Unknown,	// 1BC9A..1BC9B
        Duployan,	// 1BC9C..1BC9F
        Common,	// 1BCA0..1BCA3
        Unknown,	// 1BCA4..1CEFF
        Inherited,	// 1CF00..1CF2D
        Unknown,	// 1CF2E..1CF2F
        Inherited,	// 1CF30..1CF46
        Unknown,	// 1CF47..1CF4F
        Common,	// 1CF50..1CFC3
        Unknown,	// 1CFC4..1CFFF
        Common,	// 1D000..1D0F5
        Unknown,	// 1D0F6..1D0FF
        Common,	// 1D100..1D126
        Unknown,	// 1D127..1D128
        Common,	// 1D129..1D166
        Inherited,	// 1D167..1D169
        Common,	// 1D16A..1D17A
        Inherited,	// 1D17B..1D182
        Common,	// 1D183..1D184
        Inherited,	// 1D185..1D18B
        Common,	// 1D18C..1D1A9
        Inherited,	// 1D1AA..1D1AD
        Common,	// 1D1AE..1D1EA
        Unknown,	// 1D1EB..1D1FF
        Greek,	// 1D200..1D245
        Unknown,	// 1D246..1D2BF
        Common,	// 1D2C0..1D2D3
        Unknown,	// 1D2D4..1D2DF
        Common,	// 1D2E0..1D2F3
        Unknown,	// 1D2F4..1D2FF
        Common,	// 1D300..1D356
        Unknown,	// 1D357..1D35F
        Common,	// 1D360..1D378
        Unknown,	// 1D379..1D3FF
        Common,	// 1D400..1D454
        Unknown,	// 1D455
        Common,	// 1D456..1D49C
        Unknown,	// 1D49D
        Common,	// 1D49E..1D49F
        Unknown,	// 1D4A0..1D4A1
        Common,	// 1D4A2
        Unknown,	// 1D4A3..1D4A4
        Common,	// 1D4A5..1D4A6
        Unknown,	// 1D4A7..1D4A8
        Common,	// 1D4A9..1D4AC
        Unknown,	// 1D4AD
        Common,	// 1D4AE..1D4B9
        Unknown,	// 1D4BA
        Common,	// 1D4BB
        Unknown,	// 1D4BC
        Common,	// 1D4BD..1D4C3
        Unknown,	// 1D4C4
        Common,	// 1D4C5..1D505
        Unknown,	// 1D506
        Common,	// 1D507..1D50A
        Unknown,	// 1D50B..1D50C
        Common,	// 1D50D..1D514
        Unknown,	// 1D515
        Common,	// 1D516..1D51C
        Unknown,	// 1D51D
        Common,	// 1D51E..1D539
        Unknown,	// 1D53A
        Common,	// 1D53B..1D53E
        Unknown,	// 1D53F
        Common,	// 1D540..1D544
        Unknown,	// 1D545
        Common,	// 1D546
        Unknown,	// 1D547..1D549
        Common,	// 1D54A..1D550
        Unknown,	// 1D551
        Common,	// 1D552..1D6A5
        Unknown,	// 1D6A6..1D6A7
        Common,	// 1D6A8..1D7CB
        Unknown,	// 1D7CC..1D7CD
        Common,	// 1D7CE..1D7FF
        SignWriting,	// 1D800..1DA8B
        Unknown,	// 1DA8C..1DA9A
        SignWriting,	// 1DA9B..1DA9F
        Unknown,	// 1DAA0
        SignWriting,	// 1DAA1..1DAAF
        Unknown,	// 1DAB0..1DEFF
        Latin,	// 1DF00..1DF1E
        Unknown,	// 1DF1F..1DF24
        Latin,	// 1DF25..1DF2A
        Unknown,	// 1DF2B..1DFFF
        Glagolitic,	// 1E000..1E006
        Unknown,	// 1E007
        Glagolitic,	// 1E008..1E018
        Unknown,	// 1E019..1E01A
        Glagolitic,	// 1E01B..1E021
        Unknown,	// 1E022
        Glagolitic,	// 1E023..1E024
        Unknown,	// 1E025
        Glagolitic,	// 1E026..1E02A
        Unknown,	// 1E02B..1E02F
        Cyrillic,	// 1E030..1E06D
        Unknown,	// 1E06E..1E08E
        Cyrillic,	// 1E08F
        Unknown,	// 1E090..1E0FF
        NyiakengPuachueHmong,	// 1E100..1E12C
        Unknown,	// 1E12D..1E12F
        NyiakengPuachueHmong,	// 1E130..1E13D
        Unknown,	// 1E13E..1E13F
        NyiakengPuachueHmong,	// 1E140..1E149
        Unknown,	// 1E14A..1E14D
        NyiakengPuachueHmong,	// 1E14E..1E14F
        Unknown,	// 1E150..1E28F
        Toto,	// 1E290..1E2AE
        Unknown,	// 1E2AF..1E2BF
        Wancho,	// 1E2C0..1E2F9
        Unknown,	// 1E2FA..1E2FE
        Wancho,	// 1E2FF
        Unknown,	// 1E300..1E4CF
        NagMundari,	// 1E4D0..1E4F9
        Unknown,	// 1E4FA..1E7DF
        Ethiopic,	// 1E7E0..1E7E6
        Unknown,	// 1E7E7
        Ethiopic,	// 1E7E8..1E7EB
        Unknown,	// 1E7EC
        Ethiopic,	// 1E7ED..1E7EE
        Unknown,	// 1E7EF
        Ethiopic,	// 1E7F0..1E7FE
        Unknown,	// 1E7FF
        MendeKikakui,	// 1E800..1E8C4
        Unknown,	// 1E8C5..1E8C6
        MendeKikakui,	// 1E8C7..1E8D6
        Unknown,	// 1E8D7..1E8FF
        Adlam,	// 1E900..1E94B
        Unknown,	// 1E94C..1E94F
        Adlam,	// 1E950..1E959
        Unknown,	// 1E95A..1E95D
        Adlam,	// 1E95E..1E95F
        Unknown,	// 1E960..1EC70
        Common,	// 1EC71..1ECB4
        Unknown,	// 1ECB5..1ED00
        Common,	// 1ED01..1ED3D
        Unknown,	// 1ED3E..1EDFF
        Arabic,	// 1EE00..1EE03
        Unknown,	// 1EE04
        Arabic,	// 1EE05..1EE1F
        Unknown,	// 1EE20
        Arabic,	// 1EE21..1EE22
        Unknown,	// 1EE23
        Arabic,	// 1EE24
        Unknown,	// 1EE25..1EE26
        Arabic,	// 1EE27
        Unknown,	// 1EE28
        Arabic,	// 1EE29..1EE32
        Unknown,	// 1EE33
        Arabic,	// 1EE34..1EE37
        Unknown,	// 1EE38
        Arabic,	// 1EE39
        Unknown,	// 1EE3A
        Arabic,	// 1EE3B
        Unknown,	// 1EE3C..1EE41
        Arabic,	// 1EE42
        Unknown,	// 1EE43..1EE46
        Arabic,	// 1EE47
        Unknown,	// 1EE48
        Arabic,	// 1EE49
        Unknown,	// 1EE4A
        Arabic,	// 1EE4B
        Unknown,	// 1EE4C
        Arabic,	// 1EE4D..1EE4F
        Unknown,	// 1EE50
        Arabic,	// 1EE51..1EE52
        Unknown,	// 1EE53
        Arabic,	// 1EE54
        Unknown,	// 1EE55..1EE56
        Arabic,	// 1EE57
        Unknown,	// 1EE58
        Arabic,	// 1EE59
        Unknown,	// 1EE5A
        Arabic,	// 1EE5B
        Unknown,	// 1EE5C
        Arabic,	// 1EE5D
        Unknown,	// 1EE5E
        Arabic,	// 1EE5F
        Unknown,	// 1EE60
        Arabic,	// 1EE61..1EE62
        Unknown,	// 1EE63
        Arabic,	// 1EE64
        Unknown,	// 1EE65..1EE66
        Arabic,	// 1EE67..1EE6A
        Unknown,	// 1EE6B
        Arabic,	// 1EE6C..1EE72
        Unknown,	// 1EE73
        Arabic,	// 1EE74..1EE77
        Unknown,	// 1EE78
        Arabic,	// 1EE79..1EE7C
        Unknown,	// 1EE7D
        Arabic,	// 1EE7E
        Unknown,	// 1EE7F
        Arabic,	// 1EE80..1EE89
        Unknown,	// 1EE8A
        Arabic,	// 1EE8B..1EE9B
        Unknown,	// 1EE9C..1EEA0
        Arabic,	// 1EEA1..1EEA3
        Unknown,	// 1EEA4
        Arabic,	// 1EEA5..1EEA9
        Unknown,	// 1EEAA
        Arabic,	// 1EEAB..1EEBB
        Unknown,	// 1EEBC..1EEEF
        Arabic,	// 1EEF0..1EEF1
        Unknown,	// 1EEF2..1EFFF
        Common,	// 1F000..1F02B
        Unknown,	// 1F02C..1F02F
        Common,	// 1F030..1F093
        Unknown,	// 1F094..1F09F
        Common,	// 1F0A0..1F0AE
        Unknown,	// 1F0AF..1F0B0
        Common,	// 1F0B1..1F0BF
        Unknown,	// 1F0C0
        Common,	// 1F0C1..1F0CF
        Unknown,	// 1F0D0
        Common,	// 1F0D1..1F0F5
        Unknown,	// 1F0F6..1F0FF
        Common,	// 1F100..1F1AD
        Unknown,	// 1F1AE..1F1E5
        Common,	// 1F1E6..1F1FF
        Hiragana,	// 1F200
        Common,	// 1F201..1F202
        Unknown,	// 1F203..1F20F
        Common,	// 1F210..1F23B
        Unknown,	// 1F23C..1F23F
        Common,	// 1F240..1F248
        Unknown,	// 1F249..1F24F
        Common,	// 1F250..1F251
        Unknown,	// 1F252..1F25F
        Common,	// 1F260..1F265
        Unknown,	// 1F266..1F2FF
        Common,	// 1F300..1F6D7
        Unknown,	// 1F6D8..1F6DB
        Common,	// 1F6DC..1F6EC
        Unknown,	// 1F6ED..1F6EF
        Common,	// 1F6F0..1F6FC
        Unknown,	// 1F6FD..1F6FF
        Common,	// 1F700..1F776
        Unknown,	// 1F777..1F77A
        Common,	// 1F77B..1F7D9
        Unknown,	// 1F7DA..1F7DF
        Common,	// 1F7E0..1F7EB
        Unknown,	// 1F7EC..1F7EF
        Common,	// 1F7F0
        Unknown,	// 1F7F1..1F7FF
        Common,	// 1F800..1F80B
        Unknown,	// 1F80C..1F80F
        Common,	// 1F810..1F847
        Unknown,	// 1F848..1F84F
        Common,	// 1F850..1F859
        Unknown,	// 1F85A..1F85F
        Common,	// 1F860..1F887
        Unknown,	// 1F888..1F88F
        Common,	// 1F890..1F8AD
        Unknown,	// 1F8AE..1F8AF
        Common,	// 1F8B0..1F8B1
        Unknown,	// 1F8B2..1F8FF
        Common,	// 1F900..1FA53
        Unknown,	// 1FA54..1FA5F
        Common,	// 1FA60..1FA6D
        Unknown,	// 1FA6E..1FA6F
        Common,	// 1FA70..1FA7C
        Unknown,	// 1FA7D..1FA7F
        Common,	// 1FA80..1FA88
        Unknown,	// 1FA89..1FA8F
        Common,	// 1FA90..1FABD
        Unknown,	// 1FABE
        Common,	// 1FABF..1FAC5
        Unknown,	// 1FAC6..1FACD
        Common,	// 1FACE..1FADB
        Unknown,	// 1FADC..1FADF
        Common,	// 1FAE0..1FAE8
        Unknown,	// 1FAE9..1FAEF
        Common,	// 1FAF0..1FAF8
        Unknown,	// 1FAF9..1FAFF
        Common,	// 1FB00..1FB92
        Unknown,	// 1FB93
        Common,	// 1FB94..1FBCA
        Unknown,	// 1FBCB..1FBEF
        Common,	// 1FBF0..1FBF9
        Unknown,	// 1FBFA..1FFFF
        Han,	// 20000..2A6DF
        Unknown,	// 2A6E0..2A6FF
        Han,	// 2A700..2B739
        Unknown,	// 2B73A..2B73F
        Han,	// 2B740..2B81D
        Unknown,	// 2B81E..2B81F
        Han,	// 2B820..2CEA1
        Unknown,	// 2CEA2..2CEAF
        Han,	// 2CEB0..2EBE0
        Unknown,	// 2EBE1..2EBEF
        Han,	// 2EBF0..2EE5D
        Unknown,	// 2EE5E..2F7FF
        Han,	// 2F800..2FA1D
        Unknown,	// 2FA1E..2FFFF
        Han,	// 30000..3134A
        Unknown,	// 3134B..3134F
        Han,	// 31350..323AF
        Unknown,	// 323B0..E0000
        Common,	// E0001
        Unknown,	// E0002..E001F
        Common,	// E0020..E007F
        Unknown,	// E0080..E00FF
        Inherited,	// E0100..E01EF
        Unknown,	// E01F0..10FFFF
    };

    ///<summary>Gets the Unicode Script for the given character</summary>
    public static UnicodeScript GetScript(this char ch)
    {
		if (!IsValidCodePoint(ch))
			throw new ArgumentException($"Not a valid Unicode code point '{ch}'");

		var category = char.GetUnicodeCategory(ch);
        if (category == UnicodeCategory.OtherNotAssigned)
            return UnicodeScript.Unknown;

        var index = Array.BinarySearch(ScriptStarts, (int)ch);
        if (index < 0)
            index = -index - 2;

        return Scripts[index];
    }

    private static bool IsValidCodePoint(int codePoint)
    {
        int plane = codePoint >>> 16;
        return plane < ((0x10FFFF + 1) >>> 16);
    }
}

