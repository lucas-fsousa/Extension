using System.Data;

namespace PublicUtility.Extension {
  public static partial class Extends {

    public static string GetOnlyNumbers(this IEnumerable<char> input) => input.Where(char.IsNumber).AsString();

    public static string GetOnlyLetters(this IEnumerable<char> input) => input.Where(char.IsLetter).AsString();

    public static string GetOnlySymbols(this IEnumerable<char> input) => input.Where(char.IsSymbol).AsString();

    public static string GetOnlySpecials(this IEnumerable<char> input) => input.Where(char.IsPunctuation).AsString();

    public static string GetOnlyUpper(this IEnumerable<char> input) => input.Where(char.IsUpper).AsString();

    public static string GetOnlyLower(this IEnumerable<char> input) => input.Where(char.IsLower).AsString();

    public static string GetOnlyWhiteSpace(this IEnumerable<char> input) => input.Where(char.IsWhiteSpace).AsString();

    public static string GetOnlyLettersAndNumbers(this IEnumerable<char> input) => input.Where(x => char.IsLetter(x) || char.IsNumber(x)).AsString();

    //public static T? GetSafeValue<T>(this object value) { 
    //  try { 
    //    return (T)Convert.ChangeType(value, typeof(T)); 
    //  } catch(Exception) { 
    //    return default(T); 
    //  } 
    //}

    public static T? GetNext<T>(this IEnumerable<T> enumerable, T value) {
      int next = enumerable.GetIndex(value) + 1;
      if(next < 0)
        return default;
      else
        return enumerable.ToArray()[next];
    }

    public static int GetIndex<T>(this IEnumerable<T> enumerable, T itemToLoc) => enumerable.ToList().IndexOf(itemToLoc);
  }
}
