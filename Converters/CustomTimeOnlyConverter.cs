using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension.Converters {
  public class CustomTimeOnlyConverter: JsonConverter<TimeOnly> {
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      var temp = TimeOnly.FromDateTime(reader.GetString().GetSafeValue<DateTime>());
      return temp;
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options) => writer.WriteStringValue(value.AsString());
    
  }
}
