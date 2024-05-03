using FluentAssertions;
using Lingua.Internal;
using Xunit;
using static Lingua.Language;

namespace Lingua.Tests;

public class LanguageDetectorTests : IDisposable
{
	private static readonly Dictionary<string, double> UnigramLanguageModelForEnglish = new()
	{
		["a"] = 0.01,
		["l"] = 0.02,
		["t"] = 0.03,
		["e"] = 0.04,
		["r"] = 0.05,
		// unknown unigram in model
		["w"] = 0
	};

	private static readonly Dictionary<string, double> BigramLanguageModelForEnglish = new()
	{
		["al"] = 0.11,
		["lt"] = 0.12,
		["te"] = 0.13,
		["er"] = 0.14,
		// unknown bigrams in model
		["aq"] = 0,
		["wx"] = 0
	};

	private static readonly Dictionary<string, double> TrigramLanguageModelForEnglish = new()
	{
		["alt"] = 0.19,
		["lte"] = 0.2,
		["ter"] = 0.21,
		// unknown trigrams in model
		["aqu"] = 0,
		["tez"] = 0,
		["wxy"] = 0
	};

	private static readonly Dictionary<string, double> QuadrigramLanguageModelForEnglish = new()
	{
		["alte"] = 0.25,
		["lter"] = 0.26,
		// unknown quadrigrams in model
		["aqua"] = 0,
		["wxyz"] = 0
	};

	private static readonly Dictionary<string, double> FivegramLanguageModelForEnglish = new()
	{
		["alter"] = 0.29,
		// unknown fivegrams in model
		["aquas"] = 0
	};

	private static readonly Dictionary<string, double> UnigramLanguageModelForGerman = new()
	{
		["a"] = 0.06,
		["l"] = 0.07,
		["t"] = 0.08,
		["e"] = 0.09,
		["r"] = 0.1,
		// unknown unigrams in model
		["w"] = 0
	};

	private static readonly Dictionary<string, double> BigramLanguageModelForGerman = new()
	{
		["al"] = 0.15,
		["lt"] = 0.16,
		["te"] = 0.17,
		["er"] = 0.18,
		// unknown bigrams in model
		["wx"] = 0
	};

	private static readonly Dictionary<string, double> TrigramLanguageModelForGerman = new()
	{
		["alt"] = 0.22,
		["lte"] = 0.23,
		["ter"] = 0.24,
		// unknown trigrams in model
		["wxy"] = 0
	};

	private static readonly Dictionary<string, double> QuadrigramLanguageModelForGerman = new()
	{
		["alte"] = 0.27,
		["lter"] = 0.28,
		// unknown quadrigrams in model
		["wxyz"] = 0
	};

	private static readonly Dictionary<string, double> FivegramLanguageModelForGerman = new()
	{
		["alter"] = 0.3
	};

	private readonly LanguageDetector _detectorForEnglishAndGerman = new(
		[English, German],
		0,
		false,
		false);

	private readonly LanguageDetector _detectorForAllLanguages = new(
		LanguageInfo.All().ToHashSet(),
		0,
		false,
		false);

	private static readonly TestLanguageModel UnigramTestLanguageModel = new(new()
	{
		new("a"),
		new("l"),
		new("t"),
		new("e"),
		new("r")
	});

	private static readonly TestLanguageModel TrigramTestLanguageModel = new(new()
	{
		new("alt"),
		new("lte"),
		new("ter"),
		new("wxy")
	});

	private static readonly TestLanguageModel QuadrigramTestLanguageModel = new(new()
	{
		new("alte"),
		new("lter"),
		new("wxyz")
	});

	public LanguageDetectorTests() => AddLanguageModelsToDetector();

	public void Dispose() => RemoveLanguageModelsFromDetector();

