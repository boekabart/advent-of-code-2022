using System.Collections.Immutable;
using System.Text.RegularExpressions;
using shared;

namespace day16;

internal record Thing(string Name, int FlowRate, List<string> Connections);

internal record Valve(string Name, int FlowRate, List<Valve> Connections)
{
    public override int GetHashCode() => Name.GetHashCode();
    public virtual bool Equals(Valve? other) => Name.Equals(other!.Name ?? "");
 };

internal record Snapshot(int Time, Valve Location, ImmutableHashSet<Valve> OpenValves,
    int AccumulatedFlow, ImmutableList<Valve> AllValves, ImmutableList<string> History);

public static class D16P1
{
    public static object Part1Answer(this string input) =>
        input
            .ParseThings()
            .AsValves()
            .Best()
            .AccumulatedFlow;
    
    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .NotEmptyTrimmedLines()
            .Select(TryParseAsThing);

    private static readonly Regex parseRegex = new Regex(@"\s(\w\w)\s.+=(\d+).+valves? (.+)$");
    internal static Thing TryParseAsThing(this string line)
    {
        var match = parseRegex.Match(line);
        var name = match.Groups[1].Value;
        var flow = int.Parse(match.Groups[2].Value);
        var connections = match.Groups[3].Value
            .Split(',')
            .Select(s => s.Trim())
            .ToList();
        return new Thing(name, flow, connections);
    }

    internal static ICollection<Valve> AsValves(this IEnumerable<Thing> things)
    {
        var thingDict = things.ToDictionary(th => th.Name);
        var valvesDictionary = thingDict.Values
            .Select(th => new Valve(th.Name, th.FlowRate, new()))
            .ToDictionary(v => v.Name);
        foreach (var valve in valvesDictionary.Values)
            valve.Connections.AddRange(thingDict[valve.Name].Connections.Select(name => valvesDictionary[name]));
        return valvesDictionary.Values;
    }

    internal class Graph
    {
        private readonly Dictionary<string, int> _distances = new();

        public int Distance(Valve src, Valve dst) => Distance(src, dst, ImmutableHashSet<Valve>.Empty) ?? throw new InvalidOperationException($"No distance between {src.Name} and {dst.Name}");

        private int? Distance(Valve src, Valve dst, ImmutableHashSet<Valve> forbiddenValves)
        {
            if (src == dst) return 0;
            var key = $"{src.Name}-{dst.Name}";
            if (_distances.TryGetValue(key, out var dist))
                return dist;

            var distance = src.Connections
                .Min(conn => forbiddenValves.Contains(conn)
                    ? null
                : Distance(conn, dst, forbiddenValves.Add(src))) + 1;
            if (distance.HasValue)
                _distances[key] = distance.Value;
            return distance;
        }
    }

    internal static Snapshot Best(this ICollection<Valve> things)
    {
        var graph = new Graph();
//        var allValves = things.ToImmutableDictionary(t => t.Name);
        var startSnapshot = new Snapshot(
            0,
            things.Single(t => t.Name == "AA"),
            things.Where(t => t.FlowRate == 0).ToImmutableHashSet(),
            0,
            things.OrderByDescending(t => t.FlowRate).Where(t => t.FlowRate !=0).ToImmutableList(),
            ImmutableList<string>.Empty);
        var queue = new List<(int, Snapshot)>();
        var minGuaranteedResult = 0;
        queue.Enqueue(startSnapshot, graph, minGuaranteedResult);
        var solutions = new List<Snapshot>();
        int bestSolution = int.MinValue;
        while (queue.Any())
        {
            var snapshot = queue.Dequeue();

            var maximalEndResult = snapshot.MaximalTheoreticalEndResult(graph);
            if (maximalEndResult < minGuaranteedResult)
                continue;
            
            if (maximalEndResult <= bestSolution)
                continue;

            if (snapshot.TimeLeft() == 0)
            {
                bestSolution = snapshot.AccumulatedFlow;
                Console.WriteLine(snapshot.AccumulatedFlow);
                snapshot.History.ForEach(Console.WriteLine);
                solutions.Add(snapshot);

                continue;
            }

            if (!snapshot.OpenValves.Contains(snapshot.Location))
                minGuaranteedResult = queue.Enqueue(snapshot.OpenValve(snapshot.Location), graph, minGuaranteedResult);

            foreach (var conn in snapshot.Location.Connections)
                minGuaranteedResult = queue.Enqueue(snapshot.MoveTo(conn), graph, minGuaranteedResult);
        }

        return solutions.MaxBy(snapshot => snapshot.AccumulatedFlow)!;
    }
    internal static Snapshot Dequeue(this List<(int, Snapshot Snapshot)> queue)
    {
        var iter = queue[^1].Snapshot;
        queue.RemoveAt(queue.Count-1);
        return iter;
    }
    
