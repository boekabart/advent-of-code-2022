using FluentAssertions;
using Xunit;

namespace day18;

public static class D18P1Tests
{
    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(13);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 64;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 3396;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
