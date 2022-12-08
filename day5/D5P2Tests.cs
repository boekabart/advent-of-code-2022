using FluentAssertions;
using Xunit;

namespace day5;

public static class D5P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = "MCD";
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = "ZFSJBPRFP";
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}