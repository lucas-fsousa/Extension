using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace PublicUtility.Extension {
  public static partial class Extends {
    public static string AsJsonDateTime(this object data) => data.AsDateTime().AsString("s");
    
    public static DateTime AsDateTime<T>(this T dateTime, CultureInfo culture = null) => Convert.ToDateTime(dateTime, culture);

    public static bool AsBool<T>(this T obj) => ConvertoToBool(obj);

    public static int AsInt<T>(this T obj, CultureInfo cultureInfo = null) => ToIntType<int>(obj, cultureInfo);

    public static long AsLong<T>(this T obj, CultureInfo cultureInfo = null) => ToIntType<long>(obj, cultureInfo);

    public static short AsShort<T>(this T obj, CultureInfo cultureInfo = null) => ToIntType<short>(obj, cultureInfo);

    public static double AsDouble<T>(this T obj, CultureInfo cultureInfo = null) => ToOtherNumberType<double>(obj, cultureInfo: cultureInfo);

    public static decimal AsDecimal<T>(this T obj, int precision = 2, CultureInfo cultureInfo = null) => ToOtherNumberType<decimal>(obj, precision, cultureInfo);

    public static float AsFloat<T>(this T obj, CultureInfo cultureInfo = null) => ToOtherNumberType<float>(obj, cultureInfo: cultureInfo);

    public static char AsChar<T>(this T obj) => ConverToChar(obj);

    public static string AsString(this object obj) => obj.ToString();

    public static string AsString(this DateTime dateTime, string formmat = "s") => dateTime.ToString(formmat);

    public static string AsString(this byte[] byteArray) => Encoding.UTF8.GetString(byteArray);

    public static string AsString(this Stream stream) => stream.AsByteArray().AsString();

    public static byte[] AsByteArray(this string utf8Base64String) => Encoding.UTF8.GetBytes(utf8Base64String);

    public static byte[] AsByteArray(this Stream stream) {
      var buffer = new byte[stream.Length];
      stream.Read(buffer);
      return buffer;
    }
    

  }
}
