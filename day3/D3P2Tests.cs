using FluentAssertions;
using Xunit;

namespace day3;

public static class D3P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 70;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 2569;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}