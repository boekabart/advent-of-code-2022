namespace day4;

public static class D4P2
{
    public static int Part2Answer(this string input) =>
        input
            .ParsePairAssignments()
            .Select(r => r.Expand())
            .GetNumberOfOverlappingPairs();

    internal static int GetNumberOfOverlappingPairs(this IEnumerable<PairAssignment> things) =>
        things.Count(HaveAnyOverlap);

    internal static bool HaveAnyOverlap(this PairAssignment thing) =>
        thing.FirstElfAssignment.Intersect(thing.SecondElfAssignment).Any();
}
