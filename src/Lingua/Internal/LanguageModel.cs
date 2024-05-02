using System.Text.Json;

namespace Lingua.Internal;

internal static class LanguageModel
{
	public static Dictionary<string, float> FromJson(Stream stream)
	{
		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		var reader = new Utf8JsonReader(memoryStream.ToArray());
		var frequencies = new Dictionary<string, float>();

		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var propertyName = reader.GetString()!;
				switch (propertyName)
				{
					case "language":
						reader.Read();
						break;
					case "ngrams":
						reader.Read();
						while (reader.Read())
						{
							// each ngram is represented as a property in an object of the form {"<fraction>": "<ngram>"}
							if (reader.TokenType != JsonTokenType.PropertyName)
								break;

							var fraction = reader.GetString()!.AsSpan();
							var delimiter = fraction.IndexOf('/');
							var frequency = float.Parse(fraction[..delimiter]) / int.Parse(fraction[(delimiter + 1)..]);
							reader.Read();
							var ngrams = reader.GetString()!.AsSpan();
							foreach (var ngram in ngrams.Split(" "))
							{
								frequencies.Add(ngram.AsSpan().ToString(), frequency);
							}
						}
						break;
					default:
						throw new JsonException($"Unexpected property name '{propertyName}' in language model JSON");
				}
			}
		}

		frequencies.TrimExcess();
		return frequencies;
	}
}

