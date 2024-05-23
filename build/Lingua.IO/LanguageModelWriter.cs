using System.IO.Compression;
using System.Text;
using Lingua.Internal;
using static Lingua.IO.PathValidation;

namespace Lingua.IO;

/// <summary>
/// Creates language model files and writes them to a directory.
/// </summary>
public static class LanguageModelWriter
{
	/// <summary>
	/// Creates language model files and writes them to a directory.
	/// </summary>
	/// <param name="inputFilePath">The path to a txt file used for language model creation.</param>
	/// <param name="encoding">The encoding of <paramref name="inputFilePath"/>. Defaults to <see cref="Encoding.UTF8"/></param>
	/// <param name="outputDirectoryPath">The directory where the language model files are to be written.</param>
	/// <param name="language">The language for which to create language models.</param>
	/// <param name="charClass"> A regex character class as supported by <see cref="System.Text.RegularExpressions.Regex"/></param>
	public static void CreateAndWriteLanguageModelFiles(
		string inputFilePath,
		Encoding? encoding,
		string outputDirectoryPath,
		Language language,
		string charClass = "\\p{L}"
	)
	{
		CheckInputFilePath(inputFilePath);
		CheckOutputDirectoryPath(outputDirectoryPath);

		encoding ??= Encoding.UTF8;
		var unigramModel = CreateLanguageModel(
			inputFilePath,
			encoding,
			language,
			1,
			charClass,
			new Dictionary<Ngram, int>()
		);
		var bigramModel = CreateLanguageModel(
			inputFilePath,
			encoding,
			language,
			2,
			charClass,
			unigramModel.AbsoluteFrequencies
		);
		var trigramModel = CreateLanguageModel(
			inputFilePath,
			encoding,
			language,
			3,
			charClass,
			bigramModel.AbsoluteFrequencies
		);
		var quadrigramModel = CreateLanguageModel(
			inputFilePath,
			encoding,
			language,
			4,
			charClass,
			trigramModel.AbsoluteFrequencies
		);
		var fivegramModel = CreateLanguageModel(
			inputFilePath,
			encoding,
			language,
			5,
			charClass,
			quadrigramModel.AbsoluteFrequencies
		);

		WriteLanguageModel(unigramModel, outputDirectoryPath, "unigrams.json");
		WriteLanguageModel(bigramModel, outputDirectoryPath, "bigrams.json");
		WriteLanguageModel(trigramModel, outputDirectoryPath, "trigrams.json");
		WriteLanguageModel(quadrigramModel, outputDirectoryPath, "quadrigrams.json");
		WriteLanguageModel(fivegramModel, outputDirectoryPath, "fivegrams.json");
	}

	private static TrainingDataLanguageModel CreateLanguageModel(
		string inputFilePath,
		Encoding inputFileCharset,
		Language language,
		int ngramLength,
		string charClass,
		Dictionary<Ngram, int> lowerNgramAbsoluteFrequencies
	)
	{
		var lines = File.ReadLines(inputFilePath, inputFileCharset);
		return TrainingDataLanguageModel.FromText(
			lines,
			language,
			ngramLength,
			charClass,
			lowerNgramAbsoluteFrequencies
		);
	}

	private static void WriteLanguageModel(
		TrainingDataLanguageModel model,
		string outputDirectoryPath,
		string fileName
	)
	{
		var modelFilePath = Path.Combine(outputDirectoryPath, fileName);

		File.WriteAllText(modelFilePath, model.ToJson());

		using var readStream = File.OpenRead(modelFilePath);
		using var writeStream = File.OpenWrite(modelFilePath + ".br");
		using var brotliStream = new BrotliStream(writeStream, CompressionMode.Compress);
		readStream.CopyTo(brotliStream);
	}
}
