using FluentAssertions;
using Lingua.Api;
using Lingua.Internal;
using Xunit;
using static Lingua.Api.Language;

namespace Lingua.Tests;

public class LanguageDetectorTests : IDisposable
{
    private static readonly Dictionary<string, float> UnigramLanguageModelForEnglish = new()
    {
        ["a"] = 0.01F,
        ["l"] = 0.02F,
        ["t"] = 0.03F,
        ["e"] = 0.04F,
        ["r"] = 0.05F,
        // unknown unigram in model
        ["w"] = 0F
    };

    private static readonly Dictionary<string, float> BigramLanguageModelForEnglish = new()
    {
        ["al"] = 0.11F,
        ["lt"] = 0.12F,
        ["te"] = 0.13F,
        ["er"] = 0.14F,
        // unknown bigrams in model
        ["aq"] = 0F,
        ["wx"] = 0F
    };

    private static readonly Dictionary<string, float> TrigramLanguageModelForEnglish = new()
    {
        ["alt"] = 0.19F,
        ["lte"] = 0.2F,
        ["ter"] = 0.21F,
        // unknown trigrams in model
        ["aqu"] = 0F,
        ["tez"] = 0F,
        ["wxy"] = 0F
    };

    private static readonly Dictionary<string, float> QuadrigramLanguageModelForEnglish = new()
    {
        ["alte"] = 0.25F,
        ["lter"] = 0.26F,
        // unknown quadrigrams in model
        ["aqua"] = 0F,
        ["wxyz"] = 0F
    };

    private static readonly Dictionary<string, float> FivegramLanguageModelForEnglish = new()
    {
        ["alter"] = 0.29F,
        // unknown fivegrams in model
        ["aquas"] = 0F
    };

    private static readonly Dictionary<string, float> UnigramLanguageModelForGerman = new()
    {
        ["a"] = 0.06F,
        ["l"] = 0.07F,
        ["t"] = 0.08F,
        ["e"] = 0.09F,
        ["r"] = 0.1F,
        // unknown unigrams in model
        ["w"] = 0F
    };

    private static readonly Dictionary<string, float> BigramLanguageModelForGerman = new()
    {
        ["al"] = 0.15F,
        ["lt"] = 0.16F,
        ["te"] = 0.17F,
        ["er"] = 0.18F,
        // unknown bigrams in model
        ["wx"] = 0F
    };

    private static readonly Dictionary<string, float> TrigramLanguageModelForGerman = new()
    {
        ["alt"] = 0.22F,
        ["lte"] = 0.23F,
        ["ter"] = 0.24F,
        // unknown trigrams in model
        ["wxy"] = 0F
    };

    private static readonly Dictionary<string, float> QuadrigramLanguageModelForGerman = new()
    {
        ["alte"] = 0.27F,
        ["lter"] = 0.28F,
        // unknown quadrigrams in model
        ["wxyz"] = 0F
    };

    private static readonly Dictionary<string, float> FivegramLanguageModelForGerman = new()
    {
        ["alter"] = 0.3F
    };

    private readonly LanguageDetector _detectorForEnglishAndGerman = new(
        [ENGLISH, GERMAN],
        0,
        false,
        false);

    private readonly LanguageDetector _detectorForAllLanguages = new(
        LanguageExtensions.All().ToHashSet(),
        0,
        false,
        false);

    private static readonly TestDataLanguageModel UnigramTestDataLanguageModel = new(new()
    {
        new("a"),
        new("l"),
        new("t"),
        new("e"),
        new("r")
    });

    private static readonly TestDataLanguageModel TrigramTestDataLanguageModel = new(new()
    {
        new("alt"),
        new("lte"),
        new("ter"),
        new("wxy")
    });

    private static readonly TestDataLanguageModel QuadrigramTestDataLanguageModel = new(new()
    {
        new("alte"),
        new("lter"),
        new("wxyz")
    });

    public LanguageDetectorTests() => AddLanguageModelsToDetector();

    public void Dispose() => RemoveLanguageModelsFromDetector();

    [Fact]
    public void TextIsCleanedUpProperly() =>
	    _detectorForAllLanguages.CleanUpInputText(
		    """
		    Weltweit    gibt es ungefähr 6.000 Sprachen,
		    wobei laut Schätzungen zufolge ungefähr 90  Prozent davon
		    am Ende dieses Jahrhunderts verdrängt sein werden.
		    """).Should().Be(
		    "weltweit gibt es ungefähr sprachen wobei laut schätzungen zufolge ungefähr " +
		    "prozent davon am ende dieses jahrhunderts verdrängt sein werden"
	    );

