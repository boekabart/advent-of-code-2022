using System.Text.RegularExpressions;

namespace day5;

public record ProgramStep(int NumberOfBoxes, int FromStack, int ToStack);

internal static class D5P1
{
    private static readonly Regex BoxesRegex = new(@"^((\[\w\]|   ) )*(\[\w\]|   )$", RegexOptions.Compiled);

    public static List<char?>? TryParseLineOfBoxes(this string line)
    {
        var m = BoxesRegex.Match(line);
        return !m.Success
            ? null
            : Enumerable.Range(0, (line.Length+1)/4)
                .Select(idx => line[idx * 4 + 1])
                .Select(chr => chr switch { ' ' => (char?)null, _ => chr })
                .ToList();
    }
    
    public static IEnumerable<List<char?>> ParseBoxes(this string input) =>
        input
            .Split('\n', '\r')
            .Select(TryParseLineOfBoxes)
            .OfType<List<char?>>();

    public static List<Stack<char>> AsStacks(this IEnumerable<List<char?>> input) =>
        input.Reverse().Aggregate(new List<Stack<char>>(), AddToStacks);

    public static List<Stack<char>> AddToStacks(List<Stack<char>> stacks, List<char?> boxes)
    {
        while (stacks.Count < boxes.Count) stacks.Add(new());
        foreach (var zip in boxes.Zip(stacks))
            if (zip.First.HasValue)
                zip.Second.Push(zip.First.Value);
        return stacks;
    }

    public static IEnumerable<ProgramStep> ParseProgram(this string input) =>
        input
            .Split(new[] { '\n' })
            .Select(s => s.Trim())
            .Select(TryParseAsProgramStep)
            .OfType<ProgramStep>();


    private static readonly Regex ProgramRegex =
        new(@"^move (?<count>\d+) from (?<from>\d+) to (?<to>\d+)$", RegexOptions.Compiled);

    public static ProgramStep? TryParseAsProgramStep(this string line)
    {
        var match = ProgramRegex.Match(line);
        if (!match.Success) return null;
        return new ProgramStep(
            int.Parse(match.Groups["count"].Value),
            int.Parse(match.Groups["from"].Value),
            int.Parse(match.Groups["to"].Value));
    }


    public static List<Stack<char>> Execute(this IEnumerable<ProgramStep> program, List<Stack<char>> stacks) =>
        program.Aggregate(stacks, Execute);

    public static List<Stack<char>> Execute(this List<Stack<char>> stacks, ProgramStep programStep) =>
        Execute(programStep, stacks);

    public static List<Stack<char>> Execute(this ProgramStep programStep, List<Stack<char>> stacks)
    {
        for (int q = 0; q < programStep.NumberOfBoxes; q++)
        {
            var box = stacks[programStep.FromStack - 1].Pop();
            stacks[programStep.ToStack - 1].Push(box);
        }

        return stacks;
    }

    public static string TopCrates(this List<Stack<char>> stacks) => new(stacks.Select(s => s.Peek()).ToArray());

    public static IEnumerable<T[]> Buffer<T>(this IEnumerable<T> items, int bufferSize) =>
        items.Select((item, idx) => (Item: item, Window: idx / bufferSize))
            .GroupBy(gr => gr.Window)
            .Select(gr => gr.Select(pair => pair.Item).ToArray());
}