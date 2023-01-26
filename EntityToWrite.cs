namespace PublicUtility.Extension {
  internal class EntityToWrite {
    internal EntityToWrite(ConsoleColor color, string message) {
      Message = message;
      Color = color;
    }

    internal string Message { get; set; }
    internal ConsoleColor Color { get; set; }
  }


  internal static class CustomConsole {
    private static ConsoleColor GetColor(string color) {

      if(string.IsNullOrEmpty(color))
        return ConsoleColor.Gray;

      try {
        var lstColor = new Dictionary<string, ConsoleColor>() {
        { "c01", ConsoleColor.Black },
        { "c02", ConsoleColor.DarkBlue },
        { "c03", ConsoleColor.DarkGreen },
        { "c04", ConsoleColor.DarkCyan },
        { "c05", ConsoleColor.DarkRed },
        { "c06", ConsoleColor.DarkMagenta },
        { "c07", ConsoleColor.DarkYellow },
        { "c08", ConsoleColor.Gray },
        { "c09", ConsoleColor.DarkGray },
        { "c10", ConsoleColor.Blue },
        { "c11", ConsoleColor.Green },
        { "c12", ConsoleColor.Cyan },
        { "c13", ConsoleColor.Red },
        { "c14", ConsoleColor.Magenta },
        { "c15", ConsoleColor.Yellow },
        { "c16", ConsoleColor.White }
      };

        return lstColor[color.ToLower()];
      } catch(Exception ex) {
        throw new Exception($"{ex.Message} - Choose a valid color between c01 and c16");
      }
    }

    private static List<EntityToWrite> WriteGenerator(string message) {
      var lstFrases = new List<EntityToWrite>();
      var itens = message.ToLower().Split("##c");

      for(int i = 0; i < itens.LongLength - 1; i++) {
        var startIndexFrase = message.ToLower().IndexOf("##c") + 6;
        var endIndexFrase = message.ToLower().IndexOf("]##", startIndexFrase);

        var temp = message[..(startIndexFrase - 6)];

        if(!temp.IsFilled())
          lstFrases.Add(new EntityToWrite(GetColor(string.Empty), temp));

        if(!message.ToLower()[startIndexFrase - 1].Equals('['))
          throw new Exception("SINTAX ERROR. INITIALIZATION TO APPLY COLOR NOT FOUND OR NOT REPORTED CORRECTLY. EX: \"##CXX[\"");

        if(endIndexFrase == -1)
          throw new Exception("SINTAX ERROR. THE \"]##\" MUST BE PRESENTED AFTER \"##CXX[\"");

        var color = message.ToLower().Substring(startIndexFrase - 4, 3);

        lstFrases.Add(new EntityToWrite(GetColor(color), message[startIndexFrase..endIndexFrase]));

        message = message[(endIndexFrase + 3)..];
      }

      if(!string.IsNullOrWhiteSpace(message))
        lstFrases.Add(new EntityToWrite(GetColor(string.Empty), message));

      return lstFrases;
    }

    internal static void Write(string message) {
      if(message.ToLower().Contains("##c")) {
        WriteGenerator(message).ForEach(x => {
          Console.ForegroundColor = x.Color;
          Console.Write(x.Message);
          Console.ResetColor();
        });

      } else {
        Console.Write(message);
      }

    }

    internal static void Write(object obj) => Console.Write(obj);

    internal static void WriteLine(string message) {
      if(message.ToLower().Contains("##c")) {
        WriteGenerator(message).ForEach(x => {
          Console.ForegroundColor = x.Color;
          Console.Write(x.Message);
          Console.ResetColor();
        });
        Console.WriteLine();

      } else {
        Console.WriteLine(message);
      }
    }

    internal static void WriteLine(object obj) => Console.WriteLine(obj);

    internal static void WriteLine() => Console.WriteLine();
  }
}
