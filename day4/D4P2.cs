namespace day4;

internal static class D4P2
{
    public static int Part2Answer(this string input) =>
        input
            .ParsePairAssignments()
            .Select(r => r.Expand())
            .GetNumberOfOverlappingPairs();

    public static int GetNumberOfOverlappingPairs(this IEnumerable<PairAssignment> things) =>
        things.Count(HaveAnyOverlap);

    public static bool HaveAnyOverlap(this PairAssignment thing) =>
        thing.FirstElfAssignment.Intersect(thing.SecondElfAssignment).Any();
}
