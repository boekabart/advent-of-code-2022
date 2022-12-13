using FluentAssertions;
using Xunit;

namespace day13;

public class D13P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 140;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 25800;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}