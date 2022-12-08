using FluentAssertions;
using Xunit;

namespace day3;

public static class D3P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 70;
        var things = Input.ExampleInput.ParseBackpacks();
        var groups = things.MakeGroups();
        var actual = groups.SumOfBadgePriorities();
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 2569;
        var things = Input.RawInput.ParseBackpacks();
        var groups = things.MakeGroups();
        var actual = groups.SumOfBadgePriorities();
        actual.Should().Be(expected);
    }
}