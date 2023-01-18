using System.Text;

namespace PublicUtility.Extension {
  public static partial class Extends {
    public static string AsJsonDateTime(this object data) => data.GetSafeValue<DateTime>().ToString("s");

    public static string AsDateFormmat(this string dateTime, string formmat = "yyyy/MM/dd HH:mm:ss") => Convert.ToDateTime(dateTime).ToString(formmat);

    public static bool AsBool<T>(this T obj) => ConvertoToBool(obj);

    public static int AsInt<T>(this T obj) => ConvertToNumber<int>(obj);

    public static long AsLong<T>(this T obj) => ConvertToNumber<long>(obj);

    public static short AsShort<T>(this T obj) => ConvertToNumber<short>(obj);

    public static double AsDouble<T>(this T obj) => ConvertToNumber<double>(obj);

    public static decimal AsDecimal<T>(this T obj, int precision = 2) => ConvertToNumber<decimal>(obj, precision);

    public static float AsFloat<T>(this T obj) => ConvertToNumber<float>(obj);

    public static char AsChar<T>(this T obj) => ConverToChar(obj);

    public static string AsString(this object obj) => obj.ToString();

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
