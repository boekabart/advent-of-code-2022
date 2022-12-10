using shared;

namespace day10;

internal record Instruction(int Duration, int DeltaX);
internal record TickAction(int DeltaX);

internal record CpuRegisters(int X = 1);

public static class D10P1
{
    public static object Part1Answer(this string input) =>
        input
            .ParseInstructions()
            .Expand()
            .Execute()
            .GetSignalStrengths()
            .GetTheSixRequestedValuesForX()
            .Sum();

    internal static IEnumerable<Instruction> ParseInstructions(this string input) =>
        input
            .TrimmedLines()
            .Select(TryParseAsThing)
            .OfType<Instruction>();

    internal static Instruction? TryParseAsThing(this string line)
    {
        if (line.Equals("noop")) return new(1, 0);
        if (line.StartsWith("addx ") && int.TryParse(line[5..], out var deltaX)) return new(2, deltaX);
        return null;
    }

    internal static IEnumerable<TickAction> Expand(this IEnumerable<Instruction> src) =>
        src.SelectMany(instruction => Enumerable.Range(0, instruction.Duration).Select(idx => new TickAction(idx * instruction.DeltaX)));

    internal static IEnumerable<CpuRegisters> Execute(this IEnumerable<TickAction> src) =>
        src.Scan(new CpuRegisters(), (reg, action, _) => (reg, action.Execute(reg)));

    internal static CpuRegisters Execute(this TickAction action, CpuRegisters regs)
        => new(X: regs.X + action.DeltaX);

    internal static IEnumerable<int> GetSignalStrengths(this IEnumerable<CpuRegisters> src) =>
        src.Select((reg, idx) => reg.X * (idx + 1));

    internal static IEnumerable<int> GetTheSixRequestedValuesForX(this IEnumerable<int> src) =>
        src
            .Buffer(40)
            .Take(6)
            .Select(buf => buf[19]);
}