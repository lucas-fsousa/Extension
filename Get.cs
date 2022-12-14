using System.Data;

namespace PublicUtility.Extension {
  public static partial class Extends {

    public static string GetOnlyNumbers(this IEnumerable<char> input) => input.Where(char.IsNumber).ToString();

    public static string GetOnlyLetters(this IEnumerable<char> input) => input.Where(char.IsLetter).ToString();

    public static string GetOnlySymbols(this IEnumerable<char> input) => input.Where(char.IsSymbol).ToString();

    public static string GetOnlySpecials(this IEnumerable<char> input) => input.Where(char.IsPunctuation).ToString();

    public static string GetOnlyUpper(this IEnumerable<char> input) => input.Where(char.IsUpper).ToString();

    public static string GetOnlyLower(this IEnumerable<char> input) => input.Where(char.IsLower).ToString();

    public static string GetOnlyWhiteSpace(this IEnumerable<char> input) => input.Where(char.IsWhiteSpace).ToString();

    public static string GetOnlyLettersAndNumbers(this IEnumerable<char> input) => input.Where(x => char.IsLetter(x) || char.IsNumber(x)).ToString();

    public static T GetSafeValue<T>(this T value) { 
      try { 
        return (T)Convert.ChangeType(value, typeof(T)); 
      } catch(Exception) {
        return default; 
      } 
    }

    public static T GetSafeValue<T>(this object value) { 
      try { 
        return (T)Convert.ChangeType(value, typeof(T)); 
      } catch(Exception) { 
        return default; 
      } 
    }

    public static T GetNext<T>(this IEnumerable<T> enumerable, T value) {
      int next = enumerable.GetIndex(value) + 1;
      if(next < 0)
        return default;
      else
        return enumerable.ToList()[next];
    }

    public static int GetIndex<T>(this IEnumerable<T> enumerable, T itemToLoc) => enumerable.ToList().IndexOf(itemToLoc);
  }
}
