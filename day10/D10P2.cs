using shared;

namespace day10;

public static class D10P2
{
    public static IEnumerable<string> Part2Answer(this string input) =>
        input
            .ParseInstructions()
            .Expand()
            .Execute()
            .ScanLines()
            .Render();

    internal static IEnumerable<CpuRegisters[]> ScanLines(this IEnumerable<CpuRegisters> src)
        => src.Buffer(40);

    internal static IEnumerable<string> Render(this IEnumerable<CpuRegisters[]> src)
        => src.Select(RenderLine);

    internal static string RenderLine(this CpuRegisters[] regs)
        => new(regs.Select(RenderPixel).ToArray());

    internal static char RenderPixel(this CpuRegisters reg, int scan)
        => Math.Abs(reg.X - scan) >= 2 ? '.' : '█';
}
