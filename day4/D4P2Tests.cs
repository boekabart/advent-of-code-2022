using FluentAssertions;
using Xunit;

namespace day4;

public static class D4P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 4;
        var actual = Input.ExampleInput.Part2Answer();
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 806;

        var actual = Input.RawInput.Part2Answer();
        actual.Should().Be(expected);
    }
}