using FluentAssertions;
using Xunit;

namespace day19;

public static class D19P1Tests
{
    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseBlueprints().ToArray();
        things.Should().HaveCount(2);
        var first = things[0];
        first.Id.Should().Be(1);
        first.Robots.Should().HaveCount(4);
        
        first.Robots[0].Makes.Should().Be("ore");
        first.Robots[0].Costs.Should().HaveCount(1);
        first.Robots[0].Costs[0].Material.Should().Be("ore");
        first.Robots[0].Costs[0].Amount.Should().Be(4);
        
        first.Robots[2].Makes.Should().Be("obsidian");
        first.Robots[2].Costs.Should().HaveCount(2);
        first.Robots[2].Costs[0].Material.Should().Be("ore");
        first.Robots[2].Costs[0].Amount.Should().Be(3);
        first.Robots[2].Costs[1].Material.Should().Be("clay");
        first.Robots[2].Costs[1].Amount.Should().Be(14);
    }
    
    [Fact]
    internal static void ParseRealInputTest()
    {
        var things = Input.RawInput.ParseBlueprints().ToArray();
        things.Should().HaveCount(30);
    }

    [Fact]
    internal static void StepTest()
    {
        var things = Input.ExampleInput.ParseBlueprints().ToArray()[0];
        var outcomes = things.GenerateOutcomes(1);
        outcomes.Should().HaveCount(1);
        outcomes[0].Resources["ore"].Should().Be(1);
        var outcomes2 = things.GenerateOutcomes(2);
        outcomes2.Should().HaveCount(1);
        outcomes2[0].Resources["ore"].Should().Be(2);
    }

    [InlineData(1,9)]
    [InlineData(2,12)]
    [Theory]
    internal static void GeodesTest(int blueprint, int expectedGeodes)
    {
        var things = Input.ExampleInput.ParseBlueprints().ToArray()[blueprint-1];
        var actualGeodes = things.GenerateGeodes(24);
        actualGeodes.Should().Be(expectedGeodes);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 33;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact(Skip = "ToDo")]
    internal static void RegressionTest()
    {
        var expected = 42;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
