using System.CommandLine;
using System.Text;
using Build;
using Bullseye;
using static BuildTargets;
using static Bullseye.Targets;
using static SimpleExec.Command;

const string packOutput = "nuget";
const string reportOutput = "accuracy-reports";

var language = new Option<string[]>(["--language"], "languages to generate an accuracy report for")
{
	Arity = ArgumentArity.ZeroOrMore,
};

var detector = new Option<string[]>(["--implementation"], "implementations to generate an accuracy report for")
{
	Arity = ArgumentArity.ZeroOrMore,
};
detector.FromAmong("Lingua", "NTextCat", "LanguageDetection");

var compare = new Option<bool>("--compare",
	"whether implementations should use the same subset of supported languages when generating an accuracy report");

var cmd = new RootCommand
{
	language,
	detector,
	compare,
	new Argument<string[]>("targets")
	{
		Description =
			"A list of targets to run or list. If not specified, the \"default\" target will be run, or all targets will be listed.",
	}
};

foreach (var (aliases, description) in Options.Definitions)
	cmd.Add(new Option<bool>(aliases.ToArray(), description));

cmd.SetHandler(async () =>
{
	var cmdLine = cmd.Parse(args);
	var tokens = cmdLine.CommandResult.Tokens.Select(token => token.Value).ToList();
	var targets = new List<string>();
	var seen = false;
	var targetOptions = new List<string>();
	foreach (var t in tokens)
	{
		if (seen)
		{
			targetOptions.Add(t);
			continue;
		}

		if (t.StartsWith("-"))
		{
			seen = true;
			targetOptions.Add(t);
		}
		else
			targets.Add(t);
	}

	var options = new Options(Options.Definitions.Select(d => (d.Aliases[0],
		cmdLine.GetValueForOption(cmd.Options.OfType<Option<bool>>().Single(o => o.HasAlias(d.Aliases[0]))))));

	Target(Restore, () =>
	{
		Run("dotnet", "restore");
		Run("dotnet", "tool restore");
	});

	Target(CleanBuildOutput, DependsOn(Restore), () =>
	{
		Run("dotnet", "clean -c Release -v m --nologo");
	});

	Target(Format, DependsOn(Restore), () =>
	{
		Run("dotnet", "format");
	});

	Target(BuildSln, DependsOn(CleanBuildOutput, Format), () =>
	{
		Run("dotnet", "build -c Release --nologo");
	});

	Target(Test, DependsOn(BuildSln), () =>
	{
		Run("dotnet", "test tests/Lingua.Tests -c Release --no-build --verbosity normal");
	});

	Target(Report, DependsOn(CleanReportOutput, BuildSln), () =>
	{
		var filter = new StringBuilder();
		var languages = cmdLine.GetValueForOption(language);
		if (languages?.Length > 0)
		{
			foreach (var l in languages)
			{
				if (filter.Length > 0)
					filter.Append(" & ");

				filter.Append("(FullyQualifiedName~");
				filter.Append(l);
				filter.Append(')');
			}
		}

		var detectors = cmdLine.GetValueForOption(detector);
		if (detectors?.Length > 0)
		{
			if (filter.Length > 0)
				filter.Append(" & ");

			filter.Append('(');

			for (var index = 0; index < detectors.Length; index++)
			{
				if (index > 0)
					filter.Append(" | ");

				var d = detectors[index];
				filter.Append("(FullyQualifiedName~.");
				filter.Append(d);
				filter.Append(')');
			}

			filter.Append(')');
		}

		var additionalArgs = new StringBuilder();
		if (cmdLine.GetValueForOption(compare))
			additionalArgs.Append(" --environment TEST_COMPARE=\"true\"");
		if (filter.Length > 0)
			additionalArgs.Append($" --filter \"{filter}\" --environment TEST_FILTER=\"{filter}\"");

		Run("dotnet", $"test tests/Lingua.AccuracyReport.Tests -c Release{additionalArgs}");

		CombinedAccuracyReport.Create();
	});

	Target(Benchmark, () =>
	{
		if (targetOptions.Count != 0)
			Run("dotnet", "run --project tests/Lingua.Benchmarks -c Release " + string.Join(' ', targetOptions));
		else
			Run("dotnet", "run --project tests/Lingua.Benchmarks -c Release");
	});

	Target(CleanReportOutput, () =>
	{
		if (Directory.Exists(reportOutput))
			Directory.Delete(reportOutput, true);
	});

	Target(CleanPackOutput, () =>
	{
		if (Directory.Exists(packOutput))
			Directory.Delete(packOutput, true);
	});

	Target(Clean, DependsOn(CleanBuildOutput, CleanReportOutput, CleanPackOutput));

	Target(Pack, DependsOn(Clean, Format), () =>
	{
		var outputDir = Directory.CreateDirectory(packOutput);
		Run("dotnet", $"pack -c Release -o \"{outputDir.FullName}\" --nologo");
	});

	Target(Default, DependsOn(Test));

	await RunTargetsAndExitAsync(targets, options, messageOnly: ex => ex is SimpleExec.ExitCodeException);
});

return await cmd.InvokeAsync(args);

internal static class BuildTargets
{
	public const string CleanBuildOutput = "clean-build-output";
	public const string CleanPackOutput = "clean-pack-output";
	public const string CleanReportOutput = "clean-report-output";
	public const string Clean = "clean";
	public const string BuildSln = "build";
	public const string Test = "test";
	public const string Default = "default";
	public const string Restore = "restore";
	public const string Format = "format";
	public const string Pack = "pack";
	public const string Report = "report";
	public const string Benchmark = "benchmark";
}
