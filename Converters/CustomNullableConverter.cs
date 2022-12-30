using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension.Converters {
  public class CustomNullableIntConverter: JsonConverter<int?> {
    public override bool HandleNull => true;

    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetInt32();

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableUIntConverter: JsonConverter<uint?> {
    public override bool HandleNull => true;
    
    public override uint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetUInt32();

    public override void Write(Utf8JsonWriter writer, uint? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableDoubleConverter: JsonConverter<double?> {
    public override bool HandleNull => true;

    public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetDouble();

    public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableFloatConverter: JsonConverter<float?> {
    public override bool HandleNull => true;

    public override float? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetSingle();

    public override void Write(Utf8JsonWriter writer, float? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableDecimalConverter: JsonConverter<decimal?> {
    public override bool HandleNull => true;
    public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetDecimal();

    public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableByteConverter: JsonConverter<byte?> {
    public override bool HandleNull => true;
    
    public override byte? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetByte();

    public override void Write(Utf8JsonWriter writer, byte? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableSByteConverter: JsonConverter<sbyte?> {
    public override bool HandleNull => true;
    
    public override sbyte? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetSByte();

    public override void Write(Utf8JsonWriter writer, sbyte? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableLongConverter: JsonConverter<long?> {
    public override bool HandleNull => true;
    
    public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetInt64();

    public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableULongConverter: JsonConverter<ulong?> {
    public override bool HandleNull => true;

    public override ulong? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetUInt64();

    public override void Write(Utf8JsonWriter writer, ulong? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableShortConverter: JsonConverter<short?> {
    public override bool HandleNull => true;
    
    public override short? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetInt16();

    public override void Write(Utf8JsonWriter writer, short? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableUShortConverter: JsonConverter<ushort?> {
    public override bool HandleNull => true;

    public override ushort? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetUInt16();
    public override void Write(Utf8JsonWriter writer, ushort? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableBoolConverter: JsonConverter<bool?> {
    public override bool HandleNull => true;

    public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {

      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.None:
          return null;
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
            "Null" => null,
            "null" => null,
            "none" => null,
            "None" => null,
            _ => throw new JsonException()
          };
        default:
          throw new JsonException();
      }
    }

    public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteBooleanValue(value.Value);
      else
        writer.WriteNullValue();
    }

  }

}
