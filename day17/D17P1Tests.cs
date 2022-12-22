using FluentAssertions;
using Xunit;

namespace day17;

public static class D17P1Tests
{

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 3068;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }
    
    [Fact]
    internal static void RegressionTest()
    {
        var expected = 3127;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
