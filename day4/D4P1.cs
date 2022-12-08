using shared;

namespace day4;

public record RawAssignment(int FirstSection, int LastSection);
public record RawPairAssignment(RawAssignment FirstElveAssignment, RawAssignment SecondElveAssignment);
public record PairAssignment(IEnumerable<int> FirstElfAssignment, IEnumerable<int> SecondElfAssignment);

internal static class D4P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParsePairAssignments()
            .Select(r => r.Expand())
            .GetNumberOfFullyOverlappingPairs();

    public static IEnumerable<RawPairAssignment> ParsePairAssignments(this string input) =>
        input
            .Lines()
            .Select(TryParseAsPairAssignment)
            .OfType<RawPairAssignment>();

    public static RawPairAssignment? TryParseAsPairAssignment(this string line)
    {
        var ranges = line.Split(',');
        if (ranges.Length != 2) return null;
        var firstElfAssignment = TryParseAsRawAssignment(ranges[0]);
        var secondElfAssignment = TryParseAsRawAssignment(ranges[1]);
        if (firstElfAssignment is { } && secondElfAssignment is { })
            return new RawPairAssignment(firstElfAssignment, secondElfAssignment);
        return null;
    }

    public static RawAssignment? TryParseAsRawAssignment(this string range)
    {
        var numbers = range.Split('-');
        return numbers.Length == 2 && int.TryParse(numbers[0], out var first) && int.TryParse(numbers[1], out var last)
            ? new RawAssignment(first, last)
            : null;
    }

    public static PairAssignment Expand(this RawPairAssignment rpa) =>
        new(rpa.FirstElveAssignment.Expand(), rpa.SecondElveAssignment.Expand());

    public static IEnumerable<int> Expand(this RawAssignment rawAssignment) =>
        Enumerable.Range(rawAssignment.FirstSection, 1 + (rawAssignment.LastSection - rawAssignment.FirstSection))
            .ToHashSet();

    public static int GetNumberOfFullyOverlappingPairs(this IEnumerable<PairAssignment> things) =>
        things.Count(AreCompletelyContained);

    public static bool AreCompletelyContained(this PairAssignment thing) =>
        thing.FirstElfAssignment.Union(thing.SecondElfAssignment).Count() ==
        Math.Max(thing.FirstElfAssignment.Count(), thing.SecondElfAssignment.Count());
}