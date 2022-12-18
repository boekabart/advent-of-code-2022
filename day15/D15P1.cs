using System.Numerics;
using System.Text.RegularExpressions;
using shared;

namespace day15;


internal record struct Pos(int X, int Y);

internal record Thing(Pos SensorPos, Pos BeaconPos);

internal record Area(ICollection<Sensor> Sensors, ICollection<Pos> KnownBeacons);

internal record Sensor(Pos Pos, int Range);
public static class D15P1
{
    public static object Part1Answer(this string input, int y = 2000000) =>
        input
            .ParseThings()
            .ToList()
            .AsArea()
            .NonBeaconPositions(y)
            .Count();

    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .NotEmptyLines()
            .Select(TryParseAsThing);

    private static readonly Regex parseRegex = new Regex(@"([-\d]+)[^-\d]+([-\d]+)[^-\d]+([-\d]+)[^-\d]+([-\d]+)", RegexOptions.Compiled);

    internal static Thing TryParseAsThing(this string line)
    {
        var matches = parseRegex.Match(line);
        var sx = int.Parse(matches.Groups[1].Value);
        var sy = int.Parse(matches.Groups[2].Value);
        var bx = int.Parse(matches.Groups[3].Value);
        var by = int.Parse(matches.Groups[4].Value);
        
        return new Thing(Pos(sx, sy),Pos(bx, by));
    }

    internal static Sensor AsSensor(this Thing src)
    {
        var range =     
            Math.Abs(src.BeaconPos.X - src.SensorPos.X) + Math.Abs(src.BeaconPos.Y - src.SensorPos.Y);
        return new(src.SensorPos, range);
    }

    internal static Area AsArea(this ICollection<Thing> things) =>
        new(
            things.Select(AsSensor).ToList(),
            things.Select(th => th.BeaconPos).ToList()
        );

    internal static Pos Pos(int x, int y) => new Pos(x,y);

    internal static int X(this Pos pos) => pos.X;
    internal static int Y(this Pos pos) => pos.Y;

    internal static IEnumerable<Pos> NonBeaconPositions(this Area area, int y)
        => area.ScannedPositions(y).Except(area.KnownBeacons);

    internal static IEnumerable<Pos> ScannedPositions(this Area area, int y)
        => area.Sensors.SelectMany(sensor => sensor.ScannedPositions(y)).Distinct();

    internal static IEnumerable<Pos> ScannedPositions(this Sensor sensor, int y)
    {
        int verticalDist = Math.Abs(sensor.Pos.Y - y);
        int min = verticalDist - sensor.Range;
        int max = sensor.Range - verticalDist;
        int count = Math.Max(0,1 + max - min);
        return Enumerable.Range(sensor.Pos.X + min, count).Select(x => Pos(x,y)).Where(pos => pos != sensor.Pos);
    }
}