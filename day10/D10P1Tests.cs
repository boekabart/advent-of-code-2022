using FluentAssertions;
using Xunit;

namespace day10;

public static class D10P1Tests
{
    [InlineData("noop", 1)]
    [InlineData("", null)]
    [InlineData("addx -11", 2, -11)]
    [InlineData("addx 0",2, 0)]
    [Theory]
    internal static void ParseInputLineTest(string line, int? duration, int deltaX = 0)
    {
        var actualThing = line.TryParseAsThing();
        if (duration is null)
        {
            actualThing.Should().BeNull();
            return;
        }
        actualThing.Should().NotBeNull();
        actualThing!.Duration.Should().Be(duration);
        actualThing.DeltaX.Should().Be(deltaX);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseInstructions().ToArray();
        things.Should().HaveCount(146);
    }

    [Fact]
    internal static void ExpandTest()
    {
        var things = Input.ExampleInput.ParseInstructions().Expand().ToArray();
        things.Should().HaveCount(240);
        things[0].DeltaX.Should().Be(0);
        things[1].DeltaX.Should().Be(15);
        things[2].DeltaX.Should().Be(0);
        things[3].DeltaX.Should().Be(-11);
    }

    [Fact]
    internal static void ExecuteTest()
    {
        var things = Input.ExampleInput.ParseInstructions().Expand().Execute().ToArray();
        things.Should().HaveCount(240);
        things[19].X.Should().Be(21);
        things[59].X.Should().Be(19);
    }

    [Fact]
    internal static void GetSignalStrengthsTest()
    {
        var things = Input.ExampleInput.ParseInstructions().Expand().Execute().GetSignalStrengths().ToArray();
        things.Should().HaveCount(240);
        things[19].Should().Be(21 * 20);
        things[59].Should().Be(19 * 60);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 13140;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 11820;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RawInputLeadsTo220Signals()
    {
        var things = Input.RawInput.ParseInstructions().Expand().Execute().GetSignalStrengths().ToArray();
        things.Length.Should().BeGreaterOrEqualTo(220);
    }
}
