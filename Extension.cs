using NJ = Newtonsoft.Json;
using PublicUtility.Extension.Converters;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace PublicUtility.Extension {
  public static partial class Extends {

    #region PRIVATES
   
    private static string ReplaceStrParams(string input, params object[]? args) {
      int i = 0;

      if(args is null || args.Length < 1)
        return input;

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

    private static T ToIntType<T>(object? @object, CultureInfo? cultureInfo = null) {
      var type = typeof(T);
      var tempNum = @object.AsString();

      if(!tempNum.IsFilled())
        throw new ArgumentException($"{nameof(@object)} is null or empty!");

      if(!tempNum.IsNumber())
        throw new ArgumentException($"{nameof(@object)} not a number!");

      if(type == typeof(int))
        return (T)Convert.ChangeType(int.Parse(tempNum, NumberStyles.Integer, cultureInfo), type);

      if(type == typeof(long))
        return (T)Convert.ChangeType(long.Parse(tempNum, NumberStyles.Integer, cultureInfo), type);

      if(type == typeof(short))
        return (T)Convert.ChangeType(short.Parse(tempNum, NumberStyles.Integer, cultureInfo), type);

      throw new Exception($"{nameof(@object)} is not a integer number!");
    }

    private static T ToOtherNumberType<T>(object? @object, int precision = 0, CultureInfo? cultureInfo = null) {
      var type = typeof(T);
      var tempNum = @object.AsString();

      if(!tempNum.IsFilled())
        throw new ArgumentException($"{nameof(@object)} is null or empty!");

      if(!tempNum.IsNumber())
        throw new ArgumentException($"{nameof(@object)} not a number!");

      if(type == typeof(decimal))
        return (T)Convert.ChangeType(decimal.Round(decimal.Parse(tempNum, NumberStyles.Currency, cultureInfo), precision), type);

      if(type == typeof(float))
        return (T)Convert.ChangeType(float.Parse(tempNum, NumberStyles.Float, cultureInfo), type);

      if(type == typeof(double))
        return (T)Convert.ChangeType(double.Parse(tempNum, NumberStyles.Any, cultureInfo), type);

      throw new Exception($"{nameof(@object)} is not a float number!");
    }

    private static char ConverToChar(object? obj) {
      if(obj.AsString().Length == 1)
        return Convert.ToChar(obj);

      return default;
    }

    private static bool ConvertoToBool(object? obj) {
      var temp = obj?.ToString();
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

    public static T ValueOrExeption<T>(this T param, Exception? typeException = null) {
      if(!typeException.IsFilled())
        typeException = new Exception("ERROR # The value entered is null or empty.");

      if(!param.IsFilled())
        throw typeException!;

      return param;
    }

    public static T? DeserializeTable<T>(this DataTable table) where T : IEnumerable {
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

    public static string RemoveWhiteSpaces(this string str) => str.Where(x => !char.IsWhiteSpace(x)).AsString();

    public static T? JsonDeserialize<T>(this string jsonStringObject) => JsonSerializer.Deserialize<T?>(jsonStringObject, GetJsonSerializerOptions());

    public async static Task<T?> JsonDeserializeAsync<T>(this Stream jsonUt8Stream, CancellationToken cancellationToken = default) => await JsonSerializer.DeserializeAsync<T>(jsonUt8Stream, GetJsonSerializerOptions(), cancellationToken);

    public static string JsonSerialize<T>(this T? objectToSerialize, bool ident = false) => JsonSerializer.Serialize(objectToSerialize, GetJsonSerializerOptions(ident));

    public async static Task JsonSerializeAsync<T>(this T? objectToSerialize, Stream utf8Json, bool ident = false, CancellationToken cancellationToken = default) => await JsonSerializer.SerializeAsync(utf8Json, objectToSerialize, GetJsonSerializerOptions(ident), cancellationToken);
  
    public static T? DeepCopy<T>(this T? input) => input.JsonSerialize().JsonDeserialize<T?>();

    public static string XmlToJson(this string xmlInput) {
      if(string.IsNullOrEmpty(xmlInput))
        return string.Empty;
      return NJ.JsonConvert.SerializeXNode(XDocument.Parse(xmlInput));
    }

    public static string RegexMatch(this string input, string pattern, int group = 1) => Regex.Matches(input, pattern)?.Cast<Match>()?.FirstOrDefault()?.Groups[group].Value ?? "";

    public static string RegexReplace(this string input, string pattern, string newValue = "") => Regex.Replace(input, pattern, newValue);

    public static string[] RegexSplit(this string input, string pattern) => Regex.Split(input, pattern);

    public static bool RegexIsMatch(this string input, string pattern) => Regex.IsMatch(input, pattern);

    public static bool Exists(this string value, string text) {
      if(string.IsNullOrEmpty(value))
        return false;

      return value.Contains(text);
    }

    public static bool Exists(this string value, bool forAll, params string[] texts) {
      if(string.IsNullOrEmpty(value))
        return false;

      return forAll ? texts.ToList().TrueForAll(value.Contains) : texts.Any(value.Contains);
    }

    public static T? FirstRng<T>(this T[] array) {
      if(!array.Any())
        return default;

      var random = new Random(Guid.NewGuid().GetHashCode());
      var index = random.Next(array.Length);

      return array[index];
    }

    public static T? FirstRng<T>(this IEnumerable<T> enumerable) => FirstRng(enumerable.ToArray());

    public static string StringInject(this string input, string reference, bool beforeReference, string value) {
      var newString = string.Empty;
      if(input.Contains(reference)) {
        var index = beforeReference ? input.IndexOf(reference) : (input.IndexOf(reference) + reference.Length);
        newString = input[..index] + value + input[index..];
      }

      return newString;
    }
  }
}
