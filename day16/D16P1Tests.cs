using FluentAssertions;
using Xunit;

namespace day16;

public static class D16P1Tests
{
    [InlineData("Valve AA has flow rate=0; tunnels lead to valves DD, II, BB","AA", 0, 3, "BB")]
    [InlineData("Valve GA has flow rate=13; tunnels lead to valve AA","GA", 13, 1, "AA")]
    [Theory]
    internal static void ParseInputLineTest(string line, string name, int rate, int count, string last)
    {
        var actualThing = line.TryParseAsThing();
        actualThing.Name.Should().Be(name);
        actualThing.FlowRate.Should().Be(rate);
        actualThing.Connections.Should().HaveCount(count);
        actualThing.Connections.Last().Should().Be(last);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(10);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 1651;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 2253;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
