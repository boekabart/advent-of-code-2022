using shared;

namespace day6;

public static class D6P2
{
    public static int Part2Answer(this string input) =>
        input.CalculateLengthOfPrefixAndMarker14()!.Value;

    internal static int? CalculateLengthOfPrefixAndMarker14(this string input) =>
        input.CalculateLengthOfPrefixAndMarker(14);
}
