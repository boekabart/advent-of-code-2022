using System.Reflection.Metadata.Ecma335;

namespace shared;

public static class MoreLinq
{
    public static IEnumerable<TResult> Scan<TInput, TState, TResult>(this IEnumerable<TInput> src,
        Func<TState, TInput, int, (TResult, TState)> mapFunc) => src.Scan(default, mapFunc!);

    public static IEnumerable<TResult> Scan<TInput, TState, TResult>(this IEnumerable<TInput> src, TState state,
        Func<TState, TInput, int, (TResult, TState)> mapFunc)
    {
        foreach (var (input, index) in src.WithIndex())
        {
            (var result, state) = mapFunc(state, input, index);
            yield return result;
        }
    }

    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        => self.Select((item, index) => (item, index));

    public static IEnumerable<string> TrimmedLines(this string multiLineString) =>
        multiLineString.Split(new[] {'\n'})
            .Select(s => s.Trim());

    public static IEnumerable<string> Lines(this string multiLineString) =>
        multiLineString
            .Replace("\r\n", "\n")
            .Split(new[] { '\n' });

    public static IEnumerable<string> NotEmptyLines(this string multiLineString) =>
        multiLineString
            .Lines()
            .Where(s => s.Length > 0);

    public static IEnumerable<string> NotEmptyTrimmedLines(this string multiLineString) =>
        multiLineString
            .TrimmedLines()
            .Where(s => s.Length > 0);

    /// <summary>
    /// Checks whether an enumerable is completely distinct
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <returns>true if they are all distinct, false if there are duplicates, true if there are none</returns>
    public static bool AreDistinct<TItem>(this IEnumerable<TItem> items)
    {
        var hashSet = new HashSet<TItem>();
        return items.All(hashSet.Add);
    }
}