    private static int Enqueue(this List<(int MinResult, Snapshot Snapshot)> queue, Snapshot candidate, Graph graph, int minGuaranteedResult)
    {
        var myMaximalEndResult = candidate.MaximalTheoreticalEndResult(graph);
        if (myMaximalEndResult < minGuaranteedResult)
            return minGuaranteedResult;
        
        var myMinimumReachableResult = candidate.MinimalEndResult(graph);
        
        int pos = 0;
        for (; pos < queue.Count; pos++)
        {
            var otherMin = queue[pos].MinResult;
            if (otherMin > myMinimumReachableResult)
                break;
        }
        queue.Insert(pos, (myMinimumReachableResult, candidate));
        return Math.Max(minGuaranteedResult, myMinimumReachableResult);
    }

    internal static Snapshot OpenValve(this Snapshot snapshot, Valve valve)
        => snapshot.ProgressTime() with { OpenValves = snapshot.OpenValves.Add(valve), History = snapshot.History.Add($"open {valve.Name}")};

    internal static Snapshot MoveTo(this Snapshot snapshot, Valve valve)
        => snapshot.ProgressTime() with { Location = valve, History = snapshot.History.Add($"move {valve.Name}")};

    internal static Snapshot ProgressTime(this Snapshot snapshot) =>
        snapshot with
        {
            Time = snapshot.Time + 1,
            AccumulatedFlow = snapshot.AccumulatedFlow + snapshot.OpenValves.Sum(v => v.FlowRate)
        };

    internal static int TimeLeft(this Snapshot snapshot)
        => 30 - snapshot.Time;
    
    internal static int MinimalEndResult(this Snapshot snapshot)
        => snapshot.AccumulatedFlow + snapshot.TimeLeft() * snapshot.OpenValves.Sum(v => v.FlowRate);

    private static int MinimalEndResult0(this Snapshot snapshot, Graph graph)
    {
        var timeLeft = snapshot.TimeLeft();
        var potential = 0;
        var closedValves = snapshot.AllValves.Except(snapshot.OpenValves).ToList();
        var location = snapshot.Location;
        foreach (var s in closedValves)
        {
            // The Move
            timeLeft -= graph.Distance(location, s);
            if (timeLeft <= 1)
                break;
            // The Opening
            timeLeft--;
            potential += timeLeft * s.FlowRate;
            if (timeLeft <= 0)
                break;
            location = s;
        }

        return snapshot.MinimalEndResult() + potential;
    }
    private static int MinimalEndResult(this Snapshot snapshot, Graph graph)
    {
        var timeLeft = snapshot.TimeLeft();
        var potential = 0;
        var closedValves = snapshot.AllValves.Except(snapshot.OpenValves).ToHashSet();
        var location = snapshot.Location;
        
        while (timeLeft >= 0 && closedValves.Any())
        {
            var (nextValve, dist, valvePotential) =
                closedValves.Select(cv => (Valve: cv, Dist: graph.Distance(location, cv)))
                    .Select(pair => (pair.Valve, pair.Dist,
                        Potential: (timeLeft - (1 + pair.Dist)) * pair.Valve.FlowRate))
                    .MaxBy(trp => trp.Potential);
            if (valvePotential < 0)
                break;
            potential += valvePotential;
            timeLeft -= dist;
            timeLeft--;
            location = nextValve;
            closedValves.Remove(nextValve);
        }

        return snapshot.MinimalEndResult() + potential;
    }
    
    internal static int MaximalTheoreticalEndResult(this Snapshot snapshot, Graph graph)
    {
        var timeLeft = snapshot.TimeLeft();
        if (timeLeft < 1)
            return snapshot.MinimalEndResult();
        
        var closedValves = snapshot.AllValves.Except(snapshot.OpenValves).ToList();

        var potential = closedValves.Select(cv => (Valve: cv,
                    Dist: graph.Distance(snapshot.Location, cv)
                ))
            .Select(pair => (timeLeft - (1 + pair.Dist)) * pair.Valve.FlowRate)
            .Where(pot => pot > 0)
            .Sum();

        var potential2 = 0;
        foreach (var s in closedValves)
        {
            potential2 += timeLeft * s.FlowRate;
            timeLeft -= 2;
            if (timeLeft <= 0)
                break;
        }
        return snapshot.MinimalEndResult() + Math.Min(potential, potential2);
    }
}