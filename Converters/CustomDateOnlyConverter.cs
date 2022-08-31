using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension.Converters {
  public class CustomDateOnlyConverter: JsonConverter<DateOnly> {
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      var temp = DateOnly.FromDateTime(reader.GetString().GetSafeValue<DateTime>());
      return temp;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => writer.WriteStringValue(value.AsString());
    
  }
}
