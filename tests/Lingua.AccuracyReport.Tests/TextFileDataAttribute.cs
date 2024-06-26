using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Lingua.AccuracyReport.Tests;

/// <summary>
/// A custom <see cref="IDataDiscoverer"/> that doesn't support discovery enumeration such that each parameterized
/// test case is represented as a single test. This suppresses the commandline warnings
/// 'suppress skipping test case with duplicate ID ...'
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class TextFileDataDiscoverer : DataDiscoverer
{
	public override bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod) => false;
}

/// <summary>
/// Data source for a parameterized test from a text file.
/// </summary>
/// <param name="testDirectoryName">The name of the sub-directory containing the text file.</param>
/// <param name="language">The language of the text file.</param>
[DataDiscoverer("Lingua.AccuracyReport.Tests.TextFileDataDiscoverer", "Lingua.AccuracyReport.Tests")]
[AttributeUsage(AttributeTargets.Method)]
public class TextFileDataAttribute(string testDirectoryName, Language language) : DataAttribute
{
	private static readonly string TestDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

	public override IEnumerable<object[]> GetData(MethodInfo testMethod)
	{
		var path = Path.Combine(
			TestDirectory,
			"LanguageTestData",
			testDirectoryName,
			$"{language.IsoCode6391().ToString().ToLowerInvariant()}.txt");

		if (!File.Exists(path))
		{
			throw new ArgumentException(
				$"Cannot find file '{path}' for test method {testMethod.DeclaringType!.FullName}.{testMethod.Name}");
		}

		return File.ReadAllLines(path)
			.Select(line => new object[] { line })
			.ToList();
	}
}

public class SingleWordDataAttribute(Language language) : TextFileDataAttribute("single-words", language);
public class WordPairsDataAttribute(Language language) : TextFileDataAttribute("word-pairs", language);
public class SentenceDataAttribute(Language language) : TextFileDataAttribute("sentences", language);

