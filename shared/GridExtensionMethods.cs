namespace shared;

public static class GridExtensionMethods
{
    public static IEnumerable<IEnumerable<TValue>> Columns<TValue>(this TValue[][] grid) => grid.ColumnIndices().Select(grid.Column);
    public static IEnumerable<IEnumerable<TValue>> Rows<TValue>(this IEnumerable<IEnumerable<TValue>> grid) => grid;

    public static IEnumerable<TValue> Column<TValue>(this TValue[][] grid, int x) => grid.Select(row => row[x]);

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

    public static IEnumerable<IEnumerable<TResult>> Select<TValue, TResult>(this IEnumerable<IEnumerable<TValue>> grid,
        Func<TValue, int, int, TResult> mapWithCoordinates) =>
        grid.Rows().Select((row, y) => row.Select((item, x) => mapWithCoordinates(item, x, y)));

    public static TValue[][] AsGridArray<TValue>(this IEnumerable<IEnumerable<TValue>> grid) =>
        grid is TValue[][] gridArray ? gridArray : grid.Rows().Select(row => row.AsArray()).ToArray();

    public static TValue[] AsArray<TValue>(this IEnumerable<TValue> row) =>
        row is TValue[] array ? array : row.AsArray();
}