using System.Diagnostics;
using shared;

namespace day12;

internal record Thing(int Height, int? DistanceFromStart, int? DistanceFromEnd);
internal record Thing2(int X, int Y, int Height, int? DistanceFromStart, int? DistanceFromEnd) : Thing(Height, DistanceFromStart, DistanceFromEnd);

public static class D12P1
{
    public static object Part1Answer(this string input) =>
        input
            .ParseThings()
            .Select((thing, x, y) => new Thing2(x, y, thing.Height, thing.DistanceFromStart, thing.DistanceFromEnd))
            .AsGridArray()
            .PopulateDistancesFromEnd()
            .LowestDistanceFromStartToEnd();

    internal static IEnumerable<IEnumerable<Thing>> ParseThings(this string input) =>
        input
            .NotEmptyTrimmedLines()
            .Select(ParseAsThingRow);

    internal static Thing[] ParseAsThingRow(this string line) => line.Select(ParseCharacter).ToArray();

    internal static Thing ParseCharacter(this char line) =>
        line switch
        {
            >='a' and <='z' => new Thing(line-'a',null,null),
            'S' => new Thing(0 ,0, null),
            'E' => new Thing('z'-'a',null,0),
            _=> throw new UnreachableException($"Impossible character {line}")
        };

    internal static Thing2[][] PopulateDistancesFromStart(this Thing2[][] grid)
    {
        var start = grid.GridItems().First(thing => thing.DistanceFromStart == 0);
        List<Thing2> todo = new() { start };
        while (todo.Any())
        {
            var thing = todo.Last();
            todo.RemoveAt(todo.Count - 1);
            if (thing.DistanceFromEnd.HasValue)
                continue;
            var nextDistance = thing.DistanceFromStart! + 1;

            todo.AddRange(grid.Neighbors(thing.X, thing.Y)
                .Where(loc => loc.Height - thing.Height <= 1)
                .Where(t => !t.DistanceFromStart.HasValue || t.DistanceFromStart.Value > nextDistance)
                .Select(neighbor => grid[neighbor.Y][neighbor.X] = neighbor with { DistanceFromStart = nextDistance }));
        }

        return grid;
    }

    internal static Thing2[][] PopulateDistancesFromEnd(this Thing2[][] grid)
    {
        var start = grid.GridItems().First(thing => thing.DistanceFromEnd == 0);
        List<Thing2> todo = new() { start };
        while (todo.Any())
        {
            var thing = todo.Last();
            todo.RemoveAt(todo.Count - 1);
            if (thing.DistanceFromStart.HasValue)
                continue;
            var nextDistance = thing.DistanceFromEnd! + 1;

            todo.AddRange(grid.Neighbors(thing.X, thing.Y)
                .Where(loc => loc.Height - thing.Height >= -1)
                .Where(t => !t.DistanceFromEnd.HasValue || t.DistanceFromEnd.Value > nextDistance).Select(neighbor =>
                    grid[neighbor.Y][neighbor.X] = neighbor with { DistanceFromEnd = nextDistance }));
        }

        return grid;
    }

    internal static int LowestDistanceFromStartToEnd(this Thing2[][] grid) =>
        grid
            .Rows()
            .SelectMany(row => row.Select(TotalDistance))
            .Min();

    internal static int TotalDistance(this Thing v)
        => (v.DistanceFromStart, v.DistanceFromEnd) switch
        {
            (null, _) => int.MaxValue,
            (_, null) => int.MaxValue,
            ({ } s, { } e) => s + e,
        };

    internal static int GetResult(this IEnumerable<Thing> things) => things.Select(AsResult).Sum();
    internal static int AsResult(this Thing thing) => 0;
}