using System.Collections;

namespace PublicUtility.Extension {
  public static partial class Extends {

    public static void Println(this string message) => CustomConsole.WriteLine(message);

    public static void Println(this string message, params object[] args) => CustomConsole.WriteLine(ReplaceStrParams(message, args));

    public static void Println(this string message, object arg = null) => CustomConsole.WriteLine(ReplaceStrParams(message, arg));

    public static void Println(this object obj) => CustomConsole.WriteLine(obj);

    public static void Println() => Console.WriteLine();

    public static void Println<T>(this T[] array) where T : IEnumerable { 
      try { 
        JsonSerialize(array).Println(); 
      } catch(Exception) { 
        array.ToString().Println(); 
      } 
    }

    public static void Println<T>(this IList<T> list) where T : IEnumerable { 
      try { 
        JsonSerialize(list).Println(); 
      } catch(Exception) { 
        list.ToString().Println(); 
      }
    }

    public static void Println<T>(this T obj) where T : class { 
      try { 
        JsonSerialize(obj).Println(); 
      } catch(Exception) { 
        obj.ToString().Println(); 
      }
    }

    public static void Print(this string message) => CustomConsole.Write(message);

    public static void Print(this string message, params object[] args) => CustomConsole.Write(ReplaceStrParams(message, args));

    public static void Print(this string message, object arg = null) => CustomConsole.Write(ReplaceStrParams(message, arg));

    public static void Print(this object obj) => CustomConsole.Write(obj);

    public static void Print<T>(this T[] array) { 
      try { 
        JsonSerialize(array).Print(); 
      } catch(Exception) { 
        array.ToString().Print(); 
      } 
    }

    public static void Print<T>(this IList<T> list) { 
      try { 
        JsonSerialize(list).Print(); 
      } catch(Exception) { 
        list.ToString().Print(); 
      } 
    }

    public static void Print<T>(this T obj) where T : class { 
      try { JsonSerialize(obj).Print(); 
      } catch(Exception) { 
        obj.ToString().Print(); 
      } 
    }

  }
}
