using System.Collections.Generic;
using System.Data;

namespace PublicUtility.Extension {
  public static partial class Extends {

    public static bool IsFilled<T>(this T value) {
      if(value == null)
        return false;

      if(typeof(T) == typeof(string) && value.AsString() == string.Empty) 
        return false;

      return true;
    }
    
    public static bool IsFilled<T>(this IEnumerable<T> enumerable) {
      if(enumerable == null)
        return false;

      return enumerable.Any();
    }

    public static bool IsFilled<T>(this IList<T> list) {
      if(list == null)
        return false;

      return list.Any();
    }

    public static bool IsFilled<T>(this T[] arraylist) {
      if(arraylist == null)
        return false;

      return arraylist.Any();
    }
    
    public static bool IsDefault<T>(this T value) => value.Equals(default(T));

    public static bool IsSomeBool(this string input) {
      if(!input.IsFilled())
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
        if(!input.IsFilled())
          return false;

        return !GetSafeValue<DateTime>(input).IsDefault();
      } catch(Exception) { return false; }
    }

    public static bool IsNumber(this string input) {
      var numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ',' };

      if(!input.IsFilled())
        return false;

      if(input.Where(x => !numbers.Contains(x)).IsFilled()) 
        return false;

      return true;
    }
  }
}
