using System.Text.RegularExpressions;

namespace Lingua.Tests;

public static partial class StringExtensions
{
	[GeneratedRegex("\r?\n\\s*")]
	private static partial Regex MinifyRegex();

	public static string Minify(this string value) => MinifyRegex().Replace(value, string.Empty);
}
