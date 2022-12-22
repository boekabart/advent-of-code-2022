using shared;

namespace day18;

internal record Thing(int X, int Y, int Z);
internal record Direction(int dX, int dY, int dZ);

internal record Side(Thing Thing, Direction Dir);

public static class D18P1
{
    public static object Part1Answer(this string input) =>
        input
            .ParseThings()
            .ToHashSet()
            .ExposedSides()
            .Count();

    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .NotEmptyTrimmedLines()
            .Select(ParseAsThing);

    internal static Thing ParseAsThing(this string line)
    {
        var split = line.Split(',').Select(int.Parse).ToArray();
        return new(split[0], split[1], split[2]);
    }

    internal static IEnumerable<Side> ExposedSides(this HashSet<Thing> things)
    {
        return things.SelectMany(Sides).Where(side => !things.Contains(side.OtherThing()));
    }

    internal static Thing OtherThing(this Side side) => new(
        side.Thing.X + side.Dir.dX,
        side.Thing.Y + side.Dir.dY,
        side.Thing.Z + side.Dir.dZ);

    internal static IEnumerable<Side> Sides(this Thing thing) => Directions.Select(dir => new Side(thing, dir));

    private static readonly Direction[] Directions =
    {
        new(0, 0, 1),
        new(0, 0, -1),
        new(0, 1, 0),
        new(0, -1, 0),
        new(1, 0, 0),
        new(-1, 0, 0),
    };
}