using System.Runtime.CompilerServices;

namespace Lingua.Internal;

internal readonly ref struct SpanSplitter<T>
	where T : IEquatable<T>
{
	private readonly ReadOnlySpan<T> _source;
	private readonly ReadOnlySpan<T> _separator;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SpanSplitter(ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
	{
		if (separator.Length == 0)
			throw new ArgumentException("Requires non-empty value", nameof(separator));

		_source = source;
		_separator = separator;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SpanSplitEnumerator<T> GetEnumerator() => new(_source, _separator);
}

internal ref struct SpanSplitEnumerator<T>
	where T : IEquatable<T>
{
	private int _nextStartIndex = 0;
	private readonly ReadOnlySpan<T> _separator;
	private readonly ReadOnlySpan<T> _source;
	private SpanSplitValue _current;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SpanSplitEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
	{
		if (separator.Length == 0)
			throw new ArgumentException("Requires non-empty value", nameof(separator));

		_source = source;
		_separator = separator;
	}

	public bool MoveNext()
	{
		if (_nextStartIndex > _source.Length)
			return false;

		var nextSource = _source[_nextStartIndex..];
		var foundIndex = nextSource.IndexOf(_separator);
		var length = -1 < foundIndex
			? foundIndex
			: nextSource.Length;

		_current = new SpanSplitValue { StartIndex = _nextStartIndex, Length = length, Source = _source, };
		_nextStartIndex += _separator.Length + _current.Length;

		return true;
	}

	public SpanSplitValue Current
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _current;
	}

	public readonly ref struct SpanSplitValue
	{
		public int StartIndex { get; init; }
		public int Length { get; init; }
		public ReadOnlySpan<T> Source { get; init; }

		public ReadOnlySpan<T> AsSpan() => Source.Slice(StartIndex, Length);

		public static implicit operator ReadOnlySpan<T>(SpanSplitValue value)
			=> value.AsSpan();
	}
}

internal static class ExtensionMethods
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static SpanSplitter<T> Split<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
		where T : IEquatable<T> =>
		new(source, separator);
}
