using FluentAssertions;
using Xunit;

namespace day5;

public class D5P1Tests
{
    [InlineData("[A] [B]    ", "AB.")]
    [InlineData("[A] [B]", "AB")]
    [InlineData("    [B]", ".B")]
    [InlineData("", null)]
    [InlineData(" 1   2   3   4   5   6   7   8   9 ", null)]
    [InlineData("move 2 from 8 to 2", null)]
    [Theory]
    public void ParseLineOfBoxesTest(string line, string? expectedThing)
    {
        var actualThing = line.TryParseLineOfBoxes();
        if (expectedThing is null)
        {
            actualThing.Should().BeNull();
            return;
        }

        actualThing.Should().NotBeNull();
        actualThing!.Select(str => str ?? '.').Should().BeEquivalentTo(expectedThing);
    }

    [Fact]
    public void ParseBoxesTest()
    {
        var lines = Input.ExampleInput.ParseBoxes().ToList();
        lines.Should().HaveCount(3);
        lines[0][0].Should().BeNull();
        lines[0][1].Should().Be('D');
        lines[0][2].Should().BeNull();
    }

    [Fact]
    public void ParseBoxesAsStacksTest()
    {
        var stacks = Input.ExampleInput.ParseBoxes().AsStacks();
        stacks.Should().HaveCount(3);
        stacks[0].Should().BeEquivalentTo(new[] { 'Z', 'N' });
    }

    [Fact]
    public void AsStacksTest()
    {
        var linesOfBoxes = new[]
        {
            new List<char?> {null, 'A', 'B'},
            new List<char?> {'C', 'D', 'E'},
        };
        var actual = linesOfBoxes.AsStacks();
        actual.Should().HaveCount(3);
        actual[0].Peek().Should().Be('C');
        actual[0].Count.Should().Be(1);
        actual[1].Peek().Should().Be('A');
        actual[1].Count.Should().Be(2);
        actual[2].Peek().Should().Be('B');
        actual[2].Count.Should().Be(2);
    }

    [Fact]
    public void TopCratesTest()
    {
        var linesOfBoxes = new[]
        {
            new List<char?> {null, 'A', 'B'},
            new List<char?> {'C', 'D', 'E'},
        };
        var stacks = linesOfBoxes.AsStacks();
        var actual = stacks.TopCrates();
        actual.Should().Be("CAB");
    }

    [Fact]
    public void ExecuteProgramStepTest()
    {
        var linesOfBoxes = new[]
        {
            new List<char?> {null, 'A', 'B'},
            new List<char?> {'C', 'D', 'E'},
        };
        var stacks = linesOfBoxes.AsStacks();
        var programStep = new ProgramStep(1, 3, 1);
        var actual = programStep.Execute(stacks);
        actual.TopCrates().Should().Be("BAE");
    }

    [Fact]
    public void ExecuteProgramTest()
    {
        var linesOfBoxes = new[]
        {
            new List<char?> {null, 'A', 'B'},
            new List<char?> {'C', 'D', 'E'},
        };
        var stacks = linesOfBoxes.AsStacks();
        var programStep1 = new ProgramStep(1, 3, 1);
        var programStep2 = new ProgramStep(1, 2, 1);
        var program = new[] { programStep1, programStep2 };
        var actual = program.Execute(stacks);
        actual.TopCrates().Should().Be("ADE");
    }

    [Fact]
    public void TryParseAsProgramStepTest()
    {
        var line = "move 3 from 1 to 2";
        var actual = line.TryParseAsProgramStep();
        actual.Should().NotBeNull();
        actual!.NumberOfBoxes.Should().Be(3);
        actual.FromStack.Should().Be(1);
        actual.ToStack.Should().Be(2);
    }

    [Fact]
    public void TryParseAsProgramStepNullTest()
    {
        var line = "[A] [B]   ";
        var actual = line.TryParseAsProgramStep();
        actual.Should().BeNull();
    }

    [Fact]
    public void AcceptanceTest()
    {
        var expected = "CMZ";
        var stacks = Input.ExampleInput.ParseBoxes().AsStacks();
        var program = Input.ExampleInput.ParseProgram();
        var newStacks = program.Execute(stacks);
        var actual = newStacks.TopCrates();
        actual.Should().Be(expected);
    }
}