    [Fact]
    public void TextIsSplitIntoWordsCorrectly()
    {
        _detectorForAllLanguages.SplitTextIntoWords("this is a sentence")
            .Should()
            .BeEquivalentTo(new List<string>
            {
                "this", "is", "a", "sentence"
            });

        _detectorForAllLanguages.SplitTextIntoWords("sentence")
            .Should()
            .BeEquivalentTo(new List<string>
            {
                "sentence"
            });

        _detectorForAllLanguages.SplitTextIntoWords("上海大学是一个好大学 this is a sentence")
            .Should()
            .BeEquivalentTo(new List<string>
            {
                "上", "海", "大", "学", "是", "一", "个", "好", "大", "学", "this", "is", "a", "sentence"
            });
    }

    [Theory]
    [InlineData(ENGLISH, "a", 0.01)]
    [InlineData(ENGLISH, "lt", 0.12)]
    [InlineData(ENGLISH, "ter", 0.21)]
    [InlineData(ENGLISH, "alte", 0.25)]
    [InlineData(ENGLISH, "alter", 0.29)]
    [InlineData(GERMAN, "t", 0.08)]
    [InlineData(GERMAN, "er", 0.18)]
    [InlineData(GERMAN, "alt", 0.22)]
    [InlineData(GERMAN, "lter", 0.28)]
    [InlineData(GERMAN, "alter", 0.30)]
    public void NgramProbabilityLookupWorksCorrectly(Language language, string ngram, float expectedProbability) =>
        _detectorForEnglishAndGerman.LookupNgramProbability(language, new Ngram(ngram)).Should()
            .Be(expectedProbability);

