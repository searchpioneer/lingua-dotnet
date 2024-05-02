using System.CommandLine;
using Bullseye;
using static BuildTargets;
using static Bullseye.Targets;
using static SimpleExec.Command;

const string packOutput = "nuget";
const string reportOutput = "accuracy-reports";

var cmd = new RootCommand
{
	new Argument<string[]>("targets")
	{
		Description =
			"A list of targets to run or list. If not specified, the \"default\" target will be run, or all targets will be listed.",
	},
};

foreach (var (aliases, description) in Options.Definitions)
	cmd.Add(new Option<bool>(aliases.ToArray(), description));

cmd.SetHandler(async () =>
{
	var cmdLine = cmd.Parse(args);
	var targets = cmdLine.CommandResult.Tokens.Select(token => token.Value);
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

	Target(Build, DependsOn(CleanBuildOutput, Format), () =>
	{
		Run("dotnet", "build -c Release --nologo");
	});

	Target(Test, DependsOn(Build), () =>
	{
		Run("dotnet", "test tests/Lingua.Tests -c Release --no-build  --verbosity normal");
	});

	Target(Report, DependsOn(CleanReportOutput, Build), () =>
	{
		Run("dotnet", "test tests/Lingua.AccuracyReport.Tests -c Release --no-build");
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

	Target(Pack, DependsOn(Build, CleanPackOutput), () =>
	{
		var outputDir = Directory.CreateDirectory(packOutput);
		Run("dotnet", $"pack -c Release -o \"{outputDir.FullName}\" --no-build --nologo");
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
	public const string Build = "build";
	public const string Test = "test";
	public const string Default = "default";
	public const string Restore = "restore";
	public const string Format = "format";
	public const string Pack = "pack";
	public const string Report = "report";
}
