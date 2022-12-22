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

    internal static int GenerateGeodes(this Blueprint blueprint, int minutes)
    {
        return 0;
    }
}