    [Fact]
    public void NgramProbabilityLookupThrowsForZerogram()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
                _detectorForEnglishAndGerman.LookupNgramProbability(ENGLISH, new Ngram("")));

        exception.Message.Should().Be("Zerogram detected");
    }

    public static IEnumerable<object[]> NgramProbabilityProvider()
    {
        yield return
        [
            new HashSet<Ngram> { new("a"), new("l"), new("t"), new("e"), new("r") },
            Math.Log(0.01F) + Math.Log(0.02F) + Math.Log(0.03F) + Math.Log(0.04F) + Math.Log(0.05F)
        ];

        yield return
        [
            new HashSet<Ngram> { new("alt"), new("lte"), new("tez") },
            Math.Log(0.19F) + Math.Log(0.2F) + Math.Log(0.13F)
        ];

        yield return
        [
            new HashSet<Ngram> { new("aquas") },
            Math.Log(0.01F)
        ];
    }

    [Theory]
    [MemberData(nameof(NgramProbabilityProvider))]
    public void SumOfNgramProbabilitiesComputedCorrectly(HashSet<Ngram> ngrams, float expectedSumOfProbabilities) =>
        _detectorForEnglishAndGerman.ComputeSumOfNgramProbabilities(ENGLISH, ngrams)
            .Should()
            .Be(expectedSumOfProbabilities);

    public static IEnumerable<object[]> LanguageProbabilitiesProvider()
    {
        yield return
        [
            UnigramTestDataLanguageModel,
            new Dictionary<Language, float>
            {
                [ENGLISH] = (float)(Math.Log(0.01F) + Math.Log(0.02F) + Math.Log(0.03F) + Math.Log(0.04F) + Math.Log(0.05F)),
                [GERMAN] = (float)(Math.Log(0.06F) + Math.Log(0.07F) + Math.Log(0.08F) + Math.Log(0.09F) + Math.Log(0.1F)),
            }
        ];

        yield return
        [
            TrigramTestDataLanguageModel,
            new Dictionary<Language, float>
            {
                [ENGLISH] = (float)(Math.Log(0.19F) + Math.Log(0.2F) + Math.Log(0.21F)),
                [GERMAN] = (float)(Math.Log(0.22F) + Math.Log(0.23F) + Math.Log(0.24F)),
            }
        ];

        yield return
        [
            QuadrigramTestDataLanguageModel,
            new Dictionary<Language, float>
            {
                [ENGLISH] = (float)(Math.Log(0.25F) + Math.Log(0.26F)),
                [GERMAN] = (float)(Math.Log(0.27F) + Math.Log(0.28F)),
            }
        ];
    }

    [Theory]
    [MemberData(nameof(LanguageProbabilitiesProvider))]
    public void LanguageProbabilitiesComputedCorrectly(TestDataLanguageModel model,
        Dictionary<Language, float> expectedProbabilities) =>
        _detectorForEnglishAndGerman.ComputeLanguageProbabilities(model, _detectorForEnglishAndGerman.Languages)
            .Should().BeEquivalentTo(expectedProbabilities);

    [Theory]
    [InlineData("məhərrəm", AZERBAIJANI)]
    [InlineData("substituïts", CATALAN)]
    [InlineData("rozdělit", CZECH)]
    [InlineData("tvořen", CZECH)]
    [InlineData("subjektů", CZECH)]
    [InlineData("nesufiĉecon", ESPERANTO)]
    [InlineData("intermiksiĝis", ESPERANTO)]
    [InlineData("monaĥinoj", ESPERANTO)]
    [InlineData("kreitaĵoj", ESPERANTO)]
    [InlineData("ŝpinante", ESPERANTO)]
    [InlineData("apenaŭ", ESPERANTO)]
    [InlineData("groß", GERMAN)]
    [InlineData("σχέδια", GREEK)]
    [InlineData("fekvő", HUNGARIAN)]
    [InlineData("meggyűrűzni", HUNGARIAN)]
    [InlineData("ヴェダイヤモンド", JAPANESE)]
    [InlineData("әлем", KAZAKH)]
    [InlineData("шаруашылығы", KAZAKH)]
    [InlineData("ақын", KAZAKH)]
    [InlineData("оның", KAZAKH)]
    [InlineData("шұрайлы", KAZAKH)]
    [InlineData("teoloģiska", LATVIAN)]
    [InlineData("blaķene", LATVIAN)]
    [InlineData("ceļojumiem", LATVIAN)]
    [InlineData("numuriņu", LATVIAN)]
    [InlineData("mergelės", LITHUANIAN)]
    [InlineData("įrengus", LITHUANIAN)]
    [InlineData("slegiamų", LITHUANIAN)]
    [InlineData("припаѓа", MACEDONIAN)]
    [InlineData("ѕидови", MACEDONIAN)]
    [InlineData("ќерка", MACEDONIAN)]
    [InlineData("џамиите", MACEDONIAN)]
    [InlineData("मिळते", MARATHI)]
    [InlineData("үндсэн", MONGOLIAN)]
    [InlineData("дөхөж", MONGOLIAN)]
    [InlineData("zmieniły", POLISH)]
    [InlineData("państwowych", POLISH)]
    [InlineData("mniejszości", POLISH)]
    [InlineData("groźne", POLISH)]
    [InlineData("ialomiţa", ROMANIAN)]
    [InlineData("наслеђивања", SERBIAN)]
    [InlineData("неисквареношћу", SERBIAN)]
    [InlineData("podĺa", SLOVAK)]
    [InlineData("pohľade", SLOVAK)]
    [InlineData("mŕtvych", SLOVAK)]
    [InlineData("ґрунтовому", UKRAINIAN)]
    [InlineData("пропонує", UKRAINIAN)]
    [InlineData("пристрої", UKRAINIAN)]
    [InlineData("cằm", VIETNAMESE)]
    [InlineData("thần", VIETNAMESE)]
    [InlineData("chẳng", VIETNAMESE)]
    [InlineData("quẩy", VIETNAMESE)]
    [InlineData("sẵn", VIETNAMESE)]
    [InlineData("nhẫn", VIETNAMESE)]
    [InlineData("dắt", VIETNAMESE)]
    [InlineData("chất", VIETNAMESE)]
    [InlineData("đạp", VIETNAMESE)]
    [InlineData("mặn", VIETNAMESE)]
    [InlineData("hậu", VIETNAMESE)]
    [InlineData("hiền", VIETNAMESE)]
    [InlineData("lẻn", VIETNAMESE)]
    [InlineData("biểu", VIETNAMESE)]
    [InlineData("kẽm", VIETNAMESE)]
    [InlineData("diễm", VIETNAMESE)]
    [InlineData("phế", VIETNAMESE)]
    [InlineData("việc", VIETNAMESE)]
    [InlineData("chỉnh", VIETNAMESE)]
    [InlineData("trĩ", VIETNAMESE)]
    [InlineData("ravị", VIETNAMESE)]
    [InlineData("thơ", VIETNAMESE)]
    [InlineData("nguồn", VIETNAMESE)]
    [InlineData("thờ", VIETNAMESE)]
    [InlineData("sỏi", VIETNAMESE)]
    [InlineData("tổng", VIETNAMESE)]
    [InlineData("nhở", VIETNAMESE)]
    [InlineData("mỗi", VIETNAMESE)]
    [InlineData("bỡi", VIETNAMESE)]
    [InlineData("tốt", VIETNAMESE)]
    [InlineData("giới", VIETNAMESE)]
    [InlineData("một", VIETNAMESE)]
    [InlineData("hợp", VIETNAMESE)]
    [InlineData("hưng", VIETNAMESE)]
    [InlineData("từng", VIETNAMESE)]
    [InlineData("của", VIETNAMESE)]
    [InlineData("sử", VIETNAMESE)]
    [InlineData("cũng", VIETNAMESE)]
    [InlineData("những", VIETNAMESE)]
    [InlineData("chức", VIETNAMESE)]
    [InlineData("dụng", VIETNAMESE)]
    [InlineData("thực", VIETNAMESE)]
    [InlineData("kỳ", VIETNAMESE)]
    [InlineData("kỷ", VIETNAMESE)]
    [InlineData("mỹ", VIETNAMESE)]
    [InlineData("mỵ", VIETNAMESE)]
    [InlineData("aṣiwèrè", YORUBA)]
    [InlineData("ṣaaju", YORUBA)]
    [InlineData("والموضوع", UNKNOWN)]
    [InlineData("сопротивление", UNKNOWN)]
    [InlineData("house", UNKNOWN)]
    public void LanguageOfSingleWordWithUniqueCharactersCanBeUnambiguouslyIdentifiedWithRules(
        string word,
        Language expectedLanguage) =>
        _detectorForAllLanguages.DetectLanguageWithRules([word]).Should().Be(expectedLanguage);

    [Theory]
    [InlineData("ունենա", ARMENIAN)]
    [InlineData("জানাতে", BENGALI)]
    [InlineData("გარეუბან", GEORGIAN)]
    [InlineData("σταμάτησε", GREEK)]
    [InlineData("ઉપકરણોની", GUJARATI)]
    [InlineData("בתחרויות", HEBREW)]
    [InlineData("びさ", JAPANESE)]
    [InlineData("대결구도가", KOREAN)]
    [InlineData("ਮੋਟਰਸਾਈਕਲਾਂ", PUNJABI)]
    [InlineData("துன்பங்களை", TAMIL)]
    [InlineData("కృష్ణదేవరాయలు", TELUGU)]
    [InlineData("ในทางหลวงหมายเลข", THAI)]
    public void LanguageOfSingleWordWithUniqueAlphabetCanBeUnambiguouslyIdentifiedWithRules(
        string word,
        Language expectedLanguage) =>
        _detectorForAllLanguages.DetectLanguageWithRules([word]).Should().Be(expectedLanguage);

    public static IEnumerable<object[]> FilteredLanguagesProvider()
    {
        yield return
        [
            "والموضوع",
            new HashSet<Language> { ARABIC, PERSIAN, URDU }
        ];
        yield return
        [
            "сопротивление",
            new HashSet<Language> { BELARUSIAN, BULGARIAN, KAZAKH, MACEDONIAN, MONGOLIAN, RUSSIAN, SERBIAN, UKRAINIAN }
        ];
        yield return
        [
            "раскрывае",
            new HashSet<Language> { BELARUSIAN, KAZAKH, MONGOLIAN, RUSSIAN }
        ];
        yield return
        [
            "этот",
            new HashSet<Language> { BELARUSIAN, KAZAKH, MONGOLIAN, RUSSIAN }
        ];
        yield return
        [
            "огнём",
            new HashSet<Language> { BELARUSIAN, KAZAKH, MONGOLIAN, RUSSIAN }
        ];
        yield return
        [
            "плаваща",
            new HashSet<Language> { BULGARIAN, KAZAKH, MONGOLIAN, RUSSIAN }
        ];
        yield return
        [
            "довършат",
            new HashSet<Language> { BULGARIAN, KAZAKH, MONGOLIAN, RUSSIAN }
        ];
        yield return
        [
            "павінен",
            new HashSet<Language> { BELARUSIAN, KAZAKH, UKRAINIAN }
        ];
        yield return
        [
            "затоплување",
            new HashSet<Language> { MACEDONIAN, SERBIAN }
        ];
        yield return
        [
            "ректасцензија",
            new HashSet<Language> { MACEDONIAN, SERBIAN }
        ];
        yield return
        [
            "набљудувач",
            new HashSet<Language> { MACEDONIAN, SERBIAN }
        ];
        yield return
        [
            "aizklātā",
            new HashSet<Language> { LATVIAN, MAORI, YORUBA }
        ];
        yield return
        [
            "sistēmas",
            new HashSet<Language> { LATVIAN, MAORI, YORUBA }
        ];
        yield return
        [
            "palīdzi",
            new HashSet<Language> { LATVIAN, MAORI, YORUBA }
        ];
        yield return
        [
            "nhẹn",
            new HashSet<Language> { VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "chọn",
            new HashSet<Language> { VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "prihvaćanju",
            new HashSet<Language> { BOSNIAN, CROATIAN, POLISH }
        ];
        yield return
        [
            "nađete",
            new HashSet<Language> { BOSNIAN, CROATIAN, VIETNAMESE }
        ];
        yield return
        [
            "visão",
            new HashSet<Language> { PORTUGUESE, VIETNAMESE }
        ];
        yield return
        [
            "wystąpią",
            new HashSet<Language> { LITHUANIAN, POLISH }
        ];
        yield return
        [
            "budowę",
            new HashSet<Language> { LITHUANIAN, POLISH }
        ];
        yield return
        [
            "nebūsime",
            new HashSet<Language> { LATVIAN, LITHUANIAN, MAORI, YORUBA }
        ];
        yield return
        [
            "afişate",
            new HashSet<Language> { AZERBAIJANI, ROMANIAN, TURKISH }
        ];
        yield return
        [
            "kradzieżami",
            new HashSet<Language> { POLISH, ROMANIAN }
        ];
        yield return
        [
            "înviat",
            new HashSet<Language> { FRENCH, ROMANIAN }
        ];
        yield return
        [
            "venerdì",
            new HashSet<Language> { ITALIAN, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "años",
            new HashSet<Language> { BASQUE, SPANISH }
        ];
        yield return
        [
            "rozohňuje",
            new HashSet<Language> { CZECH, SLOVAK }
        ];
        yield return
        [
            "rtuť",
            new HashSet<Language> { CZECH, SLOVAK }
        ];
        yield return
        [
            "pregătire",
            new HashSet<Language> { ROMANIAN, VIETNAMESE }
        ];
        yield return
        [
            "jeďte",
            new HashSet<Language> { CZECH, ROMANIAN, SLOVAK }
        ];
        yield return
        [
            "minjaverðir",
            new HashSet<Language> { ICELANDIC, TURKISH }
        ];
        yield return
        [
            "þagnarskyldu",
            new HashSet<Language> { ICELANDIC, TURKISH }
        ];
        yield return
        [
            "nebûtu",
            new HashSet<Language> { FRENCH, HUNGARIAN }
        ];
        yield return
        [
            "hashemidëve",
            new HashSet<Language> { AFRIKAANS, ALBANIAN, DUTCH, FRENCH }
        ];
        yield return
        [
            "forêt",
            new HashSet<Language> { AFRIKAANS, FRENCH, PORTUGUESE, VIETNAMESE }
        ];
        yield return
        [
            "succèdent",
            new HashSet<Language> { FRENCH, ITALIAN, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "où",
            new HashSet<Language> { FRENCH, ITALIAN, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "tõeliseks",
            new HashSet<Language> { ESTONIAN, HUNGARIAN, PORTUGUESE, VIETNAMESE }
        ];
        yield return
        [
            "viòiem",
            new HashSet<Language> { CATALAN, ITALIAN, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "contrôle",
            new HashSet<Language> { FRENCH, PORTUGUESE, SLOVAK, VIETNAMESE }
        ];
        yield return
        [
            "direktør",
            new HashSet<Language> { BOKMAL, DANISH, NYNORSK }
        ];
        yield return
        [
            "vývoj",
            new HashSet<Language> { CZECH, ICELANDIC, SLOVAK, TURKISH, VIETNAMESE }
        ];
        yield return
        [
            "päralt",
            new HashSet<Language> { ESTONIAN, FINNISH, GERMAN, SLOVAK, SWEDISH }
        ];
        yield return
        [
            "labâk",
            new HashSet<Language> { FRENCH, PORTUGUESE, ROMANIAN, TURKISH, VIETNAMESE }
        ];
        yield return
        [
            "pràctiques",
            new HashSet<Language> { CATALAN, FRENCH, ITALIAN, PORTUGUESE, VIETNAMESE }
        ];
        yield return
        [
            "überrascht",
            new HashSet<Language> { AZERBAIJANI, CATALAN, ESTONIAN, GERMAN, HUNGARIAN, SPANISH, TURKISH }
        ];
        yield return
        [
            "indebærer",
            new HashSet<Language> { BOKMAL, DANISH, ICELANDIC, NYNORSK }
        ];
        yield return
        [
            "måned",
            new HashSet<Language> { BOKMAL, DANISH, NYNORSK, SWEDISH }
        ];
        yield return
        [
            "zaručen",
            new HashSet<Language> { BOSNIAN, CZECH, CROATIAN, LATVIAN, LITHUANIAN, SLOVAK, SLOVENE }
        ];
        yield return
        [
            "zkouškou",
            new HashSet<Language> { BOSNIAN, CZECH, CROATIAN, LATVIAN, LITHUANIAN, SLOVAK, SLOVENE }
        ];
        yield return
        [
            "navržen",
            new HashSet<Language> { BOSNIAN, CZECH, CROATIAN, LATVIAN, LITHUANIAN, SLOVAK, SLOVENE }
        ];
        yield return
        [
            "façonnage",
            new HashSet<Language> { ALBANIAN, AZERBAIJANI, BASQUE, CATALAN, FRENCH, PORTUGUESE, TURKISH }
        ];
        yield return
        [
            "höher",
            new HashSet<Language> { AZERBAIJANI, ESTONIAN, FINNISH, GERMAN, HUNGARIAN, ICELANDIC, SWEDISH, TURKISH }
        ];
        yield return
        [
            "catedráticos",
            new HashSet<Language>
                { CATALAN, CZECH, ICELANDIC, IRISH, HUNGARIAN, PORTUGUESE, SLOVAK, SPANISH, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "política",
            new HashSet<Language>
                { CATALAN, CZECH, ICELANDIC, IRISH, HUNGARIAN, PORTUGUESE, SLOVAK, SPANISH, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "música",
            new HashSet<Language>
                { CATALAN, CZECH, ICELANDIC, IRISH, HUNGARIAN, PORTUGUESE, SLOVAK, SPANISH, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "contradicció",
            new HashSet<Language>
                { CATALAN, HUNGARIAN, ICELANDIC, IRISH, POLISH, PORTUGUESE, SLOVAK, SPANISH, VIETNAMESE, YORUBA }
        ];
        yield return
        [
            "només",
            new HashSet<Language>
            {
                CATALAN, CZECH, FRENCH, HUNGARIAN, ICELANDIC, IRISH, ITALIAN, PORTUGUESE, SLOVAK, SPANISH,
                VIETNAMESE, YORUBA
            }
        ];
        yield return
        [
            "house",
            new HashSet<Language>
            {
                AFRIKAANS, ALBANIAN, AZERBAIJANI, BASQUE, BOKMAL, BOSNIAN, CATALAN, CROATIAN, CZECH, DANISH,
                DUTCH, ENGLISH, ESPERANTO, ESTONIAN, FINNISH, FRENCH, GANDA, GERMAN, HUNGARIAN, ICELANDIC,
                INDONESIAN, IRISH, ITALIAN, LATIN, LATVIAN, LITHUANIAN, MALAY, MAORI, NYNORSK, OROMO, POLISH,
                PORTUGUESE, ROMANIAN, SHONA, SLOVAK, SLOVENE, SOMALI, SOTHO, SPANISH, SWAHILI, SWEDISH,
                TAGALOG, TSONGA, TSWANA, TURKISH, VIETNAMESE, WELSH, XHOSA, YORUBA, ZULU
            }
        ];
    }

    [Theory]
    [MemberData(nameof(FilteredLanguagesProvider))]
    public void LanguagesCanBeCorrectlyFilteredByRules(string word, HashSet<Language> expectedLanguages) =>
        _detectorForAllLanguages.FilterLanguagesByRules([word]).Should().BeEquivalentTo(expectedLanguages);

    [Theory]
    [InlineData("")]
    [InlineData(" \n  \t;")]
    [InlineData("3<856%)§")]
    public void StringsWithoutLettersReturnUnknownLanguages(string invalidString) =>
        _detectorForAllLanguages.DetectLanguageOf(invalidString).Should().Be(UNKNOWN);

    [Fact]
    public void LanguageOfGermanNounAlterCanBeDetectedCorrectly() =>
        _detectorForEnglishAndGerman.DetectLanguageOf("Alter").Should().Be(GERMAN);

    [Fact]
    public void LanguageConfidenceValuesComputedCorrectly()
    {
        var unigramCountForBothLanguages = 5;

        var totalProbabilityForGerman = (
            // Unigrams
            Math.Log(0.06F) + Math.Log(0.07F) + Math.Log(0.08F) + Math.Log(0.09F) + Math.Log(0.1F) +
            // Bigrams
            Math.Log(0.15F) + Math.Log(0.16F) + Math.Log(0.17F) + Math.Log(0.18F) +
            // Trigrams
            Math.Log(0.22F) + Math.Log(0.23F) + Math.Log(0.24F) +
            // Quadrigrams
            Math.Log(0.27F) + Math.Log(0.28F) +
            // Fivegrams
            Math.Log(0.3F)
        ) / unigramCountForBothLanguages;

        var totalProbabilityForEnglish = (
            // Unigrams
            Math.Log(0.01F) + Math.Log(0.02F) + Math.Log(0.03F) + Math.Log(0.04F) + Math.Log(0.05F) +
            // Bigrams
            Math.Log(0.11F) + Math.Log(0.12F) + Math.Log(0.13F) + Math.Log(0.14F) +
            // Trigrams
            Math.Log(0.19F) + Math.Log(0.2F) + Math.Log(0.21F) +
            // Quadrigrams
            Math.Log(0.25F) + Math.Log(0.26F) +
            // Fivegrams
            Math.Log(0.29F)
        ) / unigramCountForBothLanguages;

        var confidenceValues = _detectorForEnglishAndGerman.ComputeLanguageConfidenceValues("Alter");

        confidenceValues.First().Key.Should().Be(GERMAN);
        confidenceValues.Last().Key.Should().Be(ENGLISH);

        confidenceValues[GERMAN].Should().Be(1.0);
        confidenceValues[ENGLISH].Should().BeApproximately(
            (totalProbabilityForGerman / totalProbabilityForEnglish),
            0.000001
        );
    }

    [Fact]
    public void UnknownLanguageReturnedWhenNoNgramProbabilitiesAvailable() =>
        _detectorForEnglishAndGerman.DetectLanguageOf("проарплап").Should().Be(UNKNOWN);

    [Fact]
    public void NoConfidenceValuesReturnedWhenNoNgramProbabilitiesAvailable() =>
        _detectorForEnglishAndGerman.ComputeLanguageConfidenceValues("проарплап").Should().BeEmpty();

    public static IEnumerable<object[]> AmbiguousTextProvider()
    {
        yield return
        [
            "ام وی با نیکی میناج تیزر داشت؟؟؟؟؟؟ i vote for bts ( _ ) as the _ via ( _ )",
            new[] { ENGLISH, URDU }
        ];

        yield return
        [
            "Az elmúlt hétvégén 12-re emelkedett az elhunyt koronavírus-fertőzöttek száma Szlovákiában. " +
            "Mindegyik szociális otthon dolgozóját letesztelik, " +
            "Matovič szerint az ingázóknak még várniuk kellene a teszteléssel",
            new[] { HUNGARIAN, SLOVAK }
        ];
    }

    [Theory]
    [MemberData(nameof(AmbiguousTextProvider))]
    public void LanguageDetectionIsDeterministic(string text, Language[] languages)
    {
        RemoveLanguageModelsFromDetector();

        AssertThatAllLanguageModelsAreUnloaded();

        var detector = LanguageDetectorBuilder
            .FromLanguages(languages)
            .WithPreloadedLanguageModels()
            .Build();

        var detectedLanguages = new HashSet<Language>();
        for (var i = 0; i < 100; i++)
        {
            var language = detector.DetectLanguageOf(text);
            detectedLanguages.Add(language);
        }

        detectedLanguages.Should().HaveCount(1);

        RemoveLanguageModelsFromDetector();

        AssertThatAllLanguageModelsAreUnloaded();

        AddLanguageModelsToDetector();

        AssertThatAllLanguageModelsAreLoaded();
    }

    [Fact]
    public void LanguageModelsCanBeProperlyUnloaded()
    {
        AssertThatAllLanguageModelsAreLoaded();

        _detectorForEnglishAndGerman.UnloadLanguageModels();

        AssertThatAllLanguageModelsAreUnloaded();

        AddLanguageModelsToDetector();

        AssertThatAllLanguageModelsAreLoaded();
    }

    [Fact]
    public void HighAccuracyModeCanBeDisabled()
    {
        RemoveLanguageModelsFromDetector();

        AssertThatAllLanguageModelsAreUnloaded();

        var detector = LanguageDetectorBuilder
            .FromLanguages(ENGLISH, GERMAN)
            .WithPreloadedLanguageModels()
            .WithLowAccuracyMode()
            .Build();

        AssertThatOnlyTrigramLanguageModelsAreLoaded();

        detector.DetectLanguageOf("short text");

        AssertThatOnlyTrigramLanguageModelsAreLoaded();

        AddLanguageModelsToDetector();

        AssertThatAllLanguageModelsAreLoaded();
    }

    [Fact]
    public void LowAccuracyModeReportsUnknownLanguageForUnigramsAndBigrams()
    {
        RemoveLanguageModelsFromDetector();

        var detector = LanguageDetectorBuilder
            .FromLanguages(ENGLISH, GERMAN)
            .WithPreloadedLanguageModels()
            .WithLowAccuracyMode()
            .Build();

        detector.DetectLanguageOf("bed").Should().NotBe(UNKNOWN);
        detector.DetectLanguageOf("be").Should().Be(UNKNOWN);
        detector.DetectLanguageOf("b").Should().Be(UNKNOWN);
        detector.DetectLanguageOf("").Should().Be(UNKNOWN);

        AddLanguageModelsToDetector();
    }

    private void AssertThatAllLanguageModelsAreUnloaded()
    {
        LanguageDetector.UnigramLanguageModels.Should().BeEmpty();
        LanguageDetector.BigramLanguageModels.Should().BeEmpty();
        LanguageDetector.TrigramLanguageModels.Should().BeEmpty();
        LanguageDetector.QuadrigramLanguageModels.Should().BeEmpty();
        LanguageDetector.FivegramLanguageModels.Should().BeEmpty();
    }

    private void AssertThatOnlyTrigramLanguageModelsAreLoaded()
    {
        LanguageDetector.UnigramLanguageModels.Should().BeEmpty();
        LanguageDetector.BigramLanguageModels.Should().BeEmpty();
        LanguageDetector.TrigramLanguageModels.Should().NotBeEmpty();
        LanguageDetector.QuadrigramLanguageModels.Should().BeEmpty();
        LanguageDetector.FivegramLanguageModels.Should().BeEmpty();
    }

    private void AssertThatAllLanguageModelsAreLoaded()
    {
        LanguageDetector.UnigramLanguageModels.Should().NotBeEmpty();
        LanguageDetector.BigramLanguageModels.Should().NotBeEmpty();
        LanguageDetector.TrigramLanguageModels.Should().NotBeEmpty();
        LanguageDetector.QuadrigramLanguageModels.Should().NotBeEmpty();
        LanguageDetector.FivegramLanguageModels.Should().NotBeEmpty();
    }

    private void AddLanguageModelsToDetector()
    {
        LanguageDetector.UnigramLanguageModels[ENGLISH] = UnigramLanguageModelForEnglish;
        LanguageDetector.UnigramLanguageModels[GERMAN] = UnigramLanguageModelForGerman;

        LanguageDetector.BigramLanguageModels[ENGLISH] = BigramLanguageModelForEnglish;
        LanguageDetector.BigramLanguageModels[GERMAN] = BigramLanguageModelForGerman;

        LanguageDetector.TrigramLanguageModels[ENGLISH] = TrigramLanguageModelForEnglish;
        LanguageDetector.TrigramLanguageModels[GERMAN] = TrigramLanguageModelForGerman;

        LanguageDetector.QuadrigramLanguageModels[ENGLISH] = QuadrigramLanguageModelForEnglish;
        LanguageDetector.QuadrigramLanguageModels[GERMAN] = QuadrigramLanguageModelForGerman;

        LanguageDetector.FivegramLanguageModels[ENGLISH] = FivegramLanguageModelForEnglish;
        LanguageDetector.FivegramLanguageModels[GERMAN] = FivegramLanguageModelForGerman;
    }

    private void RemoveLanguageModelsFromDetector()
    {
        LanguageDetector.UnigramLanguageModels.Clear();
        LanguageDetector.BigramLanguageModels.Clear();
        LanguageDetector.TrigramLanguageModels.Clear();
        LanguageDetector.QuadrigramLanguageModels.Clear();
        LanguageDetector.FivegramLanguageModels.Clear();
    }
}