	[Fact]
	public void TextIsCleanedUpProperly() =>
		LanguageDetector.CleanUpInputText(
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
		LanguageDetector.SplitTextIntoWords("this is a sentence")
			.Should()
			.BeEquivalentTo(new List<string>
			{
				"this", "is", "a", "sentence"
			});

		LanguageDetector.SplitTextIntoWords("sentence")
			.Should()
			.BeEquivalentTo(new List<string>
			{
				"sentence"
			});

		LanguageDetector.SplitTextIntoWords("上海大学是一个好大学 this is a sentence")
			.Should()
			.BeEquivalentTo(new List<string>
			{
				"上", "海", "大", "学", "是", "一", "个", "好", "大", "学", "this", "is", "a", "sentence"
			});
	}

	[Theory]
	[InlineData(English, "a", 0.01)]
	[InlineData(English, "lt", 0.12)]
	[InlineData(English, "ter", 0.21)]
	[InlineData(English, "alte", 0.25)]
	[InlineData(English, "alter", 0.29)]
	[InlineData(German, "t", 0.08)]
	[InlineData(German, "er", 0.18)]
	[InlineData(German, "alt", 0.22)]
	[InlineData(German, "lter", 0.28)]
	[InlineData(German, "alter", 0.30)]
	public void NgramProbabilityLookupWorksCorrectly(Language language, string ngram, double expectedProbability) =>
		LanguageDetector.LookupNgramProbability(language, ngram).Should()
			.Be(expectedProbability);

	[Fact]
	public void NgramProbabilityLookupThrowsForZerogram()
	{
		var exception = Assert.Throws<ArgumentException>(() =>
				LanguageDetector.LookupNgramProbability(English, ""));

		exception.Message.Should().Be("Zerogram detected");
	}

	public static IEnumerable<object[]> NgramProbabilityProvider()
	{
		yield return
		[
			new HashSet<Ngram> { new("a"), new("l"), new("t"), new("e"), new("r") },
			Math.Log(0.01) + Math.Log(0.02) + Math.Log(0.03) + Math.Log(0.04) + Math.Log(0.05)
		];

		yield return
		[
			new HashSet<Ngram> { new("alt"), new("lte"), new("tez") },
			Math.Log(0.19) + Math.Log(0.2) + Math.Log(0.13)
		];

		yield return
		[
			new HashSet<Ngram> { new("aquas") },
			Math.Log(0.01)
		];
	}

	[Theory]
	[MemberData(nameof(NgramProbabilityProvider))]
	internal void SumOfNgramProbabilitiesComputedCorrectly(HashSet<Ngram> ngrams, double expectedSumOfProbabilities) =>
		LanguageDetector.ComputeSumOfNgramProbabilities(English, ngrams)
			.Should()
			.Be(expectedSumOfProbabilities);

	public static IEnumerable<object[]> LanguageProbabilitiesProvider()
	{
		yield return
		[
			UnigramTestLanguageModel,
			new Dictionary<Language, double>
			{
				[English] = Math.Log(0.01) + Math.Log(0.02) + Math.Log(0.03) + Math.Log(0.04) + Math.Log(0.05),
				[German] = Math.Log(0.06) + Math.Log(0.07) + Math.Log(0.08) + Math.Log(0.09) + Math.Log(0.1),
			}
		];

		yield return
		[
			TrigramTestLanguageModel,
			new Dictionary<Language, double>
			{
				[English] = Math.Log(0.19) + Math.Log(0.2) + Math.Log(0.21),
				[German] = Math.Log(0.22) + Math.Log(0.23) + Math.Log(0.24),
			}
		];

		yield return
		[
			QuadrigramTestLanguageModel,
			new Dictionary<Language, double>
			{
				[English] = Math.Log(0.25) + Math.Log(0.26),
				[German] = Math.Log(0.27) + Math.Log(0.28),
			}
		];
	}

