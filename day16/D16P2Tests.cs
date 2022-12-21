using FluentAssertions;
using Xunit;

namespace day16;

public class D16P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 1707;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 2838;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}