using day4;

namespace day3;

internal static class D3P2
{
    public static int GetNumberOfOverlappingPairs(this IEnumerable<PairAssignment> things) =>
        things.Select(HaveAnyOverlap).Count(r => r == true);

    public static bool HaveAnyOverlap(this PairAssignment thing) =>
        thing.FirstElveAssignment.Intersect(thing.SecondElveAssignment).Any();
}
