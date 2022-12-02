namespace d1p1;

internal static class D1P2
{

    private record Aggregate(int Value = 0, Aggregate? Previous = null);

    public static int GetCaloriesOfHighestElves(IEnumerable<int?> input, int elveCount)
    {
        return input
            .Aggregate(new Aggregate(), DoAggregate).GetAll().OrderByDescending( val => val).Take(elveCount).Sum();
;
    }

    private static IEnumerable<int> GetAll(this Aggregate? aggregate)
    {
        while (aggregate is not null)
        {
            yield return aggregate.Value;
            aggregate = aggregate.Previous;
        }
    }

    private static Aggregate DoAggregate(this Aggregate prev, int? number) =>
        number is null ? prev.Reset() : prev.Add(number.Value);
    private static Aggregate Reset(this Aggregate aggregate) => new(0, aggregate);
    private static Aggregate Add(this Aggregate aggregate, int number)
    {
        var newValue = aggregate.Value + number;
        return new(newValue,  aggregate.Previous);
    }
}