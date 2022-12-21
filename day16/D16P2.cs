using System.Collections.Immutable;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using shared;

namespace day16;

internal record Snapshot2(int Time1, int Time2, Valve Location1, Valve Location2, ImmutableHashSet<Valve> OpenValves,
    int AccumulatedFlow, ImmutableList<Valve> AllValves, ImmutableList<string> History);
public static class D16P2
{
    internal static bool Equivalent(this Snapshot2 one, Snapshot2 two)
    {
        return (Equivalent1(one, two) || Equivalent2(one, two))
               && one.AccumulatedFlow == two.AccumulatedFlow
               && !one.OpenValves.SymmetricExcept(two.OpenValves).Any();
    }

    private static bool Equivalent1(Snapshot2 one, Snapshot2 two)
    {
        return one.Time1 == two.Time1
               && one.Time2 == two.Time2
               && one.Location1 == two.Location1
               && one.Location2 == two.Location2;
    }
    
    private static bool Equivalent2(Snapshot2 one, Snapshot2 two)
    {
        return false && one.Time1 == two.Time2
               && one.Time2 == two.Time1
               && one.Location1 == two.Location2
               && one.Location2 == two.Location1;
    }

    public static object Part2Answer(this string input) =>
        input
            .ParseThings()
            .AsValves()
            .Best2()
            .AccumulatedFlow;

    internal static Snapshot2 Best2(this ICollection<Valve> things)
    {
        var graph = new D16P1.Graph();
//        var allValves = things.ToImmutableDictionary(t => t.Name);
        var startLocation = things.Single(t => t.Name == "AA");
        var startSnapshot = new Snapshot2(
            0,0,
            startLocation,
            startLocation,
            things.Where(t => t.FlowRate == 0).ToImmutableHashSet(),
            0,
            things.OrderByDescending(t => t.FlowRate).Where(t => t.FlowRate != 0).ToImmutableList(),
            ImmutableList<string>.Empty);
        var queue = new List<(int, Snapshot2)>();
        var minGuaranteedResult = 0;
        int bestSolution = int.MinValue;
        queue.Enqueue(startSnapshot, graph, minGuaranteedResult, bestSolution);
        var solutions = new List<Snapshot2>();
        while (queue.Any())
        {
            var snapshot = queue.Dequeue();

            var maximalEndResult = snapshot.MaximalTheoreticalEndResult(graph);
            if (maximalEndResult < minGuaranteedResult)
                continue;
            
            if (maximalEndResult <= bestSolution)
                continue;

            if (snapshot.Time1 > snapshot.Time2)
            {
                var children = new List<Snapshot2>();
                if (!snapshot.OpenValves.Contains(snapshot.Location2))
                {
                    var openValve2 = snapshot.OpenValve2();
                    children.Add(openValve2);
                    minGuaranteedResult = Enqueue(queue, openValve2, graph, minGuaranteedResult, bestSolution);
                }

                foreach (var conn in snapshot.Location2.Connections)
                {
                    var move2To = snapshot.Move2To(conn);
                    children.Add(move2To);
                    minGuaranteedResult = Enqueue(queue, move2To, graph, minGuaranteedResult, bestSolution);
                }

                var myMin = snapshot.MaximalTheoreticalEndResult(graph);
                var mins = children.Select(ch => ch.MaximalTheoreticalEndResult(graph)).ToList();
                if (mins.Any(m => m > myMin))
                {
                    myMin = snapshot.MaximalTheoreticalEndResult(graph);
                    var mm = children[0].MaximalTheoreticalEndResult(graph);
                }

                continue;
            }

            if (snapshot.TimeLeft() == 0)
            {
                bestSolution = snapshot.AccumulatedFlow;
                Console.WriteLine(snapshot.AccumulatedFlow);
                snapshot.History.ForEach(Console.WriteLine);
                solutions.Add(snapshot);
                continue;
            }

            var children1 = new List<Snapshot2>();
            if (!snapshot.OpenValves.Contains(snapshot.Location1))
            {
                var openValve1 = snapshot.OpenValve1();
                children1.Add(openValve1);
                minGuaranteedResult = Enqueue(queue, openValve1, graph, minGuaranteedResult, bestSolution);
            }

            foreach (var conn in snapshot.Location1.Connections)
            {
                var moveTo = snapshot.MoveTo(conn);
                children1.Add(moveTo);
                minGuaranteedResult = Enqueue(queue, moveTo, graph, minGuaranteedResult, bestSolution);
            }

            var myMin1 = snapshot.MaximalTheoreticalEndResult(graph);
            var mins1 = children1.Select(ch => ch.MaximalTheoreticalEndResult(graph)).ToList();
            if (mins1.Any(m => m > myMin1))
            {
                myMin1 = snapshot.MaximalTheoreticalEndResult(graph);
                var mm = children1[1].MaximalTheoreticalEndResult(graph);
            }

        }

        return solutions.MaxBy(snapshot => snapshot.AccumulatedFlow)!;
    }

