using System.Diagnostics;
using UnicodeScriptGenerator;

/// <summary>Represents a Unicode script.</summary>
[DebuggerDisplay("[{DebuggerDisplay,nq}] {Name,nq}")]
public readonly struct UnicodeScript
{
    /// <summary>The code point ranges of this script.</summary>
    public readonly UnicodeCodePointRange[] CodePointRanges;
    /// <summary>The name of this script.</summary>
    public readonly string Name;

    private string DebuggerDisplay => "CodePointRanges Count = " + CodePointRanges.Length;

    internal UnicodeScript(UnicodeCodePointRange[] codePointRanges, string name)
    {
        CodePointRanges = codePointRanges;
        Name = name;
    }
}