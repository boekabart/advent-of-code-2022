using shared;

namespace day3;

internal record Backpack(HashSet<char> FirstCompartment, HashSet<char> SecondCompartment);

public static class D3P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParseBackpacks()
            .SumOfDuplicateItemPriorities();

    internal static IEnumerable<Backpack> ParseBackpacks(this string input) =>
        input
            .Lines()
            .Select(TryParseAsBackpack)
            .OfType<Backpack>();

    internal static Backpack? TryParseAsBackpack(this string line)
    {
        if (line.Length == 0 || line.Length % 2 != 0)
            return null;
        var half = line.Length / 2;
        return new Backpack(
            line[..half].ToHashSet(), 
            line[half..].ToHashSet());
    }

    internal static int SumOfDuplicateItemPriorities(this IEnumerable<Backpack> things) => things.Select(DuplicateItemPriority).Sum();
    internal static int DuplicateItemPriority(this Backpack backpack) => backpack.DuplicateItem().Priority();

    internal static char DuplicateItem(this Backpack b) => b.FirstCompartment.Intersect(b.SecondCompartment).First();

    internal static int Priority(this char item) => item switch
    {
        >= 'a' and <= 'z' => 1 + (int)(item - 'a'),
        >= 'A' and <= 'Z' => 27 + (int)(item - 'A'),
        _ => throw new ArgumentOutOfRangeException()
    };
}