    internal static Snapshot2 Dequeue(this List<(int, Snapshot2 Snapshot)> queue)
    {
        var iter = queue[^1].Snapshot;
        queue.RemoveAt(queue.Count-1);
        return iter;
    }

    private static Dictionary<int, List<Snapshot2>> _queuedItems = new();
    private static int Enqueue(this List<(int MinResult, Snapshot2 Snapshot)> queue, Snapshot2 candidate, D16P1.Graph graph, int minGuaranteedResult, int bestSolution)
    {
        var myMaximalEndResult = candidate.MaximalTheoreticalEndResult(graph);
        if (myMaximalEndResult < minGuaranteedResult)
            return minGuaranteedResult;

        if (myMaximalEndResult <= bestSolution)
            return minGuaranteedResult;
        
        var myMinimumReachableResult = candidate.MinimalEndResult(graph);
        if (!_queuedItems.TryGetValue(myMinimumReachableResult, out var list))
            list = _queuedItems[myMinimumReachableResult] = new();
        if (list.Any(i => i.Equivalent(candidate)))
            return minGuaranteedResult;
        list.Add(candidate);
        
        int pos = 0;
        int min = 0;
        int max = queue.Count;
        while (min <= max-1)
        {
            var cand = (min + max) / 2;

            var queueItem = queue[cand];
            var candMin = queueItem.MinResult;
            if (true ||candMin == myMinimumReachableResult)
            {
                var candSnap = queueItem.Snapshot;
                if (candSnap.Time1 > candidate.Time1)
                    max = cand;
                else if (candSnap.Time2 < candidate.Time2)
                    min = cand + 1;
                else if (candSnap.Time1 > candidate.Time1)
                    max = cand;
                else
                    min = cand + 1;
            }
            else if (candMin > myMinimumReachableResult)
                max = cand;
            else
                min = cand + 1;
        }
        queue.Insert(min, (myMinimumReachableResult, candidate));
        return Math.Max(minGuaranteedResult, myMinimumReachableResult);
    }

    internal static Snapshot2 OpenValve1(this Snapshot2 snapshot)
        => snapshot.ProgressTime() with { OpenValves = snapshot.OpenValves.Add(snapshot.Location1), History = snapshot.History.Add($"I open {snapshot.Location1.Name}")};

    internal static Snapshot2 OpenValve2(this Snapshot2 snapshot)
        => snapshot.ProgressTime2() with { OpenValves = snapshot.OpenValves.Add(snapshot.Location2), History = snapshot.History.Add($"E open {snapshot.Location2.Name}")};

    internal static Snapshot2 MoveTo(this Snapshot2 snapshot, Valve valve)
        => snapshot.ProgressTime() with { Location1 = valve, History = snapshot.History.Add($"I move {valve.Name}")};
    internal static Snapshot2 Move2To(this Snapshot2 snapshot, Valve valve)
        => snapshot.ProgressTime2() with { Location2 = valve, History = snapshot.History.Add($"E move {valve.Name}")};

    internal static Snapshot2 ProgressTime(this Snapshot2 snapshot) =>
        snapshot with
        {
            Time1 = snapshot.Time1 + 1,
            AccumulatedFlow = snapshot.AccumulatedFlow + snapshot.OpenValves.Sum(v => v.FlowRate)
        };
    
    internal static Snapshot2 ProgressTime2(this Snapshot2 snapshot) =>
        snapshot with
        {
            Time2 = snapshot.Time2 + 1
        };

