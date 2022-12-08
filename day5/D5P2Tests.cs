using FluentAssertions;
using Xunit;

namespace day5;

public static class D5P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = "MCD";
        var stacks = Input.ExampleInput.ParseBoxes().AsStacks();
        var program = Input.ExampleInput.ParseProgram();
        var newStacks = program.Execute9001(stacks);
        var actual = newStacks.TopCrates();
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = "ZFSJBPRFP";
        var stacks = Input.RawInput.ParseBoxes().AsStacks();
        var program = Input.RawInput.ParseProgram();
        var newStacks = program.Execute9001(stacks);
        var actual = newStacks.TopCrates();
        actual.Should().Be(expected);
    }
}