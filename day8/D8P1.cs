using shared;

namespace day8;

internal record struct Tree(int X, int Y);

public static class D8P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParseTreeHeightGrid()
            .NumberOfTreesVisibleFromAnyDirection();

    internal static int[][] ParseTreeHeightGrid(this string input) =>
        input
            .TrimmedLines()
            .Select(ParseAsLineOfTreeHeights)
            .Where(h => h.Length>0)
            .ToArray();

    internal static int[] ParseAsLineOfTreeHeights(this string line)
    {
        return line.Select(ch =>
                ch switch
                {
                    >= '0' and <= '9' => ch - '0',
                    _ => -1
                })
            .Where(h => h >= 0)
            .ToArray();
    }

    internal static int NumberOfTreesVisibleFromAnyDirection(this int[][] grid) => grid.TreesVisibleFromAnyDirection().Count();

    internal static IEnumerable<Tree> TreesVisibleFromAnyDirection(this int[][] grid) =>
        grid.TreesVisibleFromTop()
            .Concat(grid.TreesVisibleFromBottom())
            .Concat(grid.TreesVisibleFromLeft())
            .Concat(grid.TreesVisibleFromRight())
            .Distinct();

    internal static IEnumerable<Tree> TreesVisibleFromTop(this int[][] grid) =>
        grid.ColumnSelectMany(TreesVisibleFromTop);

    internal static IEnumerable<Tree> TreesVisibleFromBottom(this int[][] grid) =>
        grid.ColumnSelectMany(TreesVisibleFromBottom);

    internal static IEnumerable<Tree> TreesVisibleFromLeft(this int[][] grid) =>
        grid.RowSelectMany(TreesVisibleFromLeft);

    internal static IEnumerable<Tree> TreesVisibleFromRight(this int[][] grid) =>
        grid.RowSelectMany(TreesVisibleFromRight);

    internal static IEnumerable<Tree> TreesVisibleFromTop(this int[] column, int x) => column
        .Scan((int? max, int height, int y) => ReturnTreeIfHighestSoFar(height, x, y, max)).OfType<Tree>();

    internal static IEnumerable<Tree> TreesVisibleFromBottom(this int[] column, int x) => column.Reverse()
        .Scan((int? max, int height, int y) => ReturnTreeIfHighestSoFar(height, x, column.Length - (y + 1), max))
        .OfType<Tree>();

    internal static IEnumerable<Tree> TreesVisibleFromLeft(this int[] row, int y) => row
        .Scan((int? max, int height, int x) => ReturnTreeIfHighestSoFar(height, x, y, max)).OfType<Tree>();

    internal static IEnumerable<Tree> TreesVisibleFromRight(this int[] row, int y) => row.Reverse()
        .Scan((int? max, int height, int x) => ReturnTreeIfHighestSoFar(height, row.Length - (x + 1), y, max))
        .OfType<Tree>();

    internal static (Tree? Tree, int? Max) ReturnTreeIfHighestSoFar(int height, int x, int y, int? max) =>
        !max.HasValue || height > max.Value ? (new Tree(x, y), height) : (null, max);
}