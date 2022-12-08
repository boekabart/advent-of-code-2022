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