	[Theory]
	[MemberData(nameof(LanguageProbabilitiesProvider))]
	internal void LanguageProbabilitiesComputedCorrectly(TestLanguageModel model,
		Dictionary<Language, double> expectedProbabilities) =>
		LanguageDetector.ComputeLanguageProbabilities(model, _detectorForEnglishAndGerman.Languages)
			.Should().BeEquivalentTo(expectedProbabilities);

	[Theory]
	[InlineData("məhərrəm", Azerbaijani)]
	[InlineData("substituïts", Catalan)]
	[InlineData("rozdělit", Czech)]
	[InlineData("tvořen", Czech)]
	[InlineData("subjektů", Czech)]
	[InlineData("nesufiĉecon", Esperanto)]
	[InlineData("intermiksiĝis", Esperanto)]
	[InlineData("monaĥinoj", Esperanto)]
	[InlineData("kreitaĵoj", Esperanto)]
	[InlineData("ŝpinante", Esperanto)]
	[InlineData("apenaŭ", Esperanto)]
	[InlineData("groß", German)]
	[InlineData("σχέδια", Greek)]
	[InlineData("fekvő", Hungarian)]
	[InlineData("meggyűrűzni", Hungarian)]
	[InlineData("ヴェダイヤモンド", Japanese)]
	[InlineData("әлем", Kazakh)]
	[InlineData("шаруашылығы", Kazakh)]
	[InlineData("ақын", Kazakh)]
	[InlineData("оның", Kazakh)]
	[InlineData("шұрайлы", Kazakh)]
	[InlineData("teoloģiska", Latvian)]
	[InlineData("blaķene", Latvian)]
	[InlineData("ceļojumiem", Latvian)]
	[InlineData("numuriņu", Latvian)]
	[InlineData("mergelės", Lithuanian)]
	[InlineData("įrengus", Lithuanian)]
	[InlineData("slegiamų", Lithuanian)]
	[InlineData("припаѓа", Macedonian)]
	[InlineData("ѕидови", Macedonian)]
	[InlineData("ќерка", Macedonian)]
	[InlineData("џамиите", Macedonian)]
	[InlineData("मिळते", Marathi)]
	[InlineData("үндсэн", Mongolian)]
	[InlineData("дөхөж", Mongolian)]
	[InlineData("zmieniły", Polish)]
	[InlineData("państwowych", Polish)]
	[InlineData("mniejszości", Polish)]
	[InlineData("groźne", Polish)]
	[InlineData("ialomiţa", Romanian)]
	[InlineData("наслеђивања", Serbian)]
	[InlineData("неисквареношћу", Serbian)]
	[InlineData("podĺa", Slovak)]
	[InlineData("pohľade", Slovak)]
	[InlineData("mŕtvych", Slovak)]
	[InlineData("ґрунтовому", Ukrainian)]
	[InlineData("пропонує", Ukrainian)]
	[InlineData("пристрої", Ukrainian)]
	[InlineData("cằm", Vietnamese)]
	[InlineData("thần", Vietnamese)]
	[InlineData("chẳng", Vietnamese)]
	[InlineData("quẩy", Vietnamese)]
	[InlineData("sẵn", Vietnamese)]
	[InlineData("nhẫn", Vietnamese)]
	[InlineData("dắt", Vietnamese)]
	[InlineData("chất", Vietnamese)]
	[InlineData("đạp", Vietnamese)]
	[InlineData("mặn", Vietnamese)]
	[InlineData("hậu", Vietnamese)]
	[InlineData("hiền", Vietnamese)]
	[InlineData("lẻn", Vietnamese)]
	[InlineData("biểu", Vietnamese)]
	[InlineData("kẽm", Vietnamese)]
	[InlineData("diễm", Vietnamese)]
	[InlineData("phế", Vietnamese)]
	[InlineData("việc", Vietnamese)]
	[InlineData("chỉnh", Vietnamese)]
	[InlineData("trĩ", Vietnamese)]
	[InlineData("ravị", Vietnamese)]
	[InlineData("thơ", Vietnamese)]
	[InlineData("nguồn", Vietnamese)]
	[InlineData("thờ", Vietnamese)]
	[InlineData("sỏi", Vietnamese)]
	[InlineData("tổng", Vietnamese)]
	[InlineData("nhở", Vietnamese)]
	[InlineData("mỗi", Vietnamese)]
	[InlineData("bỡi", Vietnamese)]
	[InlineData("tốt", Vietnamese)]
	[InlineData("giới", Vietnamese)]
	[InlineData("một", Vietnamese)]
	[InlineData("hợp", Vietnamese)]
	[InlineData("hưng", Vietnamese)]
	[InlineData("từng", Vietnamese)]
	[InlineData("của", Vietnamese)]
	[InlineData("sử", Vietnamese)]
	[InlineData("cũng", Vietnamese)]
	[InlineData("những", Vietnamese)]
	[InlineData("chức", Vietnamese)]
	[InlineData("dụng", Vietnamese)]
	[InlineData("thực", Vietnamese)]
	[InlineData("kỳ", Vietnamese)]
	[InlineData("kỷ", Vietnamese)]
	[InlineData("mỹ", Vietnamese)]
	[InlineData("mỵ", Vietnamese)]
	[InlineData("aṣiwèrè", Yoruba)]
	[InlineData("ṣaaju", Yoruba)]
	[InlineData("والموضوع", Unknown)]
	[InlineData("сопротивление", Unknown)]
	[InlineData("house", Unknown)]
	public void LanguageOfSingleWordWithUniqueCharactersCanBeUnambiguouslyIdentifiedWithRules(
		string word,
		Language expectedLanguage) =>
		_detectorForAllLanguages.DetectLanguageWithRules([word]).Should().Be(expectedLanguage);

