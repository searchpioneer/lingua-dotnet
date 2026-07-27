namespace Lingua;

/// <summary>
/// Describes a contiguous single-language text section within a possibly mixed-language text.
/// </summary>
/// <param name="StartIndex">The start index of the identified single-language substring.</param>
/// <param name="EndIndex">The end index (exclusive) of the identified single-language substring.</param>
/// <param name="WordCount">The number of words being part of the identified single-language substring.</param>
/// <param name="Language">The detected language of the identified single-language substring.</param>
public readonly record struct DetectionResult(int StartIndex, int EndIndex, int WordCount, Language Language);