    internal static int TimeLeft(this Snapshot2 snapshot)
        => 26 - snapshot.Time2;
    
    internal static int Time1Left(this Snapshot2 snapshot)
        => 26 - snapshot.Time1;
    
    internal static int MinimalEndResult(this Snapshot2 snapshot)
        => snapshot.AccumulatedFlow + snapshot.Time1Left() * snapshot.OpenValves.Sum(v => v.FlowRate);
    
    private static int MinimalEndResult(this Snapshot2 snapshot, D16P1.Graph graph)
    {
        var timeLeft = snapshot.TimeLeft();
        var time2Left = timeLeft;
        var time1Left = snapshot.Time1Left();
        var potential = 0;
        var closedValves = snapshot.AllValves.Except(snapshot.OpenValves).ToHashSet();
        var location1 = snapshot.Location1;
        var location2 = snapshot.Location2;
        while ((time1Left > 0 || time2Left > 0) && closedValves.Any())
        {
            var (nextValve, dist, valvePotential) =
                closedValves.Select(cv => (Valve: cv, Dist: graph.Distance(location1, cv)))
                    .Select(pair => (pair.Valve, pair.Dist,
                        Potential: (time1Left - (1 + pair.Dist)) * pair.Valve.FlowRate))
                    .MaxBy(trp => trp.Potential);

            var (nextValve2, dist2, valvePotential2) =
                closedValves.Select(cv => (Valve: cv, Dist: graph.Distance(location2, cv)))
                    .Select(pair => (pair.Valve, pair.Dist,
                        Potential: (time2Left - (1 + pair.Dist)) * pair.Valve.FlowRate))
                    .MaxBy(trp => trp.Potential);
            if (valvePotential2 > valvePotential)
            {
                time2Left -= dist2;
                time2Left--;
                if (valvePotential2 <= 0)
                    continue;
                potential += valvePotential2;
                location2 = nextValve2;
                closedValves.Remove(nextValve2);
            }
            else
            {
                time1Left -= dist;
                time1Left--;
                if (valvePotential <= 0)
                    continue;
                potential += valvePotential;
                location1 = nextValve;
                closedValves.Remove(nextValve);
            }
        }

        var minimalEndResult = snapshot.MinimalEndResult() + potential;
        return minimalEndResult;
    }
    
    internal static int MaximalTheoreticalEndResult(this Snapshot2 snapshot, D16P1.Graph graph)
    {
        var timeLeft = snapshot.TimeLeft();
        if (timeLeft < 1)
            return snapshot.MinimalEndResult();

        var closedValves = snapshot.AllValves.Except(snapshot.OpenValves);
        var valueTuples = closedValves.Select(cv => (Valve: cv,
                    Dist1: graph.Distance(snapshot.Location1, cv),
                    Dist2: graph.Distance(snapshot.Location2, cv)
                ))
            .Select(pair => (pair.Valve,
                Pot1: (snapshot.Time1Left() - (1 + pair.Dist1)) * pair.Valve.FlowRate,
                Pot2: (snapshot.TimeLeft() - (1 + pair.Dist2)) * pair.Valve.FlowRate));
        int penalty1 = 0;
        int penalty2 = 0;
        var potential = 0;
        /*while (valueTuples.Any())
        {
            var best = valueTuples.MaxBy(pair =>
                Math.Max(
                    pair.Pot1 - pair.Valve.FlowRate * penalty1,
                    pair.Pot2 - pair.Valve.FlowRate * penalty2));

            valueTuples.Remove(best);

            var pot1 = best.Pot1 - best.Valve.FlowRate * penalty1;
            var pot2 = best.Pot2 - best.Valve.FlowRate * penalty2;
            if (pot1 >= pot2)
            {
                if (pot1 <= 0)
                    break;
                potential += pot1;
                penalty1++;
            }
            else
            {
                if (pot2 <= 0)
                    break;
                potential += pot2;
                penalty2++;
            }
        }*/

        var potential2 = valueTuples
            .Select(pair => Math.Max(pair.Pot1, pair.Pot2))
            .Where(pot => pot > 0)
            .Sum();
        var result = snapshot.MinimalEndResult() + potential;
        var result2 = snapshot.MinimalEndResult() + potential2;
        return result2;//Math.Max(result, result2);
    }
}
