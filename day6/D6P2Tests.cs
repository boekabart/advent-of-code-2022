using FluentAssertions;
using Xunit;

namespace day6;

public static class D6P2Tests
{
    [InlineData(@"
mjqjpqmgbljsphdztnvjfqwrcgsmlb
", 19)]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    [InlineData("abababababaasdasdasdasdasdasdbabababa", null)]
    [InlineData("abcd", null)]
    [InlineData("abc", null)]
    [Theory]
    public static void CalculateLengthOfPrefixAndMarker14Tests(string line, int? expectedThing)
    {
        var actual = line.CalculateLengthOfPrefixAndMarker14();
        actual.Should().Be(expectedThing);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 19;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 2823;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}