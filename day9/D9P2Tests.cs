using FluentAssertions;
using Xunit;

namespace day9;

public class D9P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 1;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void AcceptanceTest2()
    {
        var expected = 36;
        Input.ExampleInput2
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact(Skip = "ToDo")]
    internal static void RegressionTest()
    {
        var expected = 42;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}