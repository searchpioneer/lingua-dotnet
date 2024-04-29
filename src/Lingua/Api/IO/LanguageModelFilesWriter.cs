using System.Text;
using System.Text.Json;
using Lingua.Internal;
using Lingua.Internal.IO;

namespace Lingua.Api.IO;

public class LanguageModelFilesWriter : FilesWriter
{
    /// <summary>
    /// Creates language model files and writes them to a directory.
    /// </summary>
    /// <param name="inputFilePath">The path to a txt file used for language model creation.</param>
    /// <param name="inputFileCharset">The encoding of <see cref="inputFilePath"/>. Defaults to <see cref="Encoding.UTF8"/></param>
    /// <param name="outputDirectoryPath">The directory where the language model files are to be written.</param>
    /// <param name="language">The language for which to create language models.</param>
    /// <param name="charClass"> A regex character class as supported by <see cref="System.Text.RegularExpressions.Regex"/></param>
    public void CreateAndWriteLanguageModelFiles(
        string inputFilePath,
        Encoding? inputFileCharset,
        string outputDirectoryPath,
        Language language,
        string charClass = "\\p{L}"
    )
    {
        CheckInputFilePath(inputFilePath);
        CheckOutputDirectoryPath(outputDirectoryPath);

        inputFileCharset ??= Encoding.UTF8;
        var unigramModel = CreateLanguageModel(
            inputFilePath,
            inputFileCharset,
            language,
            1,
            charClass,
            new Dictionary<Ngram, int>()
        );
        var bigramModel = CreateLanguageModel(
            inputFilePath,
            inputFileCharset,
            language,
            2,
            charClass,
            unigramModel.AbsoluteFrequencies
        );
        var trigramModel = CreateLanguageModel(
            inputFilePath,
            inputFileCharset,
            language,
            3,
            charClass,
            bigramModel.AbsoluteFrequencies
        );
        var quadrigramModel = CreateLanguageModel(
            inputFilePath,
            inputFileCharset,
            language,
            4,
            charClass,
            trigramModel.AbsoluteFrequencies
        );
        var fivegramModel = CreateLanguageModel(
            inputFilePath,
            inputFileCharset,
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
    ) {
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
        if (File.Exists(modelFilePath))
        {
            File.Delete(modelFilePath);
        }
        
        File.WriteAllText(modelFilePath, model.ToJson());
    }
}