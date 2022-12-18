using shared;

namespace day15;

internal record Range(int First, int Last);

public static class D15P2
{
    public static object Part2Answer(this string input, int max = 4000000) =>
        input
            .ParseThings()
            .ToList()
            .AsArea()
            .GetUndetectedBeacon(max)
            .TuningFreq();

    internal static long TuningFreq(this Pos beaconPos) =>
        4000000L * beaconPos.X + beaconPos.Y;

    internal static Pos GetUndetectedBeacon(this Area area, int max)
    {
        var possibleLines = area.PossibleLines(max);
        var positions = possibleLines
            .SelectMany(y => area.BeaconPositions(y, max)
                .SelectMany(range => range
                    .Values()
                    .Select(x => new Pos(x, y))));

        return positions
            .Single();
    }

    internal static IEnumerable<Range> BeaconPositions(this Area area, int y, int max)
    {
        var scannedRanges = area.ScannedRanges(y).ToList();
        var beaconPositions = new Range(0, max).Except(scannedRanges)
            .ToList();
        return beaconPositions;
    }

    internal static IEnumerable<Range> ScannedRanges(this Area area, int y)
        => area.Sensors.Select(sensor => sensor.ScannedRange(y)).Where(IsValid);

    internal static Range ScannedRange(this Sensor sensor, int y)
    {
        int verticalDist = Math.Abs(sensor.Pos.Y - y);
        int min = sensor.Pos.X + verticalDist - sensor.Range;
        int max = sensor.Pos.X + sensor.Range - verticalDist;

        return new Range(min, max);
    }

    internal static IEnumerable<int> PossibleLines(this Area area, int max)
    {
        var possibleRanges = area.PossibleRanges(max)
            .ToList();
        return possibleRanges.SelectMany(Values);
    }

    internal static IEnumerable<int> Values(this Range range)
        => Enumerable.Range(range.First, 1 + (range.Last - range.First));

    internal static IEnumerable<Range> PossibleRanges(this Area area, int max)
    {
        var covered = area.Sensors.SelectMany(sens => sens.FullyCoveredLines(max))
            .ToList();
        var possibleRanges = new Range(0, max).Except(
                covered)
            .ToList();
        return possibleRanges;
    }

    internal static IEnumerable<Range> FullyCoveredLines(this Sensor sensor, int maxX)
    {
        var distFromRightEdge = Math.Abs(sensor.Pos.X - maxX);
        var coverInEachDirOnRightEdge = sensor.Range - distFromRightEdge;
        var rightRange = new Range(sensor.Pos.Y - coverInEachDirOnRightEdge, sensor.Pos.Y + coverInEachDirOnRightEdge);

        var distFromLeftEdge = Math.Abs(sensor.Pos.X);
        var coverInEachDirOnLeftEdge = sensor.Range - distFromLeftEdge;
        var leftRange = new Range(sensor.Pos.Y - coverInEachDirOnLeftEdge, sensor.Pos.Y + coverInEachDirOnLeftEdge);

        var fullyCoveredLines = leftRange.Intersect(rightRange).ToList();
        return fullyCoveredLines;
    }

    internal static IEnumerable<Range> Intersect(this Range one, Range two)
    {
        if (!one.IsValid() || !two.IsValid())
            yield break;

        var candidate = new Range(Math.Max(one.First, one.First), Math.Min(one.Last, two.Last));
        if (candidate.IsValid())
            yield return candidate;
    }

    internal static IEnumerable<Range> Union(this IEnumerable<Range> ranges)
    {
        var list = ranges.OrderBy(r => r.First).ThenBy(r => r.Last).ToList();
        if (!list.Any())
            yield break;
        if (list.Count == 1)
        {
            yield return list[0];
            yield break;
        }

        Range iter = list.First();
        foreach (var range in list.Skip(1))
        {
            if (iter.Last < range.First - 1)
            {
                yield return iter;
                iter = range;
                continue;
            }

            iter = new(iter.First, Math.Max(iter.Last,range.Last));
        }
        yield return iter;
    }

    internal static IEnumerable<Range> Except(this Range goodRange, IEnumerable<Range> exclusions)
    {
        var iter = goodRange;
        var badRanges = exclusions.Union().ToList();
        foreach (var badRange in badRanges)
        {
            if (badRange.First > iter.Last)
                continue;
            if (badRange.Last < iter.First)
                continue;
            if (badRange.First <= iter.First)
            {
                if (badRange.Last >= iter.Last)
                    yield break; // Nothing left
                // Right half left
                iter = iter with { First = badRange.Last + 1 };
                continue;
            }
            // Left half left
            if (badRange.Last >= iter.Last)
            {
                // Only left half left
                iter = iter with { Last = badRange.First - 1 };
                continue;
            }
            // left and right left
            // Yield left, iter right
            yield return iter with { Last = badRange.First - 1 };
            iter = iter with { First = badRange.Last + 1 };
        }

        if (iter.IsValid())
            yield return iter;
    }

    internal static bool IsValid(this Range range) => range.First <= range.Last;
}
