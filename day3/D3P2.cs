using System.Diagnostics;

namespace day3;

public static class D3P2
{
    public static int Part2Answer(this string input) =>
        input
            .ParseBackpacks()
            .MakeGroups()
            .SumOfBadgePriorities();

    internal static IEnumerable<Backpack[]> MakeGroups(this IEnumerable<Backpack> backpacks) =>
        backpacks.Buffer(3);

    internal static int SumOfBadgePriorities(this IEnumerable<Backpack[]> groups) =>
        groups.Select(FindBadge)
            .Select(badge => badge.Priority())
            .Sum();

    internal static char FindBadge(this Backpack[] group) => group
        .Aggregate((HashSet<char>?) null, Intersect)
        ?.First() ?? throw new UnreachableException();

    private static HashSet<char> Intersect(HashSet<char>? prev, Backpack backpack) =>
        prev is null ? backpack.AllItems() : prev.Intersect(backpack.AllItems()).ToHashSet();

    private static HashSet<char> AllItems(this Backpack bp) =>
        bp.FirstCompartment.Union(bp.SecondCompartment).ToHashSet();

    internal static IEnumerable<T[]> Buffer<T>(this IEnumerable<T> items, int bufferSize) =>
        items.Select((item, idx) => (Item: item, Window: idx / bufferSize))
            .GroupBy(gr => gr.Window)
            .Select(gr => gr.Select(pair => pair.Item).ToArray());
}