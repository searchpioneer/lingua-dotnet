using FluentAssertions;
using Lingua.Internal;
using Xunit;

namespace Lingua.Tests;

public class TokenizerTests
{
	[Theory]
	[InlineData(
		"I'm really not sure whether multi-language detection is a good idea.",
		new[] { "I", "m", "really", "not", "sure", "whether", "multi", "language", "detection", "is", "a", "good", "idea" })]
	[InlineData(
		"I'm frightened! 🙈",
		new[] { "I", "m", "frightened" })]
	[InlineData(
		"V төзімділік спорт",
		new[] { "V", "төзімділік", "спорт" })]
	[InlineData(
		"五、同业间良性的普遍推广与各团体的互助。",
		new[] { "五", "同", "业", "间", "良", "性", "的", "普", "遍", "推", "广", "与", "各", "团", "体", "的", "互", "助" })]
	[InlineData(
		"京橋創生館で1日とめても安い駐車場！",
		new[] { "京", "橋", "創", "生", "館", "で", "日", "と", "め", "て", "も", "安", "い", "駐", "車", "場" })]
	[InlineData(
		"그러나 아름다움은 또한 아주 가까이 있다.",
		new[] { "그러나", "아름다움은", "또한", "아주", "가까이", "있다" })]
	[InlineData(
		"અમુક પેટ્રોલિયમ તથા પ્રાકૃતિક ગૈસ પણ નિકળે છે.",
		new[] { "અમુક", "પેટ્રોલિયમ", "તથા", "પ્રાકૃતિક", "ગૈસ", "પણ", "નિકળે", "છે" })]
	[InlineData(
		"ਉਹ ਬੱਸ ਗਏ ਤੇ ਹੀਰੋਸ਼ੀਮਾ ਨਾਗਾਸਾਕੀ ਤੇ ਬੰਬ ਸੁੱਟ ਦਿੱਤੇ।",
		new[] { "ਉਹ", "ਬੱਸ", "ਗਏ", "ਤੇ", "ਹੀਰੋਸ਼ੀਮਾ", "ਨਾਗਾਸਾਕੀ", "ਤੇ", "ਬੰਬ", "ਸੁੱਟ", "ਦਿੱਤੇ" })]
	[InlineData(
		"அது மார்பில் செந்நிறம் பூசியது போல் ஆயிற்று.",
		new[] { "அது", "மார்பில்", "செந்நிறம்", "பூசியது", "போல்", "ஆயிற்று" })]
	[InlineData(
		"สตูล จัดพิธีมอบบ้านนักเรียน คืนความสุข สู่ลูก สพฐ.",
		new[] { "สตูล", "จัดพิธีมอบบ้านนักเรียน", "คืนความสุข", "สู่ลูก", "สพฐ" })]
	[InlineData("", new string[0])]
	[InlineData(" \n  \t;", new string[0])]
	[InlineData("3<856%)§", new string[0])]
	public void TokensWithoutWhitespaceFoundCorrectly(string text, string[] expectedTokens) =>
		Tokenizer.FindTokensWithoutWhitespace(text).Should().Equal(expectedTokens);

	[Theory]
	[InlineData(
		"  He   turned around and asked: \"Entschuldigen Sie, sprechen Sie Deutsch?\"",
		new[] { "  He   ", "turned ", "around ", "and ", "asked: ", "Entschuldigen ", "Sie, ", "sprechen ", "Sie ", "Deutsch?\"" })]
	[InlineData(
		"Parlez-vous français? Ich spreche Französisch nur ein bisschen. A little bit is better than nothing.",
		new[]
		{
			"Parlez-vous ", "français? ", "Ich ", "spreche ", "Französisch ", "nur ", "ein ", "bisschen. ",
			"A ", "little ", "bit ", "is ", "better ", "than ", "nothing."
		})]
	[InlineData(
		"English German French - Английский язык",
		new[] { "English ", "German ", "French ", "- ", "Английский ", "язык" })]
	[InlineData(
		"上海大学是一个好大学. It is such a great university.",
		new[] { "上", "海", "大", "学", "是", "一", "个", "好", "大", "学. ", "It ", "is ", "such ", "a ", "great ", "university." })]
	[InlineData(
		"3 apples and 2 bananas",
		new[] { " apples ", "and ", " bananas" })]
	[InlineData("", new string[0])]
	[InlineData(" \n  \t;", new string[0])]
	[InlineData("3<856%)§", new string[0])]
	public void TokensWithOptionalWhitespaceFoundCorrectly(string text, string[] expectedTokens) =>
		Tokenizer.FindTokensWithOptionalWhitespace(text).Select(range => text[range]).Should().Equal(expectedTokens);
}
