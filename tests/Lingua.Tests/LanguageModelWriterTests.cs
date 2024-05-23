using System.IO.Compression;
using System.Text;
using FluentAssertions;
using Lingua.IO;
using Xunit;

namespace Lingua.Tests;

public class LanguageModelWriterTests : IDisposable
{
	private static readonly string Text =
		"""
		These sentences are intended for testing purposes.
		Do not use them in production!
		By the way, they consist of 23 words in total.
		""".ToLowerInvariant();

	private static readonly string ExpectedUnigramLanguageModel =
		"""
		{
		    "language":"ENGLISH",
		    "ngrams":{
		        "13/100":"t",
		        "1/25":"h",
		        "7/50":"e",
		        "1/10":"s n o",
		        "3/100":"c a p u y",
		        "1/20":"r d",
		        "3/50":"i",
		        "1/50":"f w",
		        "1/100":"g m b l"
		    }
		}
		""".Minify();

	private static readonly string ExpectedBigramLanguageModel =
		"""
        {
            "language":"ENGLISH",
            "ngrams":{
                "4/13":"th",
                "1/1":"he by",
                "2/7":"es",
                "2/5":"se",
                "3/14":"en",
                "1/5":"nt re de or st rp do ot ro du on rd ds",
                "3/13":"te",
                "1/10":"nc nd ng os no od ns si of",
                "1/3":"ce ar pu ur po us pr uc ct ay co al",
                "2/3":"in",
                "1/14":"ed em ey",
                "1/2":"fo wa wo",
                "2/13":"ti",
                "1/6":"io is",
                "1/13":"to ta"
            }
        }
        """.Minify();

	private static readonly string ExpectedTrigramLanguageModel =
		"""
        {
            "language":"ENGLISH",
            "ngrams":{
                "1/1":"the nte nce ces are nde ded for pur urp rpo pos ose not use pro rod odu duc uct cti ion way con nsi sis ist wor rds tot tal",
                "1/4":"hes ese sen int est ing ses hem hey",
                "1/3":"ent enc end tes",
                "2/3":"ten",
                "1/2":"sti tin tio ons ord ota"
            }
        }
        """.Minify();

	private static readonly string ExpectedQuadrigramLanguageModel =
		"""
        {
            "language":"ENGLISH",
            "ngrams":{
                "1/4":"thes them they",
                "1/1":"hese sent ente nten ence nces inte ende nded test esti stin ting purp urpo rpos pose oses prod rodu oduc duct ucti ctio tion cons onsi nsis sist word ords tota otal",
                "1/2":"tenc tend"
            }
        }
        """.Minify();

	private static readonly string ExpectedFivegramLanguageModel =
		"""
        {
            "language":"ENGLISH",
            "ngrams":{
                "1/1":"these sente enten tence ences inten tende ended testi estin sting purpo urpos rpose poses produ roduc oduct ducti uctio ction consi onsis nsist words total",
                "1/2":"ntenc ntend"
            }
        }
        """.Minify();

	private readonly string _inputFilePath;

	public LanguageModelWriterTests()
	{
		_inputFilePath = Path.GetTempFileName();
		File.WriteAllText(_inputFilePath, Text);
	}

	public void Dispose() => File.Delete(_inputFilePath);

	[Fact]
	public void CreateAndWriteLanguageModelFiles()
	{
		var outputDirectoryPath = Directory.CreateTempSubdirectory();

		LanguageModelWriter.CreateAndWriteLanguageModelFiles(
			_inputFilePath,
			Encoding.UTF8,
			outputDirectoryPath.FullName,
			Language.English
		);

		var modelFilePaths = RetrieveAndSortModelFiles(outputDirectoryPath);

		modelFilePaths.Should().HaveCount(10);

		var bigrams = modelFilePaths[0];
		var fivegrams = modelFilePaths[1];
		var quadrigrams = modelFilePaths[2];
		var trigrams = modelFilePaths[3];
		var unigrams = modelFilePaths[4];

		TestModelFile(unigrams, "unigrams.json", ExpectedUnigramLanguageModel);
		TestModelFile(bigrams, "bigrams.json", ExpectedBigramLanguageModel);
		TestModelFile(trigrams, "trigrams.json", ExpectedTrigramLanguageModel);
		TestModelFile(quadrigrams, "quadrigrams.json", ExpectedQuadrigramLanguageModel);
		TestModelFile(fivegrams, "fivegrams.json", ExpectedFivegramLanguageModel);
	}

	[Fact]
	public void RelativeInputPathThrows() =>
		Assert.Throws<ArgumentException>(() => LanguageModelWriter.CreateAndWriteLanguageModelFiles(
			"some/relative/path/file.txt",
			Encoding.UTF8,
			"/some/output/directory",
			Language.English
		));

	[Fact]
	public void NonExistentFileThrows() =>
		Assert.Throws<FileNotFoundException>(() => LanguageModelWriter.CreateAndWriteLanguageModelFiles(
			"/some/non-existing/path/file.txt",
			Encoding.UTF8,
			"/some/output/directory",
			Language.English
		));

	[Fact]
	public void DirectoryAsInputPathThrows() =>
		Assert.Throws<FileNotFoundException>(() => LanguageModelWriter.CreateAndWriteLanguageModelFiles(
			Directory.CreateTempSubdirectory().FullName,
			Encoding.UTF8,
			"/some/output/directory",
			Language.English
		));

	private static void TestModelFile(FileSystemInfo modelFilePath, string expectedFileName, string expectedModelContent, bool compressed = false)
	{
		modelFilePath.Name.Should().Be(expectedFileName);
		File.ReadAllText(modelFilePath.FullName).Should().Be(expectedModelContent);

		var compressedFilePath = modelFilePath.FullName + ".br";
		using var fs = File.OpenRead(compressedFilePath);
		using var brotliStream = new BrotliStream(fs, CompressionMode.Decompress);
		using var streamReader = new StreamReader(brotliStream);

		var compressedContent = streamReader.ReadToEnd();
		compressedContent.Should().Be(expectedModelContent);
	}

	private static List<FileInfo> RetrieveAndSortModelFiles(DirectoryInfo outputDirectoryPath) =>
		outputDirectoryPath.GetFiles()
			.OrderByDescending(f => f.Extension == ".json")
			.ThenBy(f => f.Name)
			.ToList();

}
