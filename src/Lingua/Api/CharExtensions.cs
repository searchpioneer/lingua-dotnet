namespace Lingua.Api;

public static class CharExtensions
{
    private static readonly HashSet<Alphabet> ScriptsWithLogograms = LanguageExtensions.LanguagesSupportingLogograms
        .SelectMany(l => l.Alphabets())
        .ToHashSet();
    
    public static bool IsLogogram(this char ch) => 
        !char.IsWhiteSpace(ch) && ScriptsWithLogograms.Any(a => a.Matches(ch));
}