using shared;

namespace day12;

public static class D12P2
{
    public static object Part2Answer(this string input) =>
        input
            .ParseThings()
            .Select((thing, x, y) => new Thing2(x, y, thing.Height, thing.DistanceFromStart, thing.DistanceFromEnd))
            .AsGridArray()
            .PopulateDistancesFromEnd()
            .Dump()
            .LowestDistanceFromHeight0ToEnd();

    internal static int LowestDistanceFromHeight0ToEnd(this Thing2[][] grid) =>
        grid
            .Rows()
            .SelectMany(row => row
                .Where( loc => loc.Height==0)
                .Select(t => t.DistanceFromEnd ?? int.MaxValue))
            .Min();

    internal static Thing2[][] Dump(this Thing2[][] grid)
    {
        foreach (var row in grid.Rows())
        {
            Console.WriteLine(string.Join(" ",row.Select( t => $"{t.DistanceFromEnd??999:D3}")));
            Console.WriteLine(string.Join(".", row.Select(t => $"{t.Height:D3}")));
        }

        return grid;
    }
}
