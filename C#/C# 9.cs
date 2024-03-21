
// NOVIDADES DO C# 9

// Contents:
//      - Records
//      - Init-only setters
//      - Top-level statements
//      - Pattern matching
//      - Other features

// ---------------------------------------------------------------
//      - Records
// ---------------------------------------------------------------

public record Point(int X, int Y);

public record Point
{
    public int X { get; init; }
    public int Y { get; init; }
}

public record Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

// Positional records
public record Point(int X, int Y);

public void Method()
{
    Point point = new(0, 0); // Constructing
    WriteLine(point); // Point { X = 0, Y = 0 }

    (int x, int y) = point; // Deconstructing
}

// Value-based equality
public record Point(int X, int Y);

public void Method()
{
    Point point1 = new(0, 0);
    Point point2 = new(0, 0);
    WriteLine(point1 == point2); // True
    WriteLine(ReferenceEquals(point1, point2)); // False
}

// With-expressions
public record Point(int X, int Y)
{
    public string[] Titles { get; init; }
}

public void Method()
{
    Point point1 = new(0, 0) { Titles = new string[1] };
    WriteLine(point1); // Point { X = 0, Y = 0, Titles = System.String[] }

    Point point2 = point1 with { X = 1 };
    WriteLine(point1); // Point { X = 0, Y = 0, Titles = System.String[] }

    point2 = point1 with { Titles = new string[2] };
    WriteLine(point2); // Point { X = 0, Y = 0, Titles = System.String[] }

    point2 = point1 with { };
    WriteLine(point1 == point2); // True
}

// Inheritance
public record Point(int X, int Y);
public record Point3D(int X, int Y, int Z) : Point(X, Y);

Point point1 = new Point3D(X: 2, Y: 2, Z: 2);
Point point2 = point1 with { X = 1 };

// ---------------------------------------------------------------
//      - Init-only setters
// ---------------------------------------------------------------

public class Point
{
    public int X { get; init; }
    public int Y { get; init; }
}

public void Method()
{
    Point point = new() { X = 2, Y = 2 };
    WriteLine(point); // Point { X = 2, Y = 2 }

    point.Y = 3; // Error
}

// Read-only properties
public class Point
{
    readonly int _x;

    public int X
    {
        get => _x;
        init => _x = value ?? throw new ArgumentNullException(nameof(_x));
    }
}

// ---------------------------------------------------------------
//      - Top-level statements
// ---------------------------------------------------------------

// Before
using System;

class Program
{
    static void Main()
    {
        WriteLine("Hello, world!");
    }
}

// C# 9
using System;

WriteLine("Hello, world!");
return 0;

// ---------------------------------------------------------------
//      - Pattern matching
// ---------------------------------------------------------------

// Relational pattern
public record Point(int X, int Y);

static string IsLowOrHigh(Point point) => point.X switch
{
    < 0 => "Low",
    >= 0 => "High"
}

// Logical pattern
public record Point(int X, int Y);

static string IsLowOrHigh(Point point) => point.X switch
{
    < 0 or > 10 => "Out of range",
    >= 0 and <= 5 => "Low",
    >= 6 and <= 10 => "High"
}

// Negated
// Before
object obj = new();
if (!(obj is null))
{
    WriteLine("not null");
}

// C# 9
object obj = new();
if (obj is not null)
{
    WriteLine("not null");
}

static string IsNull(object obj)
    => obj is not null ? "not null" : "null";

// Conjuctive
static string IsLowOrHigh(int x)
    => x switch
    {
        < 0 => "Out of range",
        >= 0 and <= 5 => "Low",
        >= 6 and <= 10 => "High"
    };

// Disjunctive
object obj = new();
if (obj is string or int)
{
    WriteLine("string or int");
}

static string IsLowOrHigh(int x)
    => x switch
    {
        < 0 or > 10 => "Out of range",
        _ => new ArgumentOutOfRangeException(nameof(x))
    };

// Type pattern
static string GetType(object obj) => obj switch
{
    string => "string",
    int => "int",
    _ => "something else"
}

// Positional pattern
public record Point(int X, int Y);
    
static string IsOrigin(Point point) => point switch
{
    (0, 0) => "It's the origin",
    (int x, int y) => $"It's at ({x}, {y})",
    _ => throw new ArgumentOutOfRangeException(nameof(point))
}

// ---------------------------------------------------------------
//      - Other features
// ---------------------------------------------------------------

// Native integers (32 bits)
nint x = 1;
nlong y = 1;
nfloat z = 1;
ndouble w = 1;

// Type inference
// Before
List<string> list = new List<string>();
Dictionary<string, int> dictionary = new Dictionary<string, int>();

// C# 9
List<string> list = new();
Dictionary<string, int> dictionary = new();

public record Point
{
    public List<Vector> Vectors { get; } = new();
}
