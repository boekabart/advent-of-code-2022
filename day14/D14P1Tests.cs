using FluentAssertions;
using Xunit;

namespace day14;

public static class D14P1Tests
{
    [InlineData("1,2 -> 1,3 -> 2,3",3,2,3)]
    [Theory]
    internal static void ParseInputLineTest(string line, int expectedNodeCount, int expectedLastX, int expectedLastY)
    {
        var actualThing = line.TryParseAsRock();
        actualThing.Nodes.Should().HaveCount(expectedNodeCount);
        actualThing.Nodes.Last().X.Should().Be(expectedLastX);
        actualThing.Nodes.Last().Y.Should().Be(expectedLastY);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseRocks().ToArray();
        things.Should().HaveCount(2);
        var realThings = Input.RawInput.ParseRocks().ToArray();
        realThings.Should().HaveCount(177);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 24;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 964;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
