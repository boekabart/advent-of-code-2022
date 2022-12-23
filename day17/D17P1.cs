using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using shared;

namespace day17;

internal record Template(List<bool[]> Rock);
internal record Thing(List<bool[]> Rock, int X, int Y);

public static class D17P1
{
    public static object Part1Answer(this string input, long blocks = 2022)
    {
        var windDirections = input.NotEmptyTrimmedLines()
            .SelectMany(l => l.Select(w => w == '<'?-1:w == '>'?1:0))
            .Where( w => w!=0)
            .ToArray();
        
        //var theWind = Enumerable.Range(0, int.MaxValue).SelectMany(_ => windDirections);
        var theField = new Thing(new List<bool[]> { EmptyLine() }, 0, 0);
        var templateIndex = 0;
        Thing? theRock = null;
        int windIndex = 0;
        Dictionary<(long Top9Rows, int TemplateIndex, int WindIndex), int> situationsDictionary = new();
        List<int> heightsAfterBlocks = new() {0};
        while (true)
        {
            if (theRock is null)
            {
                theRock = SpawnRock();
                theField.Dump(theRock, "@");
            }

            var windDir = windDirections[windIndex++ % windDirections.Length];
            var blownRock = theRock with { X = theRock.X + windDir };
            if (!theField.Intersects(blownRock))
            {
                theRock = blownRock;
                theField.Dump(theRock, windDir<0?"<":">");
            }
            else
            {
                theField.Dump(theRock, windDir<0?"[":"]");
            }
            var fallenRock = theRock with { Y = theRock.Y - 1 };
            if (!theField.Intersects(fallenRock))
            {
                theRock = fallenRock;
                theField.Dump(theRock, "V");
                continue;
            }
            theField.Dump(theRock, "X");

           theRock = theField.Render(theRock);
           heightsAfterBlocks.Add(theField.RockHeight());

           if (templateIndex == blocks)
                break;

            var key1 = theField.Key();
            var key2 = templateIndex % Templates.Length;
            var key3 = windIndex % windDirections.Length;
            var key = (key1, key2, key3);
            
            if (situationsDictionary.TryGetValue(key, out var previousBlock))
            {
                var interval = templateIndex - previousBlock;
                var myHeight = heightsAfterBlocks[templateIndex];
                var hisHeight = heightsAfterBlocks[previousBlock];
                var heightPerInterval = myHeight - hisHeight;
                var refBlock = (int)(blocks % interval);
                var repeats = blocks / interval;
                var refHeight = heightsAfterBlocks[refBlock];
                var extra = heightPerInterval * repeats;
                var total = refHeight + extra;
                return total;
            }
            situationsDictionary.Add(key, templateIndex);
        }

        return theField.RockHeight();

        Thing SpawnRock()
        {
            return theField.Spawn(Templates[templateIndex++ % Templates.Length]);
        }
    }

    internal static long Key(this Thing big)
    {
        var valueTuples = big.Rock.AsEnumerable().Reverse().SkipWhile(Empty).Take(9)
            .SelectMany(arr => arr)
            .WithIndex()
            .ToList();
        var aggregate = valueTuples
            .Aggregate(0L, (bits, boel) => 
                boel.item ? 
                    bits + (1<< boel.index):
                    bits
                );
        return         aggregate;
    }

    internal static  void Dump(this Thing rock)
    {
        var lines = rock.Rock.AsEnumerable().Reverse().Select(arr => string.Join("", arr.Select(b => b ? "#" : ".")));
        
        foreach (var bools in lines)
        {
            Console.WriteLine(bools);
        }
    }

    private static bool dump;
    internal static  void Dump(this Thing big, Thing rock, string rok)
    {
        if (!dump)
        return;
        for (int y = rock.MaxY()+2; y >= big.Y && y > rock.Y-13; y--)
        {
            for (int x = big.X; x <= big.MaxX(); x++)
            {
                Console.Write(                rock.Value(x,y)?rok:big.Value(x,y)?"#":".");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    internal static bool Value(this Thing rok, int x, int y)
    {
        if (x < rok.X) return false;
        if (x > rok.MaxX()) return false;
        if (y < rok.Y) return false;
        if (y > rok.MaxY()) return false;
        return rok.Rock[y - rok.Y][x - rok.X];
    }
    
    internal static bool Intersects(this Thing big, Thing small)
    {
        if (small.X < big.X) return true;
        if (small.MaxX() > big.MaxX()) return true;
        if (small.Y < big.Y) return true;
        for (int y = 0; y < small.Rock.Count; y++)
        {
            var row = small.Rock[y];
            var targetRow = big.Rock[y + small.Y - big.Y];
            for (int x = 0; x < row.Length; x++)
            {
                if (targetRow[x + small.X - big.X] && row[x])
                    return true;
            }
        }

        return false;
    }

    internal static int MaxX(this Thing thing) => thing.X + thing.Rock[0].Length - 1;

    internal static int MaxY(this Thing thing) => thing.Y + thing.Rock.Count - 1;

    internal static Thing Spawn(this Thing big, Template template)
    {
        var thing = new Thing(template.Rock, 2, big.RockHeight() + 3);
        while (thing.MaxY() > big.MaxY())
            big.Rock.Add(EmptyLine());
        return thing;
    }

    internal static Thing? Render(this Thing big, Thing small)
    {
        for (int y = 0; y < small.Rock.Count; y++)
        {
            var row = small.Rock[y];
            var targetRow = big.Rock[y + small.Y - big.Y];
            for (int x = 0; x < row.Length; x++)
            {
                if (targetRow[x + small.X - big.X] && row[x])
                    throw new InvalidOperationException();
                targetRow[x + small.X - big.X] |= row[x];
            }
        }

        return null;
    }

    private static bool[] EmptyLine() => Enumerable.Repeat(0, 7).Select(_ => false).ToArray();

    internal static int RockHeight(this Thing big)
        => big.Rock.Count - big.Rock.AsEnumerable().Reverse().TakeWhile(Empty).Count();

    internal static bool Empty(bool[] line) => line.All(l => l == false);

    internal static readonly Template Dash = new(new()
        { new[] { true, true, true, true } });

    internal static readonly Template Plus = new(new()
    {
        new[] { false, true, false },
        new[] { true, true, true },
        new[] { false, true, false }
    });

    internal static readonly Template L = new(new()
    {
        new[] { true, true, true }, //  bottom line
        new[] { false, false, true },
        new[] { false, false, true }
    });

    internal static readonly Template I = new(new()
    {
        new[] { true },
        new[] { true },
        new[] { true },
        new[] { true }
    });

    internal static readonly Template Dot = new(new()
    {
        new[] { true, true },
        new[] { true, true }
    });

    internal static readonly Template[] Templates = {
        Dash,
        Plus,
        L,
        I,
        Dot
    };
}