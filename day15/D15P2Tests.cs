using FluentAssertions;
using Xunit;

namespace day15;

public class D15P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 56000011;
        Input.ExampleInput
            .Part2Answer(20)
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 11600823139120L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}