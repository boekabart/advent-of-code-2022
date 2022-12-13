using System.Diagnostics;
using shared;

namespace day13;

internal readonly record struct Thing : IComparable<Thing>
{
    internal Thing(int value) => Value = value;
    internal Thing(Thing[] value) => Things = value;
    public int? Value { get; init; }
    public Thing[]? Things { get; init; }

    public int CompareTo(Thing other) => (Value, Things, other.Value, other.Things) switch
    {
        ({ } l, null, { } r, null) => l.CompareTo(r),
        ({ } l, null, null, { }) => l.Wrapped().CompareTo(other),
        (null, { } l, null, { } r) => l.CompareTo(r),
        (null, { }, { } r, null) => this.CompareTo(r.Wrapped()),
        _ => throw new UnreachableException()
    };
}

public static class D13P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParseThings()
            .Buffer(2)
            .Select((pair, index) => (Comparison: pair[0].CompareTo(pair[1]), Index: index + 1))
            .Where(t => t.Comparison == -1)
            .Sum(t => t.Index);

    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .NotEmptyTrimmedLines()
            .Select(TryParseAsThing);

    internal static Thing TryParseAsThing(this string line)
    {
        Stack<List<Thing>> parents = new();
        int? valueInWording = null;

        foreach (var ch in line)
        {
            switch (ch)
            {
                case '[':
                  var newArray = new List<Thing>();
                  parents.Push(newArray);
                  break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    valueInWording = (valueInWording ?? 0) * 10 + (ch - '0');
                    break;

                case ',':
                    if (valueInWording.HasValue)
                        parents.Peek().Add(new Thing(valueInWording.Value));
                    valueInWording = null;
                    break;

                case ']':
                    if (valueInWording.HasValue)
                        parents.Peek().Add(new Thing(valueInWording.Value));
                    valueInWording = null;

                    var theArray = parents.Pop().ToArray();
                    var newThing = new Thing(theArray);
                    if (parents.Count == 0)
                        return newThing;
                    parents.Peek().Add(newThing);
                    break;
            }
        }

        if (valueInWording.HasValue)
            return new(valueInWording.Value);

        throw new UnreachableException();
    }

    internal static int CompareTo(this Thing[] lhs, Thing[] rhs)
    {
        var commonLength = Math.Min(lhs.Length, rhs.Length);
        var result = lhs.Take(commonLength).Zip(rhs.Take(commonLength))
            .Select(pair => (int?)pair.First.CompareTo(pair.Second))
            .FirstOrDefault(r => r != 0);
        
        return result ?? lhs.Length.CompareTo(rhs.Length);
    }

    internal static Thing Wrapped(this int value) => new(new Thing[] {new(value)});

    internal static string Render(this Thing thing) => System.Text.Json.JsonSerializer.Serialize(thing.ToObject());

    internal static object ToObject(this Thing thing) =>

        thing switch
        {
            {Things: { }} => thing.Things.Select(ToObject),
            {Value: { } v} => v,
            _ => throw new UnreachableException()
        };
}