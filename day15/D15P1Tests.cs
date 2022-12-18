using FluentAssertions;
using Xunit;

namespace day15;

public static class D15P1Tests
{
    [InlineData("Sensor at x=2, y=18: closest beacon is at x=-2, y=15",2,18,-2,15)]
    [InlineData("Sensor at x=231045, y=2977983: closest beacon is at x=-362535, y=2000000", 231045, 2977983, -362535,2000000)]
    [Theory]
    internal static void ParseInputLineTest(string line, int sx, int sy, int bx, int by)
    {
        var actualThing = line.TryParseAsThing(); 
        actualThing.SensorPos.X().Should().Be(sx);
        actualThing.SensorPos.Y().Should().Be(sy);
        actualThing.BeaconPos.X().Should().Be(bx);
        actualThing.BeaconPos.Y().Should().Be(by);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(14);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 26;
        Input.ExampleInput
            .Part1Answer(10)
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 4873353;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
