using FluentAssertions;
using Xunit;

namespace day1;

public static class D1P2Tests
{
    [Fact]
    public static void CountingIsOkFor1()
    {
        var input = new int?[] { 1000, 2000, null, 6000, 1000, null, 4000, 2000, 100 };
        var actual = input.GetCaloriesOfElvesWithMostCalories(1);
        actual.Should().Be(7000);
    }

    [Fact]
    public static void CountingIsOkFor3()
    {
        var input = new int?[] { 1000, 2000, null, 6000, 1000, null, 4000, 2000, 100 };
        var actual = input.GetCaloriesOfElvesWithMostCalories(3);
        actual.Should().Be(16100);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 204639;
        var things = Input.RawCalorieList.GetCalorieList();
        var actual = things.GetCaloriesOfElvesWithMostCalories(3);
        actual.Should().Be(expected);
    }
}