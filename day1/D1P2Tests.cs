using FluentAssertions;
using Xunit;

namespace day1;

public static class D1P2Tests
{
    [Fact]
    internal static void CountingIsOkFor1()
    {
        var input = new int?[] { 1000, 2000, null, 6000, 1000, null, 4000, 2000, 100 };
        var actual = input.GetCaloriesOfElvesWithMostCalories(1);
        actual.Should().Be(7000);
    }

    [Fact]
    internal static void CountingIsOkFor3()
    {
        var input = new int?[] { 1000, 2000, null, 6000, 1000, null, 4000, 2000, 100 };
        var actual = input.GetCaloriesOfElvesWithMostCalories(3);
        actual.Should().Be(16100);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 45000;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 204639;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}