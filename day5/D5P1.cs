using System.Diagnostics;

namespace day5;

public record ProgramStep(int NumberOfBoxes, int FromStack, int ToStack);

internal static class D5P1
{
    public static List<char?>? TryParseLineOfBoxes(this string line) =>
        throw new NotImplementedException();
    
    public static IEnumerable<List<char?>> ParseBoxes(this string input) =>
        input
            .Split(new[] {'\n'})
            .Select(TryParseLineOfBoxes)
            .OfType<List<char?>>();

    public static List<Stack<char>> AsStacks(this IEnumerable<List<char?>> input) =>
        throw new NotImplementedException();

    public static IEnumerable<ProgramStep> ParseProgram(this string input) =>
        input
            .Split(new[] { '\n' })
            .Select(s => s.Trim())
            .Select(TryParseAsProgramStep)
            .OfType<ProgramStep>();

    public static ProgramStep? TryParseAsProgramStep(this string line)
    {
        return null;
    }

    public static List<Stack<char>> Execute(this IEnumerable<ProgramStep> program, List<Stack<char>> stacks) =>
        throw new NotImplementedException();
    public static List<Stack<char>> Execute(this ProgramStep programStep, List<Stack<char>> stacks) =>
        throw new NotImplementedException();

    public static string TopCrates(this List<Stack<char>> stacks) => 
        throw new NotImplementedException();
    public static IEnumerable<T[]> Buffer<T>(this IEnumerable<T> items, int bufferSize) =>
        items.Select((item, idx) => (Item: item, Window: idx / bufferSize))
            .GroupBy(gr => gr.Window)
            .Select(gr => gr.Select(pair => pair.Item).ToArray());
}