namespace Lingua.IO;

internal static class PathValidation
{
	public static void CheckInputFilePath(string inputFilePath)
	{
		if (!Path.IsPathRooted(inputFilePath))
			throw new ArgumentException($"Input file path '{inputFilePath}' is not absolute");

		if (!File.Exists(inputFilePath))
			throw new FileNotFoundException($"Input file '{inputFilePath}' does not exist");

		var attributes = File.GetAttributes(inputFilePath);

		if (attributes.HasFlag(FileAttributes.Directory) || attributes.HasFlag(FileAttributes.Hidden) || attributes.HasFlag(FileAttributes.System))
			throw new ArgumentException($"Input file path '{inputFilePath}' does not represent a regular file");
	}

	public static void CheckOutputDirectoryPath(string outputDirectoryPath)
	{
		if (!Path.IsPathRooted(outputDirectoryPath))
			throw new ArgumentException($"Output directory path '{outputDirectoryPath}' is not absolute");

		if (!Directory.Exists(outputDirectoryPath))
			throw new DirectoryNotFoundException($"Output directory '{outputDirectoryPath}' does not exist");
	}
}
