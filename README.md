![lingua](https://raw.githubusercontent.com/searchpioneer/lingua-dotnet/main/images/logo.png) 

[![NuGet Release][nuget image]][nuget url]
[![Build Status](https://github.com/searchpioneer/lingua-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/searchpioneer/lingua-dotnet/actions/workflows/dotnet.yml)
[![Accuracy Report](https://github.com/searchpioneer/lingua-dotnet/actions/workflows/accuracy_report.yml/badge.svg)](https://github.com/searchpioneer/lingua-dotnet/actions/workflows/accuracy_report.yml)
[![Benchmark](https://github.com/searchpioneer/lingua-dotnet/actions/workflows/benchmark.yml/badge.svg)](https://github.com/searchpioneer/lingua-dotnet/actions/workflows/benchmark.yml)
[![license badge][license badge]][license url]
[![supported languages][supported languages badge]](#which-languages-are-supported)

## What does this library do?
Its task is simple: It tells you which language some provided textual data is written in. 
This is very useful as a preprocessing step for linguistic data in natural language 
processing applications such as text classification and spell checking. 
Other use cases, for instance, might include routing e-mails to the right geographically 
located customer service department, based on the e-mails' languages.

This is a .NET port of the [JVM](https://github.com/pemistahl/lingua) and [Rust](https://github.com/pemistahl/lingua-rs) implementations of Lingua.

## Why does this library exist?
Language detection is often done as part of large machine learning frameworks or natural 
language processing applications. In cases where you don't need the full-fledged 
functionality of those systems or don't want to learn the ropes of those, 
a small flexible library comes in handy.

So far, two other comprehensive open source libraries working on the CLR for this task
are [LanguageDetection](https://github.com/KRSogaard/language-detection) and 
[NTextCat](https://github.com/ivanakcheurov/ntextcat).
Unfortunately these have major drawbacks:

1. Detection only works well with quite lengthy text fragments.
   For very short text snippets such as Twitter messages, it doesn't provide adequate results.
2. The more languages take part in the decision process, the less accurate are the detection results.
3. They don't support as many languages
4. They are not as fast overall

*Lingua* nearly doesn't need any configuration and 
yields pretty accurate results on both long and short text, even on single words and phrases. 
It draws on both rule-based and statistical methods but does not use any dictionaries of words. 
It does not need a connection to any external API or service either. 
Once the library has been downloaded, it can be used completely offline. 

## Which languages are supported?

Compared to other language detection libraries, *Lingua's* focus is on *quality over quantity*, that is, 
getting detection right for a small set of languages first before adding new ones. 
Currently, the following 79 languages are supported:

- A
  - Afrikaans
  - Albanian
  - Amharic
  - Arabic
  - Armenian
  - Azerbaijani
- B
  - Basque
  - Belarusian
  - Bengali
  - Bokmal (Norwegian)
  - Bosnian
  - Bulgarian
- C
  - Catalan
  - Chinese
  - Croatian
  - Czech
- D
  - Danish
  - Dutch
- E
  - English
  - Esperanto
  - Estonian
- F
  - Finnish
  - French
- G
  - Ganda
  - Georgian
  - German
  - Greek
  - Gujarati
- H
  - Hebrew
  - Hindi
  - Hungarian
- I
  - Icelandic
  - Indonesian
  - Irish
  - Italian
- J
  - Japanese
- K
  - Kazakh
  - Korean
- L
  - Latin
  - Latvian
  - Lithuanian
- M
  - Macedonian
  - Malay
  - Maori
  - Marathi
  - Mongolian
- N
  - Nynorsk (Norwegian)
- O
  - Oromo
- P
  - Persian
  - Polish
  - Portuguese
  - Punjabi
- R
  - Romanian
  - Russian
- S
  - Serbian
  - Shona
  - Sinhala
  - Slovak
  - Slovene
  - Somali
  - Sotho
  - Spanish
  - Swahili
  - Swedish
- T
  - Tagalog
  - Tamil
  - Telugu
  - Thai
  - Tigrinya
  - Tsonga
  - Tswana
  - Turkish
- U
  - Ukrainian
  - Urdu
- V
  - Vietnamese
- W
  - Welsh
- X
  - Xhosa
- Y
  - Yoruba
- Z
  - Zulu

## How accurate is it?

Lingua is able to report accuracy statistics for some bundled test data available for each supported language.
The test data for each language is split into three parts:

1. a list of single words with a minimum length of 5 characters
2. list of word pairs with a minimum length of 10 characters 
3. a list of complete grammatical sentences of various lengths

Both the language models and the test data have been created from separate documents of the 
[Wortschatz corpora](https://wortschatz.uni-leipzig.de/) offered by Leipzig University, Germany. Data crawled from various news websites have 
been used for training, each corpus comprising one million sentences. For testing, corpora made of arbitrarily
chosen websites have been used, each comprising ten thousand sentences. From each test corpus, a random
unsorted subset of 1000 single words, 1000 word pairs and 1000 sentences has been extracted, respectively.

See the [latest Accuracy Report](https://github.com/searchpioneer/lingua-dotnet/actions/workflows/accuracy_report.yml).

## How to use

### Installation

Install with nuget

```sh
dotnet add package Lingua
```

### Basic Usage

```csharp
using Lingua;
using static Lingua.Language;

var detector = LanguageDetectorBuilder
    .FromLanguages(English, French, German, Spanish)
    .Build();

var detectedLanguage = detector.DetectLanguageOf("languages are awesome");
Assert.Equal(English, detectedLanguage);
```

### Minimum relative distance

By default, *Lingua* returns the most likely language for a given input text. However, there are
certain words that are spelled the same in more than one language. The word *prologue*, for
instance, is both a valid English and French word. *Lingua* would output either English or
French which might be wrong in the given context. For cases like that, it is possible to
specify a minimum relative distance that the logarithmized and summed up probabilities for
each possible language have to satisfy. It can be stated in the following way:

```csharp
using Lingua;
using static Lingua.Language;

var detector = LanguageDetectorBuilder
  .FromLanguages(English, French, German, Spanish)
  .WithMinimumRelativeDistance(0.9)
  .Build();

var detectedLanguage = detector.DetectLanguageOf("languages are awesome");
Assert.Equal(Unknown, detectedLanguage);
```

Be aware that the distance between the language probabilities is dependent on the length of the
input text. The longer the input text, the larger the distance between the languages. So if you
want to classify very short text phrases, do not set the minimum relative distance too high.
Otherwise `Unknown` language will be returned most of the time as in the example above. 
This is the return value for cases where language detection is not reliably possible.

### Confidence values

Knowing about the most likely language is nice but how reliable is the computed likelihood?
And how less likely are the other examined languages in comparison to the most likely one?
These questions can be answered as well:

```csharp
using Lingua;
using static Lingua.Language;

var detector = LanguageDetectorBuilder
  .FromLanguages(English, French, German, Spanish)
  .Build();

var detectedLanguage = detector.ComputeLanguageConfidenceValues("languages are awesome")
  .Select(kv => (kv.Key, Math.Round(kv.Value, 2)))
  .ToArray();

Assert.Equal(new []
{
  (English, 0.93),
  (French, 0.04),
  (German, 0.02),
  (Spanish, 0.01)
}, detectedLanguage);
```

In the example above, an array of tuples is returned containing all possible languages
sorted by their confidence value in descending order. Each value is a probability between 0.0 and 1.0.
The probabilities of all languages will sum to 1.0. If the language is unambiguously identified by
the rule engine, the value 1.0 will always be returned for this language. 
The other languages will receive a value of 0.0.

### Eager loading versus lazy loading

By default, *Lingua* uses lazy-loading to load only those language models on demand which are
considered relevant by the rule-based filter engine. For web services, for instance, it is
rather beneficial to preload all language models into memory to avoid unexpected latency while
waiting for the service response. If you want to enable the eager-loading mode, you can do it
like this:

```csharp
using Lingua;

var detector = LanguageDetectorBuilder
  .FromAllLanguages()
  .WithPreloadedLanguageModels()
  .Build();
```

Multiple instances of `LanguageDetector` share the same language models in memory which are
accessed asynchronously by the instances.

### Low accuracy mode versus high accuracy mode

*Lingua's* high detection accuracy comes at the cost of being noticeably slower
than other language detectors. The large language models also consume significant
amounts of memory. These requirements might not be feasible for systems running low
on resources. If you want to classify mostly long texts or need to save resources,
you can enable a *low accuracy mode* that loads only a small subset of the language
models into memory:

```csharp
using Lingua;

var detector = LanguageDetectorBuilder
  .FromAllLanguages()
  .WithLowAccuracyMode()
  .Build();
```

The downside of this approach is that detection accuracy for short texts consisting
of less than 120 characters will drop significantly. However, detection accuracy for
texts which are longer than 120 characters will remain mostly unaffected.

An alternative for a smaller memory footprint and faster performance is to reduce the set
of languages when building the language detector. In most cases, it is not advisable to
build the detector from all supported languages. When you have knowledge about
the texts you want to classify you can almost always rule out certain languages as impossible
or unlikely to occur.

[nuget url]: https://www.nuget.org/packages/SearchPioneer.Lingua/
[nuget image]: https://img.shields.io/nuget/v/SearchPioneer.Lingua.svg
[license badge]: https://img.shields.io/badge/license-Apache%202.0-blue.svg
[license url]: https://www.apache.org/licenses/LICENSE-2.0
[supported languages badge]: https://img.shields.io/badge/supported%20languages-79-green.svg
