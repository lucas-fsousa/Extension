using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension.Converters {
  public class CustomNullableIntConverter: JsonConverter<int?> {
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue? reader.GetInt32() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableUIntConverter: JsonConverter<uint?> {
    public override uint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetUInt32() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, uint? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableDoubleConverter: JsonConverter<double?> {
    public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetDouble() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableFloatConverter: JsonConverter<float?> {
    public override float? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetSingle() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, float? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableDecimalConverter: JsonConverter<decimal?> {
    public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetDecimal(): null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableByteConverter: JsonConverter<byte?> {
    public override byte? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetByte() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, byte? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableSByteConverter: JsonConverter<sbyte?> {
    public override sbyte? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetSByte() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, sbyte? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableLongConverter: JsonConverter<long?> {
    public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetInt64() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableULongConverter: JsonConverter<ulong?> {
    public override ulong? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetUInt64() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, ulong? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableShortConverter: JsonConverter<short?> {
    public override short? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetInt16() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, short? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableUShortConverter: JsonConverter<ushort?> {
    public override ushort? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch(reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.Number:
          bool hasValidValue = reader.GetString().IsFilled();
          return hasValidValue ? reader.GetUInt16() : null;
        case JsonTokenType.None:
          return null;
        default:
          throw new JsonException();
      }
    }


    public override void Write(Utf8JsonWriter writer, ushort? value, JsonSerializerOptions options) {
      if(value.HasValue)
        writer.WriteNumberValue(value.Value);
      else
        writer.WriteNullValue();
    }
  }

  public class CustomNullableBoolConverter: JsonConverter<bool?> {
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
