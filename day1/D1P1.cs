using shared;

namespace day1;

internal static class D1P1
{
    public static IEnumerable<int?> GetCalorieList(string input)
    {
        return input
            .TrimmedLines()
            .AsIntsOrNulls();
    }

    private record struct Aggregate(int SoFar = 0, int Max = 0);

    public static int GetCaloriesOfElfWithMostCalories(IEnumerable<int?> input)
    {
        return input
            .Aggregate(new Aggregate(), DoAggregate)
            .Max;
    }

    private static Aggregate DoAggregate(this Aggregate prev, int? number) =>
        number is null ? prev.Reset() : prev.Add(number.Value);
    private static Aggregate Reset(this Aggregate aggregate) => aggregate with {SoFar = 0};
    private static Aggregate Add(this Aggregate aggregate, int number)
    { 
        var newValue = aggregate.SoFar + number;
        return new(newValue, Math.Max(aggregate.Max, newValue));
    }
}