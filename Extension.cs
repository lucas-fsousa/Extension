using PublicUtility.Extension.Converters;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension {
  public static partial class Extends {

    #region PRIVATES
    private static string ReplaceStrParams(string input, params object[] args) {
      int i = 0;

      foreach(var arg in args) {
        input = input.Replace($"{{{i}}}", $"{arg}");
        i++;
      }
      return input;
    }

    private static JsonSerializerOptions GetJsonSerializerOptions(bool ident = false) {
      var jsonOptions = new JsonSerializerOptions() {
        PropertyNameCaseInsensitive = true,
        WriteIndented = ident,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters = {
          new CustomBoolConverter(),

          new CustomDateOnlyConverter(),
          new CustomTimeOnlyConverter(),
          new CustomDateTimeConverter(),

          new CustomNullableBoolConverter(),
          new CustomNullableIntConverter(),
          new CustomNullableUIntConverter(),
          new CustomNullableLongConverter(),
          new CustomNullableULongConverter(),
          new CustomNullableFloatConverter(),
          new CustomNullableDoubleConverter(),
          new CustomNullableDecimalConverter(),
          new CustomNullableByteConverter(),
          new CustomNullableSByteConverter(),
          new CustomNullableShortConverter(),
          new CustomNullableUShortConverter(),
        }
      };
      return jsonOptions;
    }

    private static T ConvertToNumber<T>(object obj, int precision = 0) {
      T response = default;

      if(obj.ToString().IsNumber()) {
        string value = obj.ToString();
        var splitChar = value.Split(',').Length > 0 ? ',' : '.';

        if(typeof(T) == typeof(decimal)) {
          decimal dc = decimal.Round(decimal.Parse(value, System.Globalization.NumberStyles.Currency), precision);
          response = (T)Convert.ChangeType(dc, typeof(T));

        } else if(typeof(T) == typeof(float)) {
          float ft = float.Parse(value, System.Globalization.NumberStyles.Float);
          response = (T)Convert.ChangeType(ft, typeof(T));

        } else if(typeof(T) == typeof(double)) {
          double db = double.Parse(value, System.Globalization.NumberStyles.Any);
          response = (T)Convert.ChangeType(db, typeof(T));

        } else if(typeof(T) == typeof(short)) {
          short st = short.Parse(value.Split(splitChar)[0], System.Globalization.NumberStyles.Integer);
          response = (T)Convert.ChangeType(st, typeof(T));

        } else if(typeof(T) == typeof(int)) {
          int it = int.Parse(value.Split(splitChar)[0], System.Globalization.NumberStyles.Integer);
          response = (T)Convert.ChangeType(it, typeof(T));

        } else if(typeof(T) == typeof(long)) {
          long lg = long.Parse(value.Split(splitChar)[0], System.Globalization.NumberStyles.Integer);
          response = (T)Convert.ChangeType(lg, typeof(T));

        }
      }

      return response;
    }

    private static char ConverToChar(object obj) {
      if(obj.ToString().Length == 1)
        return Convert.ToChar(obj);

      return default;
    }

    private static bool ConvertoToBool(object obj) {
      var temp = obj.ToString();
      if(temp.IsSomeBool()) {

        if(temp.IsNumber())
          return Convert.ToBoolean(obj.AsShort());

        else
          return Convert.ToBoolean(obj);
      }
      return false;
    }
    private static IList<Type> DBNums() => new List<Type> { typeof(int), typeof(long), typeof(byte), typeof(sbyte), typeof(decimal), typeof(float), typeof(double), typeof(ulong) };

    private static string GetJsonPropValue(DataColumn col, DataRow row, bool endObj = false) {
      string line;

      if(DBNums().Contains(col.DataType)) {
        var temp = row[col].AsString().Replace(',', '.');
        line = $"\"{col.ColumnName}\" : {(temp.IsFilled() ? temp : 0)}";

      } else if(col.DataType.IsArray) {
        line = $"\"{col.ColumnName}\" : {row[col].JsonSerialize()}";

      } else {
        line = $"\"{col.ColumnName}\" : \"{row[col]}\"";
      }

      if(!endObj)
        line = string.Concat(line, ',');

      return line;
    }


    #endregion

    public static T ValueOrExeption<T>(this T param, Exception typeException = null) {
      if(!typeException.IsFilled())
        typeException = new Exception("ERROR # The value entered is null or empty.");

      if(!param.IsFilled())
        throw typeException;

      return param;
    }

    public static T DeserializeTable<T>(this DataTable table) where T : IEnumerable {
      var json = new StringBuilder();

      if(!table.IsFilled())
        return default;

      int countRow = 0;
      foreach(DataRow row in table.Rows) {
        countRow++;

        json.Append('{'); // START JSON OBJECT

        int countCol = 0;
        foreach(DataColumn col in table.Columns) {
          countCol++;
          json.Append(countCol == table.Columns.Count ? GetJsonPropValue(col, row, true) : GetJsonPropValue(col, row)); // JSON PROPS
        }

        json.Append(countRow == table.Rows.Count ? '}' : "},"); // END JSON OBJECT
      }

      return JsonDeserialize<T>($"[{json.AsString()}]");
    }

    public async static ValueTask<T> DeserializeTableAsync<T>(this DataTable table, CancellationToken cancellationToken = default) where T : IEnumerable {
      return await Task.Run(T () => {
        var json = new StringBuilder();

        if(!table.IsFilled())
          return default;

        int countRow = 0;
        foreach(DataRow row in table.Rows) {
          countRow++;

          json.Append('{'); // START JSON OBJECT

          int countCol = 0;
          foreach(DataColumn col in table.Columns) {
            countCol++;
            json.Append(countCol == table.Columns.Count ? GetJsonPropValue(col, row, true) : GetJsonPropValue(col, row)); // JSON PROPS
          }

          json.Append(countRow == table.Rows.Count ? '}' : "},"); // END JSON OBJECT
        }

        return JsonDeserialize<T>($"[{json.AsString()}]");
      }, cancellationToken);
    }

    public static string RemoveWhiteSpaces(this string str) => str.Where(x => !char.IsWhiteSpace(x)).ToString();

    public static T JsonDeserialize<T>(this string jsonStringObject) => JsonSerializer.Deserialize<T>(jsonStringObject, GetJsonSerializerOptions());

    public async static ValueTask<T> JsonDeserializeAsync<T>(this Stream jsonUt8Stream, CancellationToken cancellationToken = default) => await JsonSerializer.DeserializeAsync<T>(jsonUt8Stream, GetJsonSerializerOptions(), cancellationToken);

    public static string JsonSerialize<T>(this T objectToSerialize, bool ident = false) => JsonSerializer.Serialize(objectToSerialize, GetJsonSerializerOptions(ident));

    public async static Task JsonSerializeAsync<T>(this T objectToSerialize, Stream utf8Json, bool ident = false, CancellationToken cancellationToken = default) => await JsonSerializer.SerializeAsync(utf8Json, objectToSerialize, GetJsonSerializerOptions(ident), cancellationToken);
  }
}
