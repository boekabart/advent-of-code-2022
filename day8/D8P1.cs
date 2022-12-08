namespace day8;

public record struct Tree(int X, int Y);

internal static class D8P1
{
    public static int[][] ParseThings(this string input) =>
        input
            .Split(new[] {'\n'})
            .Select(s => s.Trim())
            .Select(TryParseAsThing)
            .OfType<int[]>()
            .ToArray();

    public static int[]? TryParseAsThing(this string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return null;
        return line.Select(ch =>
            ch switch { >= '0' and <= '9' => ch - '0', _ => throw new InvalidOperationException($"Not a number {ch}") }).ToArray();
    }

    public static int GetResult(this int[][] grid) => grid.TreesVisibleFromAnyDirection().Count();

    public static IEnumerable<Tree> TreesVisibleFromAnyDirection(this int[][] grid) =>
        grid.TreesVisibleFromTop()
            .Concat(grid.TreesVisibleFromBottom())
            .Concat(grid.TreesVisibleFromLeft())
            .Concat(grid.TreesVisibleFromRight()).Distinct();
    public static IEnumerable<Tree> TreesVisibleFromTop(this int[][] grid) =>
        Enumerable.Range(0, grid[0].Length).SelectMany(grid.TreesVisibleFromTop);
    public static IEnumerable<Tree> TreesVisibleFromBottom(this int[][] grid) =>
        Enumerable.Range(0, grid[0].Length).SelectMany(grid.TreesVisibleFromBottom);
    public static IEnumerable<Tree> TreesVisibleFromLeft(this int[][] grid) =>
       grid.SelectMany(TreesVisibleFromLeft);
    public static IEnumerable<Tree> TreesVisibleFromRight(this int[][] grid) =>
        grid.SelectMany(TreesVisibleFromRight);

    public static IEnumerable<Tree> TreesVisibleFromTop(this int[][] array, int x)
    {
        var max = -1;
        var column = array.Select(row => row[x]);
        return column.Select((height, y) => Iterate(height, x, y, ref max)).OfType<Tree>();
    }

    public static IEnumerable<Tree> TreesVisibleFromBottom(this int[][] array, int x)
    {
        var max = -1;
        var column = array.Select(row => row[x]);
        return column.Reverse().Select((height, y) => Iterate(height, x, array.Length-(y+1), ref max)).OfType<Tree>();
    }

    public static IEnumerable<Tree> TreesVisibleFromLeft(this int[] row, int y)
    {
        var max = -1;
        return row.Select((height, x) => Iterate(height, x, y, ref max)).OfType<Tree>();
    }

    public static IEnumerable<Tree> TreesVisibleFromRight(this int[] row, int y)
    {
        var max = -1;
        return row.Reverse().Select((height, x) => Iterate(height, row.Length - (x+1), y, ref max)).OfType<Tree>();
    }

    public static Tree? Iterate(int height, int x, int y, ref int max)
    {
        if (height > max)
        {
            max = height;
            return new Tree(x, y);
        }

        return null;
    }
}