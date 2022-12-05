using FluentAssertions;
using Xunit;

namespace day5;

public class D5P1Tests
{
    [InlineData("[A] [B]    ","AB.")]
    [InlineData("[A] [B]","AB")]
    [InlineData("    [B]",".B")]
    [InlineData("",null)]
    [InlineData(" 1   2   3   4   5   6   7   8   9 ",null)]
    [InlineData("move 2 from 8 to 2",null)]
    [Theory]
    public void ParseLineOfBoxesTest(string line, string? expectedThing)
    {
        var actualThing = line.TryParseLineOfBoxes();
        if (expectedThing is null)
        {
            actualThing.Should().BeNull();
            return;
        }

        actualThing.Select(str => str is null ? '.' : str[0]).Should().BeEquivalentTo(expectedThing);
    }

    [Fact]
    public void ParseInputTest()
    {
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
