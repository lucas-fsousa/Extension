namespace PublicUtility.Extension {
  public static partial class Extends {

    public static bool IsFilled<T>(this T? value) {
      if(value is null)
        return false;

      if(typeof(T) == typeof(string) && value.AsString() == string.Empty) 
        return false;

      return true;
    }

    public static bool IsFilled<T>(this List<T>? list) {
      if(list == null) 
        return false;

      return list.Any();
    }
    
    public static bool IsFilled<T>(this IEnumerable<T>? enumerable) {
      if(enumerable == null)
        return false;

      return enumerable.Any();
    }

    public static bool IsFilled<T>(this IList<T>? list) {
      if(list == null)
        return false;

      return list.Any();
    }

    public static bool IsFilled<T>(this T?[]? arraylist) {
      if(arraylist == null)
        return false;

      return arraylist.Length != 0;
    }
    
    public static bool IsDefault<T>(this T? value) {
      var isEquals = value?.Equals(default(T));

      return isEquals ?? false;
    }

    public static bool IsSomeBool(this string? input) {
      var boolTypes = new[] { "false", "true", "0", "1" };
      if(!input.IsFilled())
        return false;


      return boolTypes.Any(v => v.Equals(input, StringComparison.CurrentCultureIgnoreCase));
    }

    public static bool IsAnyDate(this string? input) {
      try {
        if(!input.IsFilled())
          return false;

        return input.AsDateTime().IsDefault();
      } catch(Exception) { return false; }
    }

    public static bool IsNumber(this string? input) {
      if(!input.IsFilled() || !double.TryParse(input, out _))
        return false;

      return true;
    }
  }
}
