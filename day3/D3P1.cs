using shared;

namespace day3;

public record Backpack(HashSet<char> FirstCompartment, HashSet<char> SecondCompartment);

internal static class D3P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParseBackpacks()
            .SumOfDuplicateItemPriorities();

    public static IEnumerable<Backpack> ParseBackpacks(this string input) =>
        input
            .Lines()
            .Select(TryParseAsBackpack)
            .OfType<Backpack>();

    public static Backpack? TryParseAsBackpack(this string line)
    {
        if (line.Length == 0 || line.Length % 2 != 0)
            return null;
        var half = line.Length / 2;
        return new Backpack(
            line[..half].ToHashSet(), 
            line[half..].ToHashSet());
    }

    public static int SumOfDuplicateItemPriorities(this IEnumerable<Backpack> things) => things.Select(DuplicateItemPriority).Sum();
    public static int DuplicateItemPriority(this Backpack backpack) => backpack.DuplicateItem().Priority();

    public static char DuplicateItem(this Backpack b) => b.FirstCompartment.Intersect(b.SecondCompartment).First();

    public static int Priority(this char item) => item switch
    {
        >= 'a' and <= 'z' => 1 + (int)(item - 'a'),
        >= 'A' and <= 'Z' => 27 + (int)(item - 'A'),
        _ => throw new ArgumentOutOfRangeException()
    };
}