![lingua](images/logo.png) 

[![Build Status](https://github.com/russcam/lingua-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/russcam/lingua-dotnet/actions/workflows/dotnet.yml)
[![Accuracy Report](https://github.com/russcam/lingua-dotnet/actions/workflows/accuracy_report.yml/badge.svg)](https://github.com/russcam/lingua-dotnet/actions/workflows/accuracy_report.yml)

## 1. What does this library do?
Its task is simple: It tells you which language some provided textual data is written in. 
This is very useful as a preprocessing step for linguistic data in natural language 
processing applications such as text classification and spell checking. 
Other use cases, for instance, might include routing e-mails to the right geographically 
located customer service department, based on the e-mails' languages.

## 2. Why does this library exist?
Language detection is often done as part of large machine learning frameworks or natural 
language processing applications. In cases where you don't need the full-fledged 
functionality of those systems or don't want to learn the ropes of those, 
a small flexible library comes in handy. 

*Lingua* nearly doesn't need any configuration and 
yields pretty accurate results on both long and short text, even on single words and phrases. 
It draws on both rule-based and statistical methods but does not use any dictionaries of words. 
It does not need a connection to any external API or service either. 
Once the library has been downloaded, it can be used completely offline. 

## 3. Which languages are supported?

Compared to other language detection libraries, *Lingua's* focus is on *quality over quantity*, that is, 
getting detection right for a small set of languages first before adding new ones. 
Currently, the following 75 languages are supported:

- A
  - Afrikaans
  - Albanian
  - Arabic
  - Armenian
  - Azerbaijani
- B
  - Basque
  - Belarusian
  - Bengali
  - Norwegian Bokmal
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
  - Norwegian Nynorsk
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


