using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Lingua.UnicodeScriptGenerator;

/// <summary>
/// Generates the UnicodeScript.g.cs file for character script properties
/// </summary>
internal partial class Program
{
	[GeneratedRegex("^# Scripts-(.*?).txt$")]
	private static partial Regex VersionRegex();

	public static async Task Main(string[] args)
	{
		var fileName = "Scripts.txt";
		if (!File.Exists(fileName))
		{
			using var client = new HttpClient();
			await using var s = await client.GetStreamAsync("https://www.unicode.org/Public/UNIDATA/Scripts.txt");
			await using var fs = new FileStream(fileName, FileMode.CreateNew);
			await s.CopyToAsync(fs);
		}

		var firstLine = File.ReadLines(fileName).First();
		var version = VersionRegex().Match(firstLine).Groups[1].Value;
		var builder = new StringBuilder($@"using System.Globalization;
using static Lingua.UnicodeScript;

namespace Lingua;

///<summary>Unicode script information, version {version}</summary>
public enum UnicodeScript
{{
");

		var unicodeScripts = ReadUnicodeScriptsFromFile(fileName);

		for (var i = 0; i < unicodeScripts.Count; i++)
		{
			var (CodePointRanges, Name) = unicodeScripts[i];

			builder.AppendLine(
				$"    ///<summary>Unicode script for \"{Name.Replace("_", " ")}\"</summary>");
			builder.AppendLine($"    {Name.Replace("_", "")},");
		}

		var orderedScripts = CreateCollapsedOrderedRange(unicodeScripts);

		builder.AppendLine(@"    ///<summary>Unicode script for ""Unknown""</summary>
    Unknown,
}

/// <summary>
/// Extension method for <see cref=""char""/> to determine its <see cref=""UnicodeScript""/> property.
/// </summary>
public static class UnicodeScriptInfo
{
    private static readonly int[] ScriptStarts =
    {");

		for (var i = 0; i < orderedScripts.Count; i++)
		{
			builder.AppendLine(
				$"        0x{orderedScripts[i].CodePointRange.Start.ToString("X4")}, // {orderedScripts[i].CodePointRange.ToString()}; {orderedScripts[i].Name}");
		}

		builder.AppendLine(@"    };

    private static readonly UnicodeScript[] Scripts =
    {");

		for (var i = 0; i < orderedScripts.Count; i++)
		{
			builder.AppendLine(
				$"        {orderedScripts[i].Name.Replace("_", "")},\t// {orderedScripts[i].CodePointRange.ToString()}");
		}

		builder.AppendLine(@"    };

    ///<summary>Gets the Unicode Script for the given character</summary>
    public static UnicodeScript GetScript(this char ch)
    {
		if (!IsValidCodePoint(ch))
			throw new ArgumentException($""Not a valid Unicode code point '{ch}'"");

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
");

		var path = Path.Combine(FindSolutionRoot(), "src", "Lingua", "UnicodeScript.g.cs");
		await File.WriteAllTextAsync(path, builder.ToString());
	}

	private static string FindSolutionRoot()
	{
		var linguaSln = "Lingua.sln";
		var startDir = Directory.GetCurrentDirectory();
		var currentDirectory = new DirectoryInfo(startDir);
		do
		{
			if (File.Exists(Path.Combine(currentDirectory.FullName, linguaSln)))
				return currentDirectory.FullName;

			currentDirectory = currentDirectory.Parent;
		} while (currentDirectory != null);

		throw new InvalidOperationException(
			$"Could not find solution root directory from the current directory {startDir}");
	}

	private static List<(CodePointRange[] CodePointRanges, string Name)> ReadUnicodeScriptsFromFile(
		string fileName)
	{
		using var stream = File.OpenRead(fileName);
		using var reader = new UnicodeDataFileReader(stream, ';');
		var unicodeScripts = new List<(CodePointRange[], string)>();
		var unicodePointRanges = new List<CodePointRange>(100);
		string? name = null;

		while (reader.MoveToNextLine())
		{
			var unicodeCodePointRange = ParseCodepointRange(reader.ReadField()!);
			var currentName = reader.ReadTrimmedField();
			if (name == null || name.Equals(currentName, StringComparison.OrdinalIgnoreCase))
			{
				name = currentName;
				unicodePointRanges.Add(unicodeCodePointRange);
			}
			else
			{
				unicodeScripts.Add((unicodePointRanges.ToArray(), name));
				unicodePointRanges.Clear();
				unicodePointRanges.Add(unicodeCodePointRange);
				name = currentName;
			}
		}

		unicodeScripts.Add((unicodePointRanges.ToArray(), name!));
		return unicodeScripts;
	}

	private static CodePointRange ParseCodepointRange(string range)
	{
		int start, end;
		var rangeSeparatorOffset = range.IndexOf("..", StringComparison.InvariantCulture);
		switch (rangeSeparatorOffset)
		{
			case 0:
				throw new FormatException();
			case < 0:
				start = end = int.Parse(range, NumberStyles.HexNumber);
				break;
			default:
				start = int.Parse(range.AsSpan(0, rangeSeparatorOffset), NumberStyles.HexNumber);
				end = int.Parse(range.AsSpan(rangeSeparatorOffset + 2), NumberStyles.HexNumber);
				break;
		}

		return new(start, end);
	}

	private static List<(CodePointRange CodePointRange, string Name)> CreateCollapsedOrderedRange(
		List<(CodePointRange[] CodePointRanges, string Name)> unicodeScripts)
	{
		List<(CodePointRange CodePointRange, string Name)> orderedScripts = unicodeScripts
			.SelectMany(s => s.CodePointRanges.Select(range => (range, s.Name)))
			.OrderBy(v => v.range.Start)
			.ToList();

		List<(CodePointRange CodePointRange, string Name)> orderedScriptsWithUnknownInsertions =
			new(orderedScripts.Count);
		var currentRangeStart = orderedScripts[0].CodePointRange.Start;
		var currentRangeEnd = orderedScripts[0].CodePointRange.End;
		var currentScriptName = orderedScripts[0].Name;

		for (var i = 1; i < orderedScripts.Count; i++)
		{
			var nextRangeStart = orderedScripts[i].CodePointRange.Start;
			var nextRangeEnd = orderedScripts[i].CodePointRange.End;
			var nextScriptName = orderedScripts[i].Name;

			// If the next range is contiguous or overlapping with the current range and for the same script name
			if (nextRangeStart == currentRangeEnd + 1 && nextScriptName == currentScriptName)
			{
				// collapse the ranges
				if (nextRangeEnd > currentRangeEnd)
					currentRangeEnd = nextRangeEnd;
			}
			else
			{
				// Add the current collapsed range to the result
				orderedScriptsWithUnknownInsertions.Add((new(currentRangeStart, currentRangeEnd), currentScriptName));

				// If there's a gap between the current range's end and the next range's start, add an Unknown range
				if (nextRangeStart > currentRangeEnd + 1)
					orderedScriptsWithUnknownInsertions.Add((new(currentRangeEnd + 1, nextRangeStart - 1), "Unknown"));

				// Update currentRangeStart and currentRangeEnd
				currentRangeStart = nextRangeStart;
				currentRangeEnd = nextRangeEnd;
				currentScriptName = nextScriptName;
			}
		}

		// Add the last collapsed range to the result
		orderedScriptsWithUnknownInsertions
			.Add((new(currentRangeStart, currentRangeEnd), currentScriptName));

		// Add Unknown to cover the remaining range
		orderedScriptsWithUnknownInsertions
			.Add((new(orderedScripts[^1].CodePointRange.End + 1, 0x10FFFF), "Unknown"));

		return orderedScriptsWithUnknownInsertions;
	}
}

public readonly record struct CodePointRange(int Start, int End)
{
	public override string ToString() => Start == End
		? Start.ToString("X4")
		: $"{Start:X4}..{End:X4}";
}
