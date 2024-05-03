namespace Build;

public static class CombinedAccuracyReport
{
	public static void Create()
	{
		using var combinedResportStream = File.Create(Path.Combine("accuracy-reports", "report.md"));
		using var writer = new StreamWriter(combinedResportStream);
		foreach (var implementationDir in Directory.GetDirectories("accuracy-reports")
					 .OrderBy(v => v, new LinguaReportFirstComparer()))
		{
			var name = Path.GetFileName(implementationDir);
			writer.WriteLine($"# {name} Accuracy report");
			writer.WriteLine();
			writer.Flush();
			foreach (var report in Directory.GetFiles(implementationDir))
			{
				using var reportStream = File.OpenRead(report);
				reportStream.CopyTo(combinedResportStream);
			}
			writer.WriteLine();
			writer.Flush();
		}
	}
	private class LinguaReportFirstComparer : IComparer<string>
	{
		private const string LinguaReports = "Lingua";

		public int Compare(string? x, string? y)
		{
			if (Path.GetFileName(x) == LinguaReports)
				return 1;

			return Path.GetFileName(y) == LinguaReports ? 1 : string.CompareOrdinal(x, y);
		}
	}
}
