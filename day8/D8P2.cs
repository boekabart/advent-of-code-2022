using shared;

namespace day8;

public static class D8P2
{
    public static int Part2Answer(this string input) =>
        input
            .ParseTreeHeightGrid()
            .GetBestScenicScore();

    internal static IEnumerable<Tree> AllInnerTrees(this int[][] grid) =>
        Enumerable.Range(1, grid.Length - 2)
            .SelectMany(y => Enumerable
                .Range(1, grid[y].Length - 2)
                .Select(x => new Tree(x, y)));

    internal static int GetScenicScore(this Tree tree, int[][] grid) =>
        tree.GetScenicScoreTowardsTop(grid)
        * tree.GetScenicScoreTowardsBottom(grid)
        * tree.GetScenicScoreTowardsLeft(grid)
        * tree.GetScenicScoreTowardsRight(grid);

    internal static int GetScenicScoreTowardsTop(this Tree tree, int[][] grid) =>
        grid.Column(tree.X)
            .Take(tree.Y)
            .GetScenicScore(grid[tree.Y][tree.X]);

    internal static int GetScenicScoreTowardsBottom(this Tree tree, int[][] grid) =>
        grid.Column(tree.X)
            .Reverse()
            .Take(grid.Height() - (tree.Y + 1))
            .GetScenicScore(grid[tree.Y][tree.X]);

    internal static int GetScenicScoreTowardsLeft(this Tree tree, int[][] grid) =>
        grid.Row(tree.Y)
            .Take(tree.X)
            .GetScenicScore(grid[tree.Y][tree.X]);

    internal static int GetScenicScoreTowardsRight(this Tree tree, int[][] grid) =>
        grid.Row(tree.Y)
            .Reverse()
            .Take(grid.Height() - (tree.X + 1))
            .GetScenicScore(grid[tree.Y][tree.X]);

    private static int GetScenicScore(this IEnumerable<int> heights, int refHeight) => heights
        .Aggregate(0, (count, height) => height < refHeight ? count + 1 : 1);

    internal static int GetBestScenicScore(this int[][] grid) =>
        grid.AllInnerTrees()
            .Select(tree => tree.GetScenicScore(grid))
            .Max();

    internal static Tree GetBestScenicScoreTree(this int[][] grid) =>
        grid.AllInnerTrees()
            .MaxBy(tree => tree.GetScenicScore(grid));
}