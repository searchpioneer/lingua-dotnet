using System.Text;
using System.Text.RegularExpressions;
using UnicodeScriptGenerator;

var fileName = "Scripts.txt";
if (!File.Exists(fileName))
{
    using var client = new HttpClient();
    await using var s = await client.GetStreamAsync("https://www.unicode.org/Public/UNIDATA/Scripts.txt");
    await using var fs = new FileStream(fileName, FileMode.CreateNew);
    await s.CopyToAsync(fs);
}

var firstLine = File.ReadLines(fileName).First();
var regex = new Regex("^# Scripts-(.*?).txt$");
var version = regex.Match(firstLine).Groups[1].Value;
var builder = new StringBuilder($@"namespace Lingua;

///<summary>Unicode script information, version {version}</summary>
public enum UnicodeScript
{{
");

var unicodeScripts = ReadUnicodeScriptsFromFile(fileName);
var startsWithNames = new List<(UnicodeCodePointRange, string)>();
for (var i = 0; i < unicodeScripts.Count; i++)
{
    var unicodeScript = unicodeScripts[i];

    builder.AppendLine($"    ///<summary>Unicode script for \"{unicodeScript.Name.Replace("_", " ")}\"</summary>");
    builder.AppendLine($"    {unicodeScript.Name.Replace("_", "")},");

    for (var j = 0; j < unicodeScript.CodePointRanges.Length; j++)
    {
        startsWithNames.Add((unicodeScript.CodePointRanges[j], unicodeScript.Name));
    }
}

var orderedScripts =
    startsWithNames.OrderBy(kv => kv.Item1.FirstCodePoint).ToList();

var r = new List<(UnicodeCodePointRange, string)>();
var currentRangeStart = orderedScripts[0].Item1.FirstCodePoint;
var currentRangeEnd = orderedScripts[0].Item1.LastCodePoint;
var currentScriptName = orderedScripts[0].Item2;

for (var i = 1; i < orderedScripts.Count; i++)
{
    var nextRangeStart = orderedScripts[i].Item1.FirstCodePoint;
    var nextRangeEnd = orderedScripts[i].Item1.LastCodePoint;
    var nextScriptName = orderedScripts[i].Item2;

    // If the next range is contiguous or overlapping with the current range
    if (nextRangeStart == currentRangeEnd + 1 && nextScriptName == currentScriptName)
    {
        // Update the end of the current range if necessary
        if (nextRangeEnd > currentRangeEnd)
            currentRangeEnd = nextRangeEnd;
    }
    else
    {
        // Add the current collapsed range to the result
        r.Add((new UnicodeCodePointRange(currentRangeStart, currentRangeEnd), currentScriptName));

        // If there's a gap between the current range's end and the next range's start, add the missing range
        if (nextRangeStart > currentRangeEnd + 1)
            r.Add((new UnicodeCodePointRange(currentRangeEnd + 1, nextRangeStart - 1), "Unknown"));

        // Update currentRangeStart and currentRangeEnd
        currentRangeStart = nextRangeStart;
        currentRangeEnd = nextRangeEnd;
        currentScriptName = nextScriptName;
    }
}

// Add the last collapsed range to the result
r.Add((new UnicodeCodePointRange(currentRangeStart, currentRangeEnd), currentScriptName));

orderedScripts = r;
orderedScripts.Add((new UnicodeCodePointRange(orderedScripts[^1].Item1.LastCodePoint + 1, 0x10FFFF), "Unknown"));

builder.AppendLine(@"    ///<summary>Unicode script for ""Unknown""</summary>
    Unknown,
}

public static class CharExtensions
{
    private static readonly int[] ScriptStarts =
    {");

for (var i = 0; i < orderedScripts.Count; i++)
{
    builder.AppendLine($"        0x{orderedScripts[i].Item1.FirstCodePoint.ToString("X4")}, // {orderedScripts[i].Item1.FirstCodePoint.ToString("X4")}..{orderedScripts[i].Item1.LastCodePoint.ToString("X4")}; {orderedScripts[i].Item2}");
}

builder.AppendLine(@"    };

    private static readonly UnicodeScript[] Scripts =
    {");

for (var i = 0; i < orderedScripts.Count; i++)
{
    builder.AppendLine($"        UnicodeScript.{orderedScripts[i].Item2.Replace("_", "")},\t// {orderedScripts[i].Item1.FirstCodePoint.ToString("X4")}..{orderedScripts[i].Item1.LastCodePoint.ToString("X4")}");
}

builder.AppendLine(@"    };

    ///<summary>Gets the Unicode Script for the given character</summary>
    public static UnicodeScript GetScript(this char ch)
    {
        var index = Array.BinarySearch(ScriptStarts, (int)ch);
        if (index < 0)
        {
            index = -index - 2;
        }

        return Scripts[index];
    }
}
");

var path = Path.Combine(FindSolutionRoot(), "src", "Lingua", "UnicodeScript.g.cs");

File.WriteAllText(path, builder.ToString());

static string FindSolutionRoot()
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

static List<UnicodeScript> ReadUnicodeScriptsFromFile(string fileName)
{
    using var stream = File.OpenRead(fileName);
    using var reader = new UnicodeDataFileReader(stream, ';');
    var unicodeScripts = new List<UnicodeScript>();
    var unicodePointRanges = new List<UnicodeCodePointRange>(100);
    string? name = null;

    while (reader.MoveToNextLine())
    {
        var unicodeCodePointRange = UnicodeCodePointRange.Parse(reader.ReadField());
        var currentName = reader.ReadTrimmedField();

        if (name == null || name.Equals(currentName, StringComparison.OrdinalIgnoreCase))
        {
            name = currentName;
            unicodePointRanges.Add(unicodeCodePointRange);
        }
        else
        {
            unicodeScripts.Add(new UnicodeScript(unicodePointRanges.ToArray(), name));
            unicodePointRanges.Clear();
            unicodePointRanges.Add(unicodeCodePointRange);
            name = currentName;
        }
    }

    unicodeScripts.Add(new UnicodeScript(unicodePointRanges.ToArray(), name!));
    return unicodeScripts;
}
