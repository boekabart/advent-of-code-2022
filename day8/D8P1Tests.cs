using FluentAssertions;
using Xunit;

namespace day8;

public static class D8P1Tests
{
    [InlineData("30373", new[]{3,0,3,7,3})]
    [InlineData("", new int[0])]
    [Theory]
    public static void ParseInputLineTest(string line, int[]? expectedThing)
    {
        var actualThing = line.ParseAsLineOfTreeHeights();
        actualThing.Should().BeEquivalentTo(expectedThing);
    }

    [Fact]
    public static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseTreeHeightGrid().ToArray();
        things.Should().HaveCount(5);
        things.Should().AllSatisfy(row => row.Length.Should().Be(5));
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 21;
        var things = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = things.GetResult();
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 1807;
        var things = Input.RawInput.ParseTreeHeightGrid();
        var actual = things.GetResult();
        actual.Should().Be(expected);
    }
}
