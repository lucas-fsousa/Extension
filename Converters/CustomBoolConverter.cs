using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension.Converters {
  public class CustomBoolConverter: JsonConverter<bool> {
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.True:
          return true;
        case JsonTokenType.False:
          return false;
        case JsonTokenType.Number:
          if(reader.GetSByte() > 1)
            throw new JsonException();

          return reader.GetSByte() == 1;
        case JsonTokenType.String:
          return reader.GetString() switch {
            "true" => true,
            "false" => false,
            "True" => true,
            "False" => false,
            _ => throw new JsonException()
          };
        default:
          throw new JsonException();
      }
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
    
  }

}
