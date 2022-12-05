namespace day5;

internal static class D3P2
{
    public static List<Stack<char>> Execute9001(this IEnumerable<ProgramStep> program, List<Stack<char>> stacks) => program.Aggregate(stacks, Execute9001);

    public static List<Stack<char>> Execute9001(this List<Stack<char>> stacks, ProgramStep programStep) => Execute9001(programStep, stacks);

    public static List<Stack<char>> Execute9001(this ProgramStep programStep, List<Stack<char>> stacks)
    {
        Enumerable.Range(0, programStep.NumberOfBoxes)
            .Select(_ => stacks[programStep.FromStack - 1].Pop())
            .Reverse()
            .ToList()
            .ForEach(box => stacks[programStep.ToStack - 1].Push(box));
        return stacks;
    }
}
