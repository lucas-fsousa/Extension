using System.Data;

namespace PublicUtility.Extension {
  public static partial class Extends {

    public static string GetOnlyNumbers(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(char.IsNumber).ToList().ForEach(x => resp += x);
      return resp;
    }

    public static string GetOnlyLetters(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(char.IsLetter).ToList().ForEach(x => resp += x);
      return resp;
    }

    public static string GetOnlySymbols(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(char.IsSymbol).ToList().ForEach(x => resp += x);
      return resp;
    }

    public static string GetOnlySpecials(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(char.IsPunctuation).ToList().ForEach(x => resp += x);
      return resp;
    }

    public static string GetOnlyUpper(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(char.IsUpper).ToList().ForEach(x => resp += x);
      return resp;
    }

    public static string GetOnlyLower(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(char.IsLower).ToList().ForEach(x => resp += x);
      return resp;
    }

    public static string GetOnlyWhiteSpace(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(char.IsWhiteSpace).ToList().ForEach(x => resp += x);
      return resp; 
    }

    public static string GetOnlyLettersAndNumbers(this IEnumerable<char> input) {
      string resp = string.Empty;
      input.Where(x => char.IsLetter(x) || char.IsNumber(x)).ToList().ForEach(x => resp += x);
      return resp;
    }

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
