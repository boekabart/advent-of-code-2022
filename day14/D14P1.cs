using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using shared;

namespace day14;

internal record Rock(List<Coordinate> Nodes);
internal record Coordinate(int X, int Y);

internal record Grid(char[][] Array, Coordinate Source);

public static class D14P1
{
    public static object Part1Answer(this string input) =>
        input
            .ParseRocks()
            .FillInTheBlanks()
            .ToArray()
            .AsGrid()
            .Dump()
            .CountHowManySandCanBeSpawnedWithoutSpilling();

    internal static IEnumerable<Rock> ParseRocks(this string input) =>
        input
            .NotEmptyTrimmedLines()
            .Select(TryParseAsRock);

    internal static Grid AsGrid(this ICollection<Coordinate> rockCoordinates)
    {
        var minX = rockCoordinates.Min(c => c.X);
        var maxX = rockCoordinates.Max(c => c.X);
        var width = 1 + (maxX - minX);
        var maxY = rockCoordinates.Max(c => c.Y);
        var height = maxY + 1;
        var gridArray = Enumerable.Range(0, height)
            .Select(_ => Enumerable.Range(0,width)
                .Select(_=>'.')
                .ToArray())
            .ToArray();
        foreach (var rc in rockCoordinates)
            gridArray[rc.Y][rc.X - minX] = '#';
        var source = new Coordinate(500 - minX, 0);
        gridArray[source.Y][source.X] = 'x';

        return new Grid(gridArray, source);
    }

    internal static Grid Dump(this Grid grid)
    {
        foreach (var line in grid.Array)
        {
            Console.WriteLine(line);
        }

        return grid;

    }

    internal static int CountHowManySandCanBeSpawnedWithoutSpilling(this Grid grid)
    {
        for (var count = 0; ; count++)
        {
            var finalLocation = grid.SimulateFallingSand();
            if (finalLocation is null)
            {
                grid.Dump();
                return count;
            }

            grid.Array[finalLocation.Y][finalLocation.X] = 'o';
        }

        throw new UnreachableException();
    }

    internal static Coordinate? SimulateFallingSand(this Grid grid)
    {
        var iter = grid.Source;
        if (grid.Array[iter.Y][iter.X] != 'x')
            return null;

        while (true)
            //  (iter.Y < grid.Array.Height() && iter.X >= 0 && iter.X < grid.Array.Width()))
        {
            var c1 = iter with { Y = iter.Y + 1 };
            var c2 = iter with { Y = iter.Y + 1, X = iter.X - 1 };
            var c3 = iter with { Y = iter.Y + 1, X = iter.X + 1 };
            if (c1.Y >= grid.Array.Height())
                return null;

            if (grid.Array.Val(c1.X, c1.Y) == '.')
            {
                iter = c1;
                continue;
            }

            if (c2.X < 0)
                return null;
            if (grid.Array.Val(c2.X, c2.Y) == '.')
            {
                iter = c2;
                continue;
            }

            if (c3.X >= grid.Array.Width())
                return null;

            if (grid.Array.Val(c3.X, c3.Y) == '.')
            {
                iter = c3;
                continue;
            }

            return iter;
        }
    }

        internal static IEnumerable<Coordinate> FillInTheBlanks(this IEnumerable<Rock> rocks)
        => rocks.SelectMany(FillInTheBlanks);

    internal static IEnumerable<Coordinate> FillInTheBlanks(this Rock rock)
        => rock.Nodes.Zip(rock.Nodes.Skip(1)).SelectMany(pair => RenderLine(pair.First, pair.Second)).Distinct();

    internal static IEnumerable<Coordinate> RenderLine(Coordinate c1, Coordinate c2)
        => Enumerable.Range(0, 1 + OrthoDist(c1, c2)).Select(i => c1.Move(i, OrthoSign(c1, c2)));

    private static int OrthoDist(Coordinate c1, Coordinate c2)
    {
        return Math.Max(Math.Abs(c2.X - c1.X), Math.Abs(c2.Y - c1.Y));
    }
    private static Coordinate OrthoSign(Coordinate c1, Coordinate c2)
    {
        return new(Math.Sign(c2.X - c1.X), Math.Sign(c2.Y - c1.Y));
    }

    private static Coordinate Move(this Coordinate c1, int count, Coordinate step)
    {
        return new(c1.X+count*step.X, c1.Y+count*step.Y);
    }

    internal static Rock TryParseAsRock(this string line) =>
        new Rock(
            line.Split(new[] { ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(ParseAsCoord)
                .ToList());

    internal static Coordinate ParseAsCoord(this string coord)
    {
        var split = coord.Split(',');
        return new Coordinate(int.Parse(split[0]), int.Parse(split[1]));
    }
}