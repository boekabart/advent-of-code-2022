using FluentAssertions;
using Xunit;

namespace day17;

public class D17P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 1514285714288;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 1542941176480;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}