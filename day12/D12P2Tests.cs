using FluentAssertions;
using Xunit;

namespace day12;

public class D12P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 29;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 377;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}