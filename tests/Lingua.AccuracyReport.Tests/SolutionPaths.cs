namespace Lingua.AccuracyReport.Tests;

public class SolutionPaths
{
	private static readonly Lazy<string> LazyRoot = new(FindSolutionRoot);
	public static string Root => LazyRoot.Value;

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
}
