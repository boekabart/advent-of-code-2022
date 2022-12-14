using FluentAssertions;
using Xunit;

namespace day8;

public static class D8P1Tests
{
    [InlineData("30373", new[]{3,0,3,7,3})]
    [InlineData("", new int[0])]
    [Theory]
    internal static void ParseInputLineTest(string line, int[]? expectedThing)
    {
        var actualThing = line.ParseAsLineOfTreeHeights();
        actualThing.Should().BeEquivalentTo(expectedThing);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseTreeHeightGrid().ToArray();
        things.Should().HaveCount(5);
        things.Should().AllSatisfy(row => row.Length.Should().Be(5));
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 21;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 1807;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
