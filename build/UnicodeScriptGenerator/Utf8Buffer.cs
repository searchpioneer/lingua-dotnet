// The MIT License (MIT)
//
// Copyright (c) 2014 Fabien Barbier
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// https://github.com/hexawyz/NetUnicodeInfo/blob/c2ab5227094d8f934b34fe6d3186c7f1e2be5e74/System.Unicode.Build.Core/Utf8Buffer.cs

using System.Collections.Concurrent;
using System.Text;

namespace UnicodeScriptGenerator;

public struct Utf8Buffer : IDisposable
{
	private static readonly ConcurrentStack<byte[]> BufferStack = new();

	public static Utf8Buffer Get() => new(BufferStack.TryPop(out var buffer) ? buffer : new byte[100]);

	private byte[] _buffer;

	private int Length { get; set; }

	private Utf8Buffer(byte[] buffer)
	{
		_buffer = buffer;
		Length = 0;
	}

	public void Dispose()
	{
		if (_buffer != null)
		{
			BufferStack.Push(_buffer);
			this = default;
		}
	}

	private void EnsureExtraCapacity(int count)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(count);
		if (_buffer.Length < checked(Length + count))
			Array.Resize(ref _buffer, Math.Max(Length + count, _buffer.Length << 1));
	}

	public void Append(byte[] value, int startIndex, int count)
	{
		ArgumentNullException.ThrowIfNull(value);
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, value.Length);
		if (checked(count += startIndex) > value.Length)
			throw new ArgumentOutOfRangeException(nameof(count));

		EnsureExtraCapacity(value.Length);

		var buffer = _buffer;

		for (var i = startIndex; i < count; ++i)
			buffer[Length++] = value[i];
	}

	public override string ToString() => Length > 0
		? Encoding.UTF8.GetString(_buffer, 0, Length)
		: string.Empty;

	public string ToTrimmedString()
	{
		if (Length == 0)
			return string.Empty;

		var buffer = _buffer;
		var start = 0;
		var end = Length;

		while (buffer[start] == ' ')
			if (++start == Length)
				return string.Empty;
		while (buffer[--end] == ' ')
		{
		}

		return end > start ? Encoding.UTF8.GetString(buffer, start, end - start + 1) : string.Empty;
	}
}
