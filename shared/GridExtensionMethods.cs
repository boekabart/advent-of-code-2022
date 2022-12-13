namespace shared;

public static class GridExtensionMethods
{
    public static IEnumerable<IEnumerable<TValue>> Columns<TValue>(this TValue[][] grid) => grid.ColumnIndices().Select(grid.Column);
    public static IEnumerable<IEnumerable<TValue>> Rows<TValue>(this IEnumerable<IEnumerable<TValue>> grid) => grid;

    public static IEnumerable<TValue> Column<TValue>(this TValue[][] grid, int x) => grid.Select(row => row[x]);
    public static TValue[] Row<TValue>(this TValue[][] grid, int y) => grid[y];

    public static int Width<TValue>(this TValue[][] grid) => grid[0].Length;
    public static int Height<TValue>(this TValue[][] grid) => grid.Length;

    public static TValue Val<TValue>(this TValue[][] grid, int x, int y) => grid[y][x];
    public static TValue Val<TValue>(this IEnumerable<IEnumerable<TValue>> grid, int x, int y) => grid.Skip(y).First().Skip(x).First();

    public static IEnumerable<int> ColumnIndices<TValue>(this TValue[][] grid) => Enumerable.Range(0, grid[0].Length);

    public static IEnumerable<TResult> RowSelectMany<TInput, TResult>(this TInput[][] grid,
        Func<TInput[], int, IEnumerable<TResult>> func) =>
        grid.SelectMany(func);

    public static IEnumerable<TResult> ColumnSelectMany<TInput, TResult>(this TInput[][] grid,
        Func<TInput[], int, IEnumerable<TResult>> func) =>
        grid.ColumnIndices().Select(i => grid.Column(i).ToArray()).SelectMany(func);

    public static IEnumerable<TResult> RowSelectMany<TInput, TResult>(this TInput[][] grid,
        Func<IEnumerable<TInput>, IEnumerable<TResult>> func) =>
        grid.Rows().SelectMany(func);

    public static IEnumerable<TResult> ColumnSelectMany<TInput, TResult>(this TInput[][] grid,
        Func<IEnumerable<TInput>, IEnumerable<TResult>> func) =>
        grid.Columns().SelectMany(func);

    public static IEnumerable<TValue> GridItems<TValue>(this IEnumerable<IEnumerable<TValue>> grid)
        => grid.Rows().SelectMany(row => row);

    public static IEnumerable<TValue> Neighbors<TValue>(this TValue[][] grid, int x, int y)
    {
        if (x > 0) yield return grid.Val(x - 1, y);
        if (x < grid.Width() - 1) yield return grid.Val(x + 1, y);
        if (y > 0) yield return grid.Val(x, y - 1);
        if (y < grid.Height() - 1) yield return grid.Val(x, y + 1);
    }

    public static IEnumerable<IEnumerable<TResult>> Select<TValue, TResult>(this IEnumerable<IEnumerable<TValue>> grid,
        Func<TValue, int, int, TResult> mapWithCoordinates) =>
        grid.Rows().Select((row, y) => row.Select((item, x) => mapWithCoordinates(item, x, y)));

    public static TValue[][] AsGridArray<TValue>(this IEnumerable<IEnumerable<TValue>> grid) =>
        grid is TValue[][] gridArray ? gridArray : grid.Rows().Select(row => row.AsArray()).ToArray();

    public static TValue[] AsArray<TValue>(this IEnumerable<TValue> row) =>
        row is TValue[] array ? array : row.ToArray();
}