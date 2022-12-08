namespace day8;

internal static class D3P2
{
    public static IEnumerable<Tree> AllInnerTrees(this int[][] grid) =>
        Enumerable.Range(1, grid.Length - 2)
            .SelectMany(y => Enumerable.Range(1, grid[y].Length - 2).Select(x => new Tree(x, y)));

    public static int GetScenicScore(this Tree tree, int[][] grid)
    {
        return tree.GetScenicScoreTowardsTop(grid)
            * tree.GetScenicScoreTowardsBottom(grid)
            * tree.GetScenicScoreTowardsLeft(grid)
            * tree.GetScenicScoreTowardsRight(grid);
    }

    public static int GetScenicScoreTowardsTop(this Tree tree, int[][] grid)
    {
        var columnUntilTreeExclusive = grid.Select(row => row[tree.X])
            .Take(tree.Y);
        int refHeight = grid[tree.Y][tree.X];
        return columnUntilTreeExclusive.Aggregate(0, (count, height) => height < refHeight ? count + 1 : 1);
    }

    public static int GetScenicScoreTowardsBottom(this Tree tree, int[][] grid)
    {
        var columnUntilTreeExclusive = grid.Select(row => row[tree.X])
            .Reverse()
            .Take(grid.Length - (tree.Y+1));
        int refHeight = grid[tree.Y][tree.X];
        return columnUntilTreeExclusive.Aggregate(0, (count, height) => height < refHeight ? count + 1 : 1);
    }

    public static int GetScenicScoreTowardsLeft(this Tree tree, int[][] grid)
    {
        var columnUntilTreeExclusive = grid[tree.Y]
            .Take(tree.X);
        int refHeight = grid[tree.Y][tree.X];
        return columnUntilTreeExclusive.Aggregate(0, (count, height) => height < refHeight ? count + 1 : 1);
    }

    public static int GetScenicScoreTowardsRight(this Tree tree, int[][] grid)
    {
        var columnUntilTreeExclusive = grid[tree.Y]
            .Reverse()
            .Take(grid[tree.Y].Length - (tree.X + 1));
        int refHeight = grid[tree.Y][tree.X];
        return columnUntilTreeExclusive.Aggregate(0, (count, height) => height < refHeight ? count + 1 : 1);
    }

    public static int GetBestScenicScore(this int[][] grid) =>
        grid.AllInnerTrees()
            .Select(tree => tree.GetScenicScore(grid))
            .Max();

    public static Tree GetBestScenicScoreTree(this int[][] grid) =>
        grid.AllInnerTrees()
            .MaxBy(tree => tree.GetScenicScore(grid));
}
