using PublicUtility.Extension.Converters;
using PublicUtility.Nms.Enums;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PublicUtility.Extension {
  public static class Extends {

    #region PRIVATES
    private static string ReplaceStrParams(string input, params object[] args) {
      int i = 0;

      foreach(var arg in args) {
        input = input.Replace($"{{{i}}}", $"{arg}");
        i++;
      }
      return input;
    }

    private static JsonSerializerOptions GetJsonSerializerOptions() {
      var jsonOptions = new JsonSerializerOptions() {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters = {
          new CustomBoolConverter(),
          new CustomDateOnlyConverter(),
          new CustomTimeOnlyConverter(),
          new CustomDateTimeConverter()
        }
      };
      return jsonOptions;
    }

    private static T ConvertToNumber<T>(object obj, int precision = 0) {
      T response = default;

      if(obj.ToString().IsNumber()) {
        string value = obj.ToString().Replace('.', ',');

        if(typeof(T).Name == "Decimal") {
          decimal dc = decimal.Round(decimal.Parse(value, System.Globalization.NumberStyles.Currency), precision);
          response = (T)Convert.ChangeType(dc, typeof(T));

        } else if(typeof(T).Name == "Single") {
          float ft = float.Parse(value, System.Globalization.NumberStyles.Float);
          response = (T)Convert.ChangeType(ft, typeof(T));

        } else if(typeof(T).Name == "Double") {
          double db = double.Parse(value, System.Globalization.NumberStyles.Any);
          response = (T)Convert.ChangeType(db, typeof(T));

        } else if(typeof(T).Name == "Int16") {
          short st = short.Parse(value.Split(',')[0], System.Globalization.NumberStyles.Integer);
          response = (T)Convert.ChangeType(st, typeof(T));

        } else if(typeof(T).Name == "Int32") {
          int it = int.Parse(value.Split(',')[0], System.Globalization.NumberStyles.Integer);
          response = (T)Convert.ChangeType(it, typeof(T));

        } else if(typeof(T).Name == "Int64") {
          long lg = long.Parse(value.Split(',')[0], System.Globalization.NumberStyles.Integer);
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
      if(obj.ToString().IsSomeBool()) {

        if(obj.ToString().IsNumber())
          return Convert.ToBoolean(Convert.ToInt32(obj));

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

      } else {
        line = $"\"{col.ColumnName}\" : \"{row[col]}\"";
      }


      if(!endObj)
        line = string.Concat(line, ',');

      return line;
    }

    #endregion

    public static string GetOnly(this IEnumerable<char> enumrable, SearchType searchFor) {
      string newStr = string.Empty;
      foreach(char c in enumrable) {
        if(searchFor.Equals(SearchType.Symbols)) {
          if(char.IsSymbol(c))
            newStr += c;

        } else if(searchFor.Equals(SearchType.Numbers)) {
          if(char.IsNumber(c))
            newStr += c;

        } else if(searchFor.Equals(SearchType.Letters)) {
          if(char.IsLetter(c))
            newStr += c;

        } else if(searchFor.Equals(SearchType.NumbersAndLetters)) {
          if(char.IsLetter(c) || char.IsNumber(c))
            newStr += c;

        } else if(searchFor.Equals(SearchType.SpecialChars)) {
          if(char.IsPunctuation(c))
            newStr += c;

        } else if(searchFor.Equals(SearchType.UpperCase)) {
          if(char.IsUpper(c))
            newStr += c;

        } else if(searchFor.Equals(SearchType.LowerCase)) {
          if(char.IsLower(c))
            newStr += c;

        } else if(searchFor.Equals(SearchType.WhiteSpaces)) {
          if(char.IsWhiteSpace(c))
            newStr += c;

        }

      }
      return newStr;
    }

    public static bool IsFilled<T>(this T param) {
      if(param == null)
        return false;

      if(typeof(T).Equals(typeof(string)) && param.ToString() == string.Empty)
        return false;

      return true;
    }

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

    public static T GetSafeValue<T>(this T value) { try { return (T)Convert.ChangeType(value, typeof(T)); } catch(Exception) { return default; } }

    public static T GetSafeValue<T>(this object value) { try { return (T)Convert.ChangeType(value, typeof(T)); } catch(Exception) { return default; } }

    public static T GetNext<T>(this IList<T> enumerable, T value) {
      int next = enumerable.GetIndex(value) + 1;
      if(next < 0)
        return default;
      else
        return enumerable.ToList()[next];
    }

    public static int GetNext(this int value) {
      if(value >= 0)
        return value + 1;

      return (Math.Abs(value) + 1) * -1;
    }

    public static long GetNext(this long value) {
      if(value >= 0)
        return value + 1;

      return (Math.Abs(value) + 1) * -1;
    }

    public static string RemoveWhiteSpaces(this string str) {
      string newStr = string.Empty;

      if(!string.IsNullOrEmpty(str)) {
        foreach(char c in str) {
          if(!char.IsWhiteSpace(c))
            newStr += c;
        }
      }

      return newStr;
    }

    public static string ConvertToJsonDateTime(this object data) => data.GetSafeValue<DateTime>().ToString("s");

    public static string GetConnectionString(string connectionName) {
      var connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
      if(connectionString.IsFilled())
        return connectionString;

      return string.Empty;
    }

    public static string GetFromAppConfig(string key) => ConfigurationManager.AppSettings[key].ValueOrExeption();

    public static bool IsDefault<T>(this T value) {
      T def = default;
      if(value.Equals(def))
        return true;
      return false;
    }

    public static bool IsSomeBool(this string input) {
      if(string.IsNullOrEmpty(input))
        return false;

      else if(input == string.Format("false") || input == string.Format("False"))
        return true;

      else if(input == string.Format("true") || input == string.Format("True"))
        return true;

      else if(input == string.Format("0") || input == string.Format("1"))
        return true;

      else
        return false;
    }

    public static bool IsAnyDate(this string input) {
      try {
        if(string.IsNullOrEmpty(input))
          return false;

        return !GetSafeValue<DateTime>(input).IsDefault();
      } catch(Exception) { return false; }
    }

    public static bool IsNumber(this string input) {
      var numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ',' };
      int countFloatPoint = 0;

      if(string.IsNullOrEmpty(input))
        return false;

      foreach(char c in input.ToList()) {
        if(c == '.' || c == ',')
          countFloatPoint++;

        if(countFloatPoint > 1)
          return false;

        if(!numbers.Contains(c))
          return false;
      }

      return true;
    }

    public static bool IsEmpty<T>(this IEnumerable<T> enumerable) where T : IEnumerable => !enumerable.Any();

    public static string AsDateFormmat(this string dateTime, string formmat = "yyyy/MM/dd HH:mm:ss") => Convert.ToDateTime(dateTime).ToString(formmat);

    public static bool AsBool<T>(this T obj) => ConvertoToBool(obj);

    public static int AsInt32<T>(this T obj) => ConvertToNumber<int>(obj);

    public static long AsInt64<T>(this T obj) => ConvertToNumber<long>(obj);

    public static short AsInt16<T>(this T obj) => ConvertToNumber<short>(obj);

    public static double AsDouble<T>(this T obj) => ConvertToNumber<double>(obj);

    public static decimal AsDecimal<T>(this T obj, int precision = 2) => ConvertToNumber<decimal>(obj, precision);

    public static float AsFloat<T>(this T obj) => ConvertToNumber<float>(obj);

    public static char AsChar<T>(this T obj) => ConverToChar(obj);

    public static string AsString<T>(this T obj) => obj.ToString();

    public static int GetIndex<T>(this IList<T> enumerable, T itemToLoc) {
      if(!enumerable.IsFilled() && !itemToLoc.IsFilled())
        return -1;

      for(int i = 0; i < enumerable.Count; i++) {
        if(enumerable[i].Equals(itemToLoc))
          return i;
      }

      return -1;
    }

    public static IList<T> GetUniques<T>(this IList<T> main, IList<T> off) {
      var result = new List<T>();
      var lstJsonTemp = new List<string>();

      for(int i = 0; i < main.Count; i++) {
        string json = JsonSerializer.Serialize(main[i]).ToLower().RemoveWhiteSpaces();
        if(lstJsonTemp.Contains(json))
          continue;

        lstJsonTemp.Add(json);
        result.Add(main[i]);
      }

      for(int i = 0; i < off.Count; i++) {
        string json = JsonSerializer.Serialize(off[i]).ToLower().RemoveWhiteSpaces();
        if(lstJsonTemp.Contains(json))
          continue;

        lstJsonTemp.Add(json);
        result.Add(off[i]);
      }

      return result;
    }

    public static void Println(this string message) => CustomConsole.WriteLine(message);

    public static void Println(this string message, params object[] args) => CustomConsole.WriteLine(ReplaceStrParams(message, args));

    public static void Println(this string message, object arg = null) => CustomConsole.WriteLine(ReplaceStrParams(message, arg));

    public static void Println(this object obj) => CustomConsole.WriteLine(obj);

    public static void Println() => Console.WriteLine();

    public static void Println<T>(this T[] array) { try { JsonSerialize(array).Println(); } catch(Exception) { array.ToString().Println(); } }

    public static void Println<T>(this IList<T> list) { try { JsonSerialize(list).Println(); } catch(Exception) { list.ToString().Println(); } }

    public static void Println<T>(this T obj) where T : class { try { JsonSerialize(obj).Println(); } catch(Exception) { obj.ToString().Println(); } }

    public static void Print(this string message) => CustomConsole.Write(message);

    public static void Print(this string message, params object[] args) => CustomConsole.Write(ReplaceStrParams(message, args));

    public static void Print(this string message, object arg = null) => CustomConsole.Write(ReplaceStrParams(message, arg));

    public static void Print(this object obj) => CustomConsole.Write(obj);

    public static void Print<T>(this T[] array) { try { JsonSerialize(array).Print(); } catch(Exception) { array.ToString().Print(); } }

    public static void Print<T>(this IList<T> list) { try { JsonSerialize(list).Print(); } catch(Exception) { list.ToString().Print(); } }

    public static void Print<T>(this T obj) where T : class { try { JsonSerialize(obj).Print(); } catch(Exception) { obj.ToString().Print(); } }

    public static string GetStringFromStream(this Stream stream, ICollection<string> encodingTypes = null) {
      byte[] buffer = new byte[stream.Length];

      if(!encodingTypes.IsFilled()) {
        stream.Read(buffer);
        return Encoding.Default.GetString(buffer);
      }

      if(encodingTypes.FirstOrDefault(x => x.ToLower().Equals("gzip")).IsFilled()) {
        using var zip = new GZipStream(stream, CompressionMode.Decompress, true);
        zip.Read(buffer);

      } else if(encodingTypes.FirstOrDefault(x => x.ToLower().Equals("deflate")).IsFilled()) {
        using var zip = new DeflateStream(stream, CompressionMode.Decompress, true);
        zip.Read(buffer);

      } else if(encodingTypes.FirstOrDefault(x => x.ToLower().Equals("br")).IsFilled()) {
        using var zip = new BrotliStream(stream, CompressionMode.Decompress, true);
        zip.Read(buffer);

      } else {
        stream.Read(buffer);
        stream.Close();
      }

      stream.Dispose();
      return Encoding.Default.GetString(buffer);
    }

    public static T JsonDeserialize<T>(this string jsonStringObject) => JsonSerializer.Deserialize<T>(jsonStringObject, GetJsonSerializerOptions());

    public static string JsonSerialize<T>(this T objectToSerialize) => JsonSerializer.Serialize(objectToSerialize, GetJsonSerializerOptions());


  }
}
