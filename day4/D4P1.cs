namespace day4;

public record RawAssignment(int FirstSection, int LastSection);
public record RawPairAssignment(RawAssignment FirstElveAssignment, RawAssignment SecondElveAssignment);
public record PairAssignment(IEnumerable<int> FirstElveAssignment, IEnumerable<int> SecondElveAssignment);

internal static class D4P1
{
    public static IEnumerable<RawPairAssignment> ParsePairAssignments(this string input) =>
        input
            .Split(new[] { '\n' })
            .Select(TryParseAsPairAssignment)
            .OfType<RawPairAssignment>();

    public static RawPairAssignment? TryParseAsPairAssignment(this string line)
    {
        var ranges = line.Split(',');
        if (ranges.Length != 2) return null;
        var firstElveAssignment = TryParseAsRawAssignment(ranges[0]);
        var secondElveAssignment = TryParseAsRawAssignment(ranges[1]);
        if (firstElveAssignment is { } && secondElveAssignment is { })
            return new(firstElveAssignment, secondElveAssignment);
        return null;
    }

    public static RawAssignment? TryParseAsRawAssignment(this string range)
    {
        var numbers = range.Split('-');
        return numbers.Length == 2 && int.TryParse(numbers[0], out var first) && int.TryParse(numbers[1], out var last)
            ? new(first, last)
            : null;
    }

    public static PairAssignment Expand(this RawPairAssignment rpa) =>
        new PairAssignment(rpa.FirstElveAssignment.Expand(), rpa.SecondElveAssignment.Expand());

    public static IEnumerable<int> Expand(this RawAssignment rawAssignment) =>
        Enumerable.Range(rawAssignment.FirstSection, 1 + (rawAssignment.LastSection - rawAssignment.FirstSection))
            .ToHashSet();

    public static int GetNumberOfFullyOverlappingPairs(this IEnumerable<PairAssignment> things) =>
        things.Select(AreCompletelyContained).Count(r => r == true);

    public static bool AreCompletelyContained(this PairAssignment thing) =>
        thing.FirstElveAssignment.Union(thing.SecondElveAssignment).Count() ==
        Math.Max(thing.FirstElveAssignment.Count(), thing.SecondElveAssignment.Count());
}