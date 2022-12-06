using FluentAssertions;
using Xunit;

namespace day6;

public class D6P2Tests
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
    public void CalculateLengthOfPrefixAndMarker14Tests(string line, int? expectedThing)
    {
        var actual = line.CalculateLengthOfPrefixAndMarker14(14);
        actual.Should().Be(expectedThing);
    }

    [Fact]
    public void AcceptanceTest()
    {
        var expected = 19;
        var actual = Input.ExampleInput.CalculateLengthOfPrefixAndMarker14(14);
        actual.Should().Be(expected);
    }
}