	[Theory]
	[InlineData("ունենա", Armenian)]
	[InlineData("জানাতে", Bengali)]
	[InlineData("გარეუბან", Georgian)]
	[InlineData("σταμάτησε", Greek)]
	[InlineData("ઉપકરણોની", Gujarati)]
	[InlineData("בתחרויות", Hebrew)]
	[InlineData("びさ", Japanese)]
	[InlineData("대결구도가", Korean)]
	[InlineData("ਮੋਟਰਸਾਈਕਲਾਂ", Punjabi)]
	[InlineData("துன்பங்களை", Tamil)]
	[InlineData("కృష్ణదేవరాయలు", Telugu)]
	[InlineData("ในทางหลวงหมายเลข", Thai)]
	public void LanguageOfSingleWordWithUniqueAlphabetCanBeUnambiguouslyIdentifiedWithRules(
		string word,
		Language expectedLanguage) =>
		_detectorForAllLanguages.DetectLanguageWithRules([word]).Should().Be(expectedLanguage);

	public static IEnumerable<object[]> FilteredLanguagesProvider()
	{
		yield return
		[
			"والموضوع",
			new HashSet<Language> { Arabic, Persian, Urdu }
		];
		yield return
		[
			"сопротивление",
			new HashSet<Language> { Belarusian, Bulgarian, Kazakh, Macedonian, Mongolian, Russian, Serbian, Ukrainian }
		];
		yield return
		[
			"раскрывае",
			new HashSet<Language> { Belarusian, Kazakh, Mongolian, Russian }
		];
		yield return
		[
			"этот",
			new HashSet<Language> { Belarusian, Kazakh, Mongolian, Russian }
		];
		yield return
		[
			"огнём",
			new HashSet<Language> { Belarusian, Kazakh, Mongolian, Russian }
		];
		yield return
		[
			"плаваща",
			new HashSet<Language> { Bulgarian, Kazakh, Mongolian, Russian }
		];
		yield return
		[
			"довършат",
			new HashSet<Language> { Bulgarian, Kazakh, Mongolian, Russian }
		];
		yield return
		[
			"павінен",
			new HashSet<Language> { Belarusian, Kazakh, Ukrainian }
		];
		yield return
		[
			"затоплување",
			new HashSet<Language> { Macedonian, Serbian }
		];
		yield return
		[
			"ректасцензија",
			new HashSet<Language> { Macedonian, Serbian }
		];
		yield return
		[
			"набљудувач",
			new HashSet<Language> { Macedonian, Serbian }
		];
		yield return
		[
			"aizklātā",
			new HashSet<Language> { Latvian, Maori, Yoruba }
		];
		yield return
		[
			"sistēmas",
			new HashSet<Language> { Latvian, Maori, Yoruba }
		];
		yield return
		[
			"palīdzi",
			new HashSet<Language> { Latvian, Maori, Yoruba }
		];
		yield return
		[
			"nhẹn",
			new HashSet<Language> { Vietnamese, Yoruba }
		];
		yield return
		[
			"chọn",
			new HashSet<Language> { Vietnamese, Yoruba }
		];
		yield return
		[
			"prihvaćanju",
			new HashSet<Language> { Bosnian, Croatian, Polish }
		];
		yield return
		[
			"nađete",
			new HashSet<Language> { Bosnian, Croatian, Vietnamese }
		];
		yield return
		[
			"visão",
			new HashSet<Language> { Portuguese, Vietnamese }
		];
		yield return
		[
			"wystąpią",
			new HashSet<Language> { Lithuanian, Polish }
		];
		yield return
		[
			"budowę",
			new HashSet<Language> { Lithuanian, Polish }
		];
		yield return
		[
			"nebūsime",
			new HashSet<Language> { Latvian, Lithuanian, Maori, Yoruba }
		];
		yield return
		[
			"afişate",
			new HashSet<Language> { Azerbaijani, Romanian, Turkish }
		];
		yield return
		[
			"kradzieżami",
			new HashSet<Language> { Polish, Romanian }
		];
		yield return
		[
			"înviat",
			new HashSet<Language> { French, Romanian }
		];
		yield return
		[
			"venerdì",
			new HashSet<Language> { Italian, Vietnamese, Yoruba }
		];
		yield return
		[
			"años",
			new HashSet<Language> { Basque, Spanish }
		];
		yield return
		[
			"rozohňuje",
			new HashSet<Language> { Czech, Slovak }
		];
		yield return
		[
			"rtuť",
			new HashSet<Language> { Czech, Slovak }
		];
		yield return
		[
			"pregătire",
			new HashSet<Language> { Romanian, Vietnamese }
		];
		yield return
		[
			"jeďte",
			new HashSet<Language> { Czech, Romanian, Slovak }
		];
		yield return
		[
			"minjaverðir",
			new HashSet<Language> { Icelandic, Turkish }
		];
		yield return
		[
			"þagnarskyldu",
			new HashSet<Language> { Icelandic, Turkish }
		];
		yield return
		[
			"nebûtu",
			new HashSet<Language> { French, Hungarian }
		];
		yield return
		[
			"hashemidëve",
			new HashSet<Language> { Afrikaans, Albanian, Dutch, French }
		];
		yield return
		[
			"forêt",
			new HashSet<Language> { Afrikaans, French, Portuguese, Vietnamese }
		];
		yield return
		[
			"succèdent",
			new HashSet<Language> { French, Italian, Vietnamese, Yoruba }
		];
		yield return
		[
			"où",
			new HashSet<Language> { French, Italian, Vietnamese, Yoruba }
		];
		yield return
		[
			"tõeliseks",
			new HashSet<Language> { Estonian, Hungarian, Portuguese, Vietnamese }
		];
		yield return
		[
			"viòiem",
			new HashSet<Language> { Catalan, Italian, Vietnamese, Yoruba }
		];
		yield return
		[
			"contrôle",
			new HashSet<Language> { French, Portuguese, Slovak, Vietnamese }
		];
		yield return
		[
			"direktør",
			new HashSet<Language> { Bokmal, Danish, Nynorsk }
		];
		yield return
		[
			"vývoj",
			new HashSet<Language> { Czech, Icelandic, Slovak, Turkish, Vietnamese }
		];
		yield return
		[
			"päralt",
			new HashSet<Language> { Estonian, Finnish, German, Slovak, Swedish }
		];
		yield return
		[
			"labâk",
			new HashSet<Language> { French, Portuguese, Romanian, Turkish, Vietnamese }
		];
		yield return
		[
			"pràctiques",
			new HashSet<Language> { Catalan, French, Italian, Portuguese, Vietnamese }
		];
		yield return
		[
			"überrascht",
			new HashSet<Language> { Azerbaijani, Catalan, Estonian, German, Hungarian, Spanish, Turkish }
		];
		yield return
		[
			"indebærer",
			new HashSet<Language> { Bokmal, Danish, Icelandic, Nynorsk }
		];
		yield return
		[
			"måned",
			new HashSet<Language> { Bokmal, Danish, Nynorsk, Swedish }
		];
		yield return
		[
			"zaručen",
			new HashSet<Language> { Bosnian, Czech, Croatian, Latvian, Lithuanian, Slovak, Slovene }
		];
		yield return
		[
			"zkouškou",
			new HashSet<Language> { Bosnian, Czech, Croatian, Latvian, Lithuanian, Slovak, Slovene }
		];
		yield return
		[
			"navržen",
			new HashSet<Language> { Bosnian, Czech, Croatian, Latvian, Lithuanian, Slovak, Slovene }
		];
		yield return
		[
			"façonnage",
			new HashSet<Language> { Albanian, Azerbaijani, Basque, Catalan, French, Portuguese, Turkish }
		];
		yield return
		[
			"höher",
			new HashSet<Language> { Azerbaijani, Estonian, Finnish, German, Hungarian, Icelandic, Swedish, Turkish }
		];
		yield return
		[
			"catedráticos",
			new HashSet<Language>
				{ Catalan, Czech, Icelandic, Irish, Hungarian, Portuguese, Slovak, Spanish, Vietnamese, Yoruba }
		];
		yield return
		[
			"política",
			new HashSet<Language>
				{ Catalan, Czech, Icelandic, Irish, Hungarian, Portuguese, Slovak, Spanish, Vietnamese, Yoruba }
		];
		yield return
		[
			"música",
			new HashSet<Language>
				{ Catalan, Czech, Icelandic, Irish, Hungarian, Portuguese, Slovak, Spanish, Vietnamese, Yoruba }
		];
		yield return
		[
			"contradicció",
			new HashSet<Language>
				{ Catalan, Hungarian, Icelandic, Irish, Polish, Portuguese, Slovak, Spanish, Vietnamese, Yoruba }
		];
		yield return
		[
			"només",
			new HashSet<Language>
			{
				Catalan, Czech, French, Hungarian, Icelandic, Irish, Italian, Portuguese, Slovak, Spanish,
				Vietnamese, Yoruba
			}
		];
		yield return
		[
			"house",
			new HashSet<Language>
			{
				Afrikaans, Albanian, Azerbaijani, Basque, Bokmal, Bosnian, Catalan, Croatian, Czech, Danish,
				Dutch, English, Esperanto, Estonian, Finnish, French, Ganda, German, Hungarian, Icelandic,
				Indonesian, Irish, Italian, Latin, Latvian, Lithuanian, Malay, Maori, Nynorsk, Oromo, Polish,
				Portuguese, Romanian, Shona, Slovak, Slovene, Somali, Sotho, Spanish, Swahili, Swedish,
				Tagalog, Tsonga, Tswana, Turkish, Vietnamese, Welsh, Xhosa, Yoruba, Zulu
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
		_detectorForAllLanguages.DetectLanguageOf(invalidString).Should().Be(Unknown);

	[Fact]
	public void LanguageOfGermanNounAlterCanBeDetectedCorrectly() =>
		_detectorForEnglishAndGerman.DetectLanguageOf("Alter").Should().Be(German);

	[Fact]
	public void LanguageConfidenceValuesComputedCorrectly()
	{
		List<(Language Language, double Probability)> confidenceValues =
			_detectorForEnglishAndGerman.ComputeLanguageConfidenceValues("Alter")
			.Select(kv => (kv.Key, Math.Round(kv.Value, 2)))
			.ToList();

		confidenceValues.First().Language.Should().Be(German);
		confidenceValues.First().Probability.Should().Be(0.81);

		confidenceValues.Last().Language.Should().Be(English);
		confidenceValues.Last().Probability.Should().Be(0.19);
	}

	[Fact]
	public void UnknownLanguageReturnedWhenNoNgramProbabilitiesAvailable() =>
		_detectorForEnglishAndGerman.DetectLanguageOf("проарплап").Should().Be(Unknown);

	[Fact]
	public void ZeroConfidenceValuesReturnedWhenNoNgramProbabilitiesAvailable() =>
		_detectorForEnglishAndGerman.ComputeLanguageConfidenceValues("проарплап").Should().BeEquivalentTo(
			new Dictionary<Language, double>()
			{
				[English] = 0,
				[German] = 0
			});

	public static IEnumerable<object[]> AmbiguousTextProvider()
	{
		yield return
		[
			"ام وی با نیکی میناج تیزر داشت؟؟؟؟؟؟ i vote for bts ( _ ) as the _ via ( _ )",
			new[] { English, Urdu }
		];

		yield return
		[
			"Az elmúlt hétvégén 12-re emelkedett az elhunyt koronavírus-fertőzöttek száma Szlovákiában. " +
			"Mindegyik szociális otthon dolgozóját letesztelik, " +
			"Matovič szerint az ingázóknak még várniuk kellene a teszteléssel",
			new[] { Hungarian, Slovak }
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
			.FromLanguages(English, German)
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
			.FromLanguages(English, German)
			.WithPreloadedLanguageModels()
			.WithLowAccuracyMode()
			.Build();

		detector.DetectLanguageOf("bed").Should().NotBe(Unknown);
		detector.DetectLanguageOf("be").Should().Be(Unknown);
		detector.DetectLanguageOf("b").Should().Be(Unknown);
		detector.DetectLanguageOf("").Should().Be(Unknown);

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
		LanguageDetector.UnigramLanguageModels[English] = new Lazy<Dictionary<string, double>>(UnigramLanguageModelForEnglish);
		LanguageDetector.UnigramLanguageModels[German] = new Lazy<Dictionary<string, double>>(UnigramLanguageModelForGerman);

		LanguageDetector.BigramLanguageModels[English] = new Lazy<Dictionary<string, double>>(BigramLanguageModelForEnglish);
		LanguageDetector.BigramLanguageModels[German] = new Lazy<Dictionary<string, double>>(BigramLanguageModelForGerman);

		LanguageDetector.TrigramLanguageModels[English] = new Lazy<Dictionary<string, double>>(TrigramLanguageModelForEnglish);
		LanguageDetector.TrigramLanguageModels[German] = new Lazy<Dictionary<string, double>>(TrigramLanguageModelForGerman);

		LanguageDetector.QuadrigramLanguageModels[English] = new Lazy<Dictionary<string, double>>(QuadrigramLanguageModelForEnglish);
		LanguageDetector.QuadrigramLanguageModels[German] = new Lazy<Dictionary<string, double>>(QuadrigramLanguageModelForGerman);

		LanguageDetector.FivegramLanguageModels[English] = new Lazy<Dictionary<string, double>>(FivegramLanguageModelForEnglish);
		LanguageDetector.FivegramLanguageModels[German] = new Lazy<Dictionary<string, double>>(FivegramLanguageModelForGerman);
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
