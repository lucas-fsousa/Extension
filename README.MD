# Extension

A repertoire with several methods to assist in multitasking.

## Installation

To install, just run the C# compiler to generate the .dll file and once the file has been generated, just add the reference to the project or use [Nuget](https://www.nuget.org/packages/PublicUtility.Extension) or in nuget console, use the following command:


```bash
install-Package PublicUtility.Extension
```

## Methods in Extends Class

### Println & Print
These items are equivalent to the default "Console.WriteLine" and "Console.Write" from the System namespace. However, it presents more features such as the use by extension without the need to invoke the object directly. It can be used to show the detail of objects on the console through Serealization, it can also be used to change the default color of console letters to one of the available colors.

- Println(this string message, params object[] args)
- Println(this string message, object arg = null)
- Println<T>(this IList<T> list) 
- Println(this string message)
- Println<T>(this T[] array)
- Println(this object obj)
- Println<T>(this T obj)
- Println()

####

- Print(this string message, params object[] args)
- Print(this string message, object arg = null)
- Print<T>(this IList<T> list)
- Print(this string message)
- Print<T>(this T[] array)
- Print(this object obj)
- Print<T>(this T obj)

#### How to use

```csharp
using PublicUtility.Extension;

var anyString = "This is a long string name for custom print or println";

Extension.Print(anyString); // Common usage.
Extension.Println(anyString); // Common usage.

anyString.Print(); // use by extension
anyString.Println(); // use by extension

```

#### Applying color in the console using Print and Println
To apply the color it is necessary to use a syntax similar to interpolation. The "##" indicates that the console's color alternation will start, then it is necessary to inform the color that will be used, in this case we will use the Yellow color that can be represented by "C15" and opening of couchettes "[". Everything between couchetes will have its color changed. 
Closing the change is handled by "]##", thus forming "##C15[xxxxxxxx]##". In the example below we will have the name "ERROR" in red, "PROFILE" in DarkMagenta and the name "WINDOWS" in cyan

##### Colors Availables

- "c01" = ConsoleColor.Black
- "c02" = ConsoleColor.DarkBlue
- "c03" = ConsoleColor.DarkGreen
- "c04" = ConsoleColor.DarkCyan
- "c05" = ConsoleColor.DarkRed
- "c06" = ConsoleColor.DarkMagenta
- "c07" = ConsoleColor.DarkYellow 
- "c08" = ConsoleColor.Gray
- "c09" = ConsoleColor.DarkGray
- "c10" = ConsoleColor.Blue
- "c11" = ConsoleColor.Green
- "c12" = ConsoleColor.Cyan 
- "c13" = ConsoleColor.Red 
- "c14" = ConsoleColor.Magenta 
- "c15" = ConsoleColor.Yellow 
- "c16" = ConsoleColor.White

#### How to use
```csharp
using PublicUtility.Extension;

var name = "PROFILE";
var system = "WINDOWS";

Extension.Println("##C13[ERROR]##: USER ##C06[{0}]## NOT FOUND IN THE SYSTEM ##c12[{1}]##", name, system);
Extension.Print($"##C13[ERROR]##: USER ##C06[{name}]## NOT FOUND IN THE SYSTEM ##c12[{system}]##");

```

### Converters
Converters are methods for converting value types strategically and quickly during a run. Assuming that we have a return from some object that can be either in a string format or in another primitive format and we want to get its real format, we can use the appropriate extender.

- AsDateFormmat(this string dateTime, string formmat = "yyyy/MM/dd HH:mm:ss")
- AsBool<T>(this T obj)
- AsInt32<T>(this T obj)
- AsInt64<T>(this T obj)
- AsInt16<T>(this T obj)
- AsDouble<T>(this T obj)
- AsDecimal<T>(this T obj, int precision = 2)
- AsFloat<T>(this T obj)
- AsChar<T>(this T obj)
- AsString<T>(this T obj)

#### How to use:
```csharp
using PublicUtility.Extension;

string number = "1041.31";

double convertedToDouble = number.AsDouble(); // output 1041.31
decimal convertedToDecimal = convertedToDouble.AsDecimal(); // output 1041.31m
float convertedToFloat = convertedToDecimal.AsFloat(); // output 1041.31f
int convertedToInt = convertedToFloat.AsInt32(); // output 1041

```

