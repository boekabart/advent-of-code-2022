using shared;

namespace day13;

public static class D13P2
{
    internal static Thing Divider1 = "[[2]]".TryParseAsThing();
    internal static Thing Divider2 = "[[6]]".TryParseAsThing();

    public static long Part2Answer(this string input) =>
        input
            .ParseThings()
            .Prepend(Divider1)
            .Prepend(Divider2)
            .OrderBy(x => x)
            .Select((thing, index) => (Thing: thing, Index: index + 1))
            .Where(pair => pair.Thing == Divider1 || pair.Thing == Divider2)
            .Select(pair => pair.Index)
            .Multiplied();
}
