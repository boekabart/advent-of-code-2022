using BenchmarkDotNet.Attributes;
using FluentAssertions;
using Xunit;

namespace day14;

public class D14P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 93;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Benchmark]
    [Fact]
    public void RegressionTest()
    {
        var expected = 32041;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}