### GetSafeValue
Gets the safe value of a return. If the returned value cannot be converted to the specified type, the default value of the specified type will be returned, preventing the application from crashing.

- GetSafeValue<T>(this T value)
- GetSafeValue<T>(this object value)

#### How to use:
```csharp
using PublicUtility.Extension;

string datetimeOK = "30-08-2022";
string datetimeNOK = "30;08;2022";

var datetimeSafeValueOK = datetimeOK.GetSafeValue<DateTime>(); // output 30/08/2022 00:00:00
var datetimeSafeValueNOK = datetimeNOK.GetSafeValue<DateTime>(); // output 01/01/0001 00:00:00

```

### Deserialize & Serialize
The Json Serealization and Deserealization library does not yet offer support for some specific cases, however the items in this library have already been treated and now it is possible to make the conversion without errors. It is also possible to transform a DataTable into a list or array of typed objects.

- DeserializeTable<T>(this DataTable table)
- JsonDeserialize<T>(this string jsonStringObject)
- JsonSerialize<T>(this T objectToSerialize)

#### How to use:
```csharp
using PublicUtility.Extension;

// FOR DATA TABLES
var myObjects = Extension.DeserializeTable<MyCustomObjectModel[]>(MyDataTable); // use example 1
var myObjects2 = MyDataTable.DeserializeTable<MyCustomObjectModel[]>(); // use example 2

// OTHERS
var myObject = new MyClass { Date = DateTime.Now, Name = "Lucas" };
var jsonString = myObject.JsonSerialize(); // output "{\"name\":\"Lucas\",\"date\":\"30/08/2022 21:19:32\"}"
var myObjectDeserealized = jsonString.JsonDeserialize<MyClass>(); // output new MyClass { Date = 30/08/2022 21:19:32, Name = "Lucas" }

public class MyClass {

  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("date")]
  public DateTime Date { get; set; }
}

```
### Validators
Used to validate data return in an agile and simplified way to avoid the use of multi conditions.

- IsFilled<T>(this T param) 
- IsSomeBool(this string input)
- IsAnyDate(this string input) 
- IsNumber(this string input) 
- IsEmpty<T>(this IEnumerable<T> enumerable) 
- IsDefault<T>(this T value) 

#### How to use:
```csharp
using PublicUtility.Extension;

string any = "1";

bool isnumber = any.IsNumber(); // checks if the string has a valid and convertible number format. Output true 
bool isDate = any.IsAnyDate(); // checks if the string has any date format. Output false
bool isFilled = any.IsFilled(); // checks if the object is filled in some way. Output true
bool isBool = any.IsSomeBool(); // checks if the string has some boolean format. Output true
bool isDefault = any.IsDefault(); // Checks if the value is the object's default. Output false

```
### Getters

They are methods that perform the action of collecting information from a point, be it an array, a list, an enumerator or other types of data. It can be used to locate object indices, values, distinctions, among others.

- GetUniques<T>(this IList<T> main, IList<T> off)
- GetIndex<T>(this IList<T> enumerable, T itemToLoc)
- GetNext(this int value)
- GetNext(this long value)
- GetNext<T>(this IList<T> enumerable, T value)

#### How to use:
```csharp
using PublicUtility.Extension;

var list1 = new List<int>() { 1, 2, 3, 4, 5 };
var list2 = new List<int>() { 4, 5, 6, 7, 8 };

var uniques = Extension.GetUniques(list1, list2); // compares two lists of objects and returns a new list with unique objects. Output 1,2,3,4,5,6,7,8
var indexNumber6 = uniques.GetIndex(6); // get the index of an item in a list or return -1 if not found. Output 5
var indexNumber10 = uniques.GetIndex(10); // get the index of an item in a list or return -1 if not found. Output -1
var nextValue = uniques.GetNext(3); // get the next item in the object list. Output 4
var nextInt = Extension.GetNext(213); // // get the next value in the sequence of integers. output 214

```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License