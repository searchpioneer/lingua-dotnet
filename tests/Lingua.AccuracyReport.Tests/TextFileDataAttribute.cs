using System.Reflection;
using Lingua.Api;
using Xunit.Sdk;

namespace Lingua.AccuracyReport.Tests;

[AttributeUsage(AttributeTargets.Method)]
public class TextFileDataAttribute(string name, Language language) : DataAttribute
{
	private static readonly string TestDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

	public override IEnumerable<object[]> GetData(MethodInfo testMethod)
	{
		var path = Path.Combine(
			TestDirectory,
			"LanguageTestData",
			name,
			$"{language.IsoCode6391().ToString().ToLowerInvariant()}.txt");

		if (!File.Exists(path))
		{
			throw new ArgumentException(
				$"Cannot find file '{path}' for test method {testMethod.DeclaringType!.FullName}.{testMethod.Name}");
		}

		return File.ReadAllLines(path)
			.Select(line => new object[] {line})
			.ToList();
	}
}

public class SingleWordDataAttribute(Language language) : TextFileDataAttribute("single-words", language);
public class WordPairsDataAttribute(Language language) : TextFileDataAttribute("word-pairs", language);
public class SentenceDataAttribute(Language language) : TextFileDataAttribute("sentences", language);
