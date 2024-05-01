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
// https://github.com/hexawyz/NetUnicodeInfo/blob/c2ab5227094d8f934b34fe6d3186c7f1e2be5e74/System.Unicode.Build.Core/UnicodeDataFileReader.cs#L5

namespace UnicodeScriptGenerator;

public class UnicodeDataFileReader(Stream stream, char fieldSeparator, bool leaveOpen = false) : IDisposable
{
	private readonly byte[] _byteBuffer = new byte[8192];
	private int _index;
	private int _length;
	private bool _hasField;

	public void Dispose()
	{
		if (!leaveOpen)
			stream.Dispose();
	}

	private bool RefillBuffer()
		// Evilish line of code. 😈
		=> (_length = stream.Read(_byteBuffer, 0, _byteBuffer.Length)) != (_index = 0);

	private static bool IsNewLineOrComment(byte b)
		=> b == '\n' || b == '#';

	/// <summary>Moves the stream to the next valid data row.</summary>
	/// <returns><see langword="true"/> if data is available; <see langword="false"/> otherwise.</returns>
	public bool MoveToNextLine()
	{
		if (_length == 0)
		{
			if (RefillBuffer())
			{
				if (!IsNewLineOrComment(_byteBuffer[_index]))
				{
					_hasField = true;
					goto Completed;
				}
			}
			else
				return false;
		}

		do
			while (_index < _length)
			{
				if (_byteBuffer[_index++] == '\n')
				{
					if ((_index < _length || RefillBuffer()) && !IsNewLineOrComment(_byteBuffer[_index]))
					{
						_hasField = true;
						goto Completed;
					}
				}
			}
		while (RefillBuffer());

		_hasField = false;
	Completed:
		;
		return _hasField;
	}

	private string? ReadFieldInternal(bool trim)
	{
		if (_length == 0)
			throw new InvalidOperationException();

		if (!_hasField)
			return null;

		if (_index >= _length)
			RefillBuffer();

		// If the current character is a new line or a comment, we are at the end of a line.
		if (IsNewLineOrComment(_byteBuffer[_index]))
		{
			if (_hasField)
			{
				_hasField = false;
				return string.Empty;
			}

			return null;
		}

		using var buffer = Utf8Buffer.Get();
		do
		{
			var startOffset = _index;
			var endOffset = -1;

			while (_index < _length)
			{
				var b = _byteBuffer[_index];

				if (IsNewLineOrComment(b))   // NB: Do not advance to the next byte when end of line has been reached.
				{
					endOffset = _index;
					_hasField = false;
					break;
				}

				if (b == fieldSeparator)
				{
					endOffset = _index++;
					break;
				}

				++_index;
			}

			if (endOffset >= 0)
			{
				buffer.Append(_byteBuffer, startOffset, endOffset - startOffset);
				break;
			}

			if (_index > startOffset)
				buffer.Append(_byteBuffer, startOffset, _index - startOffset);
		} while (RefillBuffer());

		return trim ? buffer.ToTrimmedString() : buffer.ToString();
	}

	/// <summary>Reads the next data field.</summary>
	/// <remarks>This method will return <see langword="null"/> on end of line.</remarks>
	/// <returns>The text value of the read field, if available; <see langword="null"/> otherwise.</returns>
	public string? ReadField() => ReadFieldInternal(false);

	/// <summary>Reads the next data field as a trimmed value.</summary>
	/// <remarks>This method will return <see langword="null"/> on end of line.</remarks>
	/// <returns>The trimmed text value of the read field, if available; <see langword="null"/> otherwise.</returns>
	public string? ReadTrimmedField() => ReadFieldInternal(true);
}
