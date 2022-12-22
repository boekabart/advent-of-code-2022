using System.Collections.Immutable;
using System.Text.RegularExpressions;
using shared;

namespace day19;

internal record Cost(string Material, int Amount);
internal record Robot(string Makes, Cost[] Costs);
internal record Blueprint(int Id, Robot[] Robots);

public static class D19P1
{
    public static object Part1Answer(this string input, int minutes = 24) =>
        input
            .ParseBlueprints()
            .Select(bp => bp.GenerateGeodes(minutes))
            .Sum();

    internal record BlueprintFactory(int Id, List<Robot> Robots);

    internal static IEnumerable<Blueprint> ParseBlueprints(this string input) =>
        input
            .Lines()
            .SelectMany(line => line.Split('.',':'))
            .Append("Blueprint 0")
            .Select(part => part.Trim())
            .Select(l => {Console.WriteLine(l);
                return l;
            })
            .ToList()
            .Scan<string, BlueprintFactory?, Blueprint?>(TryParseAsBlueprint)
            .OfType<Blueprint>();

    private static (Blueprint?, BlueprintFactory?) TryParseAsBlueprint(BlueprintFactory? wip, string line, int _)
        => wip.TryBlueprintLine(line).TryRobotLine(line);

    private static Blueprint Build(this BlueprintFactory fact) => new(fact.Id, fact.Robots.ToArray());

    private static Regex blueprintRegex = new Regex(@"^Blueprint (\d+)$");

    private static (Blueprint?, BlueprintFactory?) TryBlueprintLine(this BlueprintFactory? wip, string line)
    {
        var bpMatch = blueprintRegex.Match(line);
        if (!bpMatch.Success)
            return (null, wip);

        return (
            wip?.Build(),
            new(bpMatch.Groups[1].Value.AsInt(), new())
        );
    }

    private static Regex robot2Regex = new Regex(
        @"^Each (\w+) robot costs (\d+) (\w+)( and (\d+) (\w+))?$");
    private static (Blueprint?,BlueprintFactory?) TryRobotLine(this (Blueprint? Blueprit, BlueprintFactory? WiP ) iter, string line)
    {
        if (iter.Blueprit is { })
            return iter;

        if (iter.WiP is null)
            return iter;
        
        var bpMatch = robot2Regex.Match(line);
        if (!bpMatch.Success)
            return iter;
        var cost =
            bpMatch.Groups.Values
                .Skip(1)
                .Where(gr => gr.Value != "")
                .Buffer(3)
                .Select(gr =>
                    new Cost(gr[2].Value, gr[1].Value.AsInt()));
        var makes = bpMatch.Groups[1].Value;
        var robot = new Robot(makes, cost.ToArray());
        iter.WiP.Robots.Add(robot);
        return iter;
    }

    internal record Situation(
        int Time,
        Blueprint Blueprint,
        ImmutableList<string> Production,
        ImmutableDictionary<string,int> Resources);
    internal static int GenerateGeodes(this Blueprint blueprint, int minutes)
    {
        var results = blueprint.GenerateOutcomes(minutes);

        return results.Select( r=> r.Resources["geode"] * r.Blueprint.Id).Sum();
    }

    internal  static List<Situation> GenerateOutcomes(this Blueprint blueprint, int minutes)
    {
        var inventory = blueprint
            .Robots
            .Select(r => r.Makes)
            .Distinct()
            .ToImmutableDictionary(mat => mat, _ => 0);
        var situation = new Situation(0, blueprint, ImmutableList<string>.Empty.Add("ore"), inventory);

        Stack<Situation> todo = new();
        todo.Push(situation);

        List<Situation> results = new();
        while (todo.Any())
        {
            var iter = todo.Pop();
            if (iter.Time == minutes)
            {
                results.Add(iter);
                break;
                continue;
            }

            var options = iter
                .Options()
                .Ordered()
                .ToList();
            var bestOption = options.Last();
            foreach (var option in options)
                todo.Push(option);
        }

        return results;
    }

    internal static IEnumerable<Situation> Ordered(this IEnumerable<Situation> options)
    {
        return options
                .OrderBy(o => o.Production.Count(p => p=="geode"))
                .ThenByDescending(o => o.DaysUntil("geode"))
                .ThenByDescending(o => o.DaysUntil("obsidian"))
                .ThenByDescending(o => o.DaysUntil("clay"))
             //   .ThenByDescending(o => o.DaysUntil("ore"))
            ;
    }

    internal static int DaysUntil(this Situation situation, string robot)
    {
        var cost = situation.Blueprint.Robots.Single(r => r.Makes == robot).Costs;
        return cost.Max(cost => situation.DaysUntilIHave(cost));
    }

    internal static int DaysUntilIHave(this Situation situation, Cost cost)
    {
        var extraNeeded = cost.Amount - situation.Resources[cost.Material];
        if (extraNeeded < 0)
            return 0;
        var production = situation.Production.Count(pr => pr == cost.Material);
        if (production == 0)
            return int.MaxValue;
        if (extraNeeded % production == 0)
            return extraNeeded / production;
        return 1 + (extraNeeded / production);
    }

    internal static IEnumerable<Situation> Options(this Situation situation)
    {
        // Returns options in least desireable order - robots that make the most scarce resource first
        var situationAfterRobotsHaveWorked = situation.LetRobotsWork();
        return situation
            .AffordableRobots()
            .OrderByDescending(fo => situationAfterRobotsHaveWorked.Resources[fo.Makes])
            .Select(fo => situationAfterRobotsHaveWorked.CreateRobot(fo))
            .Prepend(situationAfterRobotsHaveWorked);
    }

    internal static IEnumerable<Robot> AffordableRobots(this Situation situation)
        => situation.Blueprint.Robots.Where(ro => ro.IsAffordable(situation.Resources));

    internal static Situation CreateRobot(this Situation situation, Robot robot) =>
        situation with
        {
            Production = situation.Production.Add(robot.Makes),
            Resources = robot.Costs.Aggregate(situation.Resources, Pay)
        };

    private static ImmutableDictionary<string, int> Pay(ImmutableDictionary<string, int> resources, Cost cost)
        => resources.Update(cost.Material, old => old - cost.Amount);

    internal static bool IsAffordable(this Robot robot, ImmutableDictionary<string, int> resources)
        => robot.Costs.All(cost => resources[cost.Material] >= cost.Amount);

    internal static Situation LetRobotsWork(this Situation situation) =>
        situation with
        {
            Time = situation.Time + 1,
            Resources = situation.Production.Aggregate(situation.Resources, Produce)
        };

    private static ImmutableDictionary<string, int> Produce(ImmutableDictionary<string, int> resources, string producedResource)
        => resources.Update(producedResource, old => old + 1);

    private static ImmutableDictionary<TKey, TValue> Update<TKey, TValue>(this ImmutableDictionary<TKey, TValue> dic,
        TKey key, Func<TValue, TValue> updateFunc)
        => dic.SetItem(key, updateFunc(dic[key]));
}