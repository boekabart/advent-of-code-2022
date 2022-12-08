using shared;

namespace day4;

internal record RawAssignment(int FirstSection, int LastSection);
internal record RawPairAssignment(RawAssignment FirstElveAssignment, RawAssignment SecondElveAssignment);
internal record PairAssignment(IEnumerable<int> FirstElfAssignment, IEnumerable<int> SecondElfAssignment);

public static class D4P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParsePairAssignments()
            .Select(r => r.Expand())
            .GetNumberOfFullyOverlappingPairs();

    internal static IEnumerable<RawPairAssignment> ParsePairAssignments(this string input) =>
        input
            .Lines()
            .Select(TryParseAsPairAssignment)
            .OfType<RawPairAssignment>();

    internal static RawPairAssignment? TryParseAsPairAssignment(this string line)
    {
        var ranges = line.Split(',');
        if (ranges.Length != 2) return null;
        var firstElfAssignment = TryParseAsRawAssignment(ranges[0]);
        var secondElfAssignment = TryParseAsRawAssignment(ranges[1]);
        if (firstElfAssignment is { } && secondElfAssignment is { })
            return new RawPairAssignment(firstElfAssignment, secondElfAssignment);
        return null;
    }

    internal static RawAssignment? TryParseAsRawAssignment(this string range)
    {
        var numbers = range.Split('-');
        return numbers.Length == 2 && int.TryParse(numbers[0], out var first) && int.TryParse(numbers[1], out var last)
            ? new RawAssignment(first, last)
            : null;
    }

    internal static PairAssignment Expand(this RawPairAssignment rpa) =>
        new(rpa.FirstElveAssignment.Expand(), rpa.SecondElveAssignment.Expand());

    internal static IEnumerable<int> Expand(this RawAssignment rawAssignment) =>
        Enumerable.Range(rawAssignment.FirstSection, 1 + (rawAssignment.LastSection - rawAssignment.FirstSection))
            .ToHashSet();

    internal static int GetNumberOfFullyOverlappingPairs(this IEnumerable<PairAssignment> things) =>
        things.Count(AreCompletelyContained);

    internal static bool AreCompletelyContained(this PairAssignment thing) =>
        thing.FirstElfAssignment.Union(thing.SecondElfAssignment).Count() ==
        Math.Max(thing.FirstElfAssignment.Count(), thing.SecondElfAssignment.Count());
}