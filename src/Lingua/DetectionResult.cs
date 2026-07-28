namespace Lingua;

/// <summary>
/// Describes a contiguous single-language text section within a possibly mixed-language text.
/// </summary>
/// <param name="Range">
/// The range of the identified single-language substring within the input text. The range is
/// always relative to the start of the text and its end is exclusive, so it can be used to slice
/// the input directly, for example <c>text[result.Range]</c>. Indices are UTF-16 code unit
/// offsets, consistent with <see cref="string"/> indexing.
/// </param>
/// <param name="WordCount">The number of words being part of the identified single-language substring.</param>
/// <param name="Language">The detected language of the identified single-language substring.</param>
public readonly record struct DetectionResult(Range Range, int WordCount, Language Language);
