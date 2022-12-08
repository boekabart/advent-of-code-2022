using FluentAssertions;
using Xunit;

namespace day3;

public static class D3P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 70;
        var actual = Input.ExampleInput.Part2Answer();
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 2569;
        var actual = Input.RawInput.Part2Answer();
        actual.Should().Be(expected);
    }
}