using System.Text.Json;
using System.Text.Json.Serialization;
using Fractions;

namespace Lingua.Internal;

internal class FractionConverter : JsonConverter<Fraction>
{
	public override Fraction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		Fraction.FromString(reader.GetString()!);

	public override void Write(Utf8JsonWriter writer, Fraction value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.ToString());
}
