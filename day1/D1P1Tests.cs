using FluentAssertions;
using Xunit;

namespace day1;

public static class D1P1Tests
{
    [Fact]
    public static void ParsingIsOk()
    {
        var input = @"
1000
2000

3000
";
        var actual = input.GetCalorieList().ToArray();
        actual.Should().HaveCount(6);
        actual.Skip(1).First().Should().Be(1000);
        actual.Skip(3).First().Should().BeNull();
    }

    [Fact]
    public static void CountingIsOk()
    {
        var input = new int?[] {1000, 2000, null, 6000, 1000, null, 4000, 2000, 100};
        var actual = input.GetCaloriesOfElfWithMostCalories();
        actual.Should().Be(7000);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 24000;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 71124;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}