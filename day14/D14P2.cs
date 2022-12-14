namespace day14;

public static class D14P2
{
    public static object Part2Answer(this string input) =>
        input
            .ParseRocks()
            .FillInTheBlanks()
            .ToArray()
            .AsGridWithFloor()
            //.Dump()
            .CountHowManySandCanBeSpawnedWithoutSpilling();

    internal static Grid AsGridWithFloor(this ICollection<Coordinate> rockCoordinates)
    {
        var maxY = rockCoordinates.Max(c => c.Y) + 2;
        var minX = rockCoordinates.Min(c => c.X);
        if (minX > 500 - maxY)
            minX = 500 - maxY;
        var maxX = rockCoordinates.Max(c => c.X);
        if (maxX < 500 + maxY)
            maxX = 500 + maxY;

        var width = 1 + (maxX - minX);
        var height = maxY + 1;
        var gridArray = Enumerable.Range(0, height)
            .Select(_ => Enumerable.Range(0, width)
                .Select(_ => '.')
                .ToArray())
            .ToArray();
        foreach (var rc in rockCoordinates)
            gridArray[rc.Y][rc.X - minX] = '#';
        var source = new Coordinate(500 - minX, 0);
        gridArray[source.Y][source.X] = 'x';

        for (var x = 0; x < width; x++)
            gridArray[height-1][x] = '=';

        return new Grid(gridArray, source);
    }

}
