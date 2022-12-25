using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension.Converters {
  public class CustomDateTimeConverter: JsonConverter<DateTime> {
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      if(reader.TokenType == JsonTokenType.String) {
        var temp = reader.GetString().AsJsonDateTime().GetSafeValue<DateTime>();
        return temp;
      }

      return reader.GetDateTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value.AsString());

  }
}
