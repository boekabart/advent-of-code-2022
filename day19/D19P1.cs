namespace day19;

internal record Thing(bool Data);

public static class D19P1
{
    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .Split(new[] {'\n'})
            .Select(TryParseAsThing)
            .OfType<Thing>();

    internal static Thing? TryParseAsThing(this string line)
    {
        return null;
    }

    internal static int GetResult(this IEnumerable<Thing> things) => things.Select(AsResult).Sum();
    internal static int AsResult(this Thing thing) => 0;
}