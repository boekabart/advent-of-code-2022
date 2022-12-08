﻿using FluentAssertions;
using Xunit;

namespace day6;

public static class D6P1Tests
{
    [InlineData(@"
mjqjpqmgbljsphdztnvjfqwrcgsmlb
", 7)]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    [InlineData("abababababababababa", null)]
    [InlineData("abcd", 4)]
    [InlineData("abc", null)]
    [Theory]
    public static void CalculateLengthOfPrefixAndMarkerTests(string line, int? expectedThing)
    {
        var actual = line.CalculateLengthOfPrefixAndMarker();
        actual.Should().Be(expectedThing);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 7;
        var actual = Input.ExampleInput.CalculateLengthOfPrefixAndMarker();
        actual.Should().Be(expected);
    }
}
