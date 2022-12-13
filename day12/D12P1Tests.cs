using FluentAssertions;
using shared;
using Xunit;

namespace day12;

public static class D12P1Tests
{
    [InlineData('a', 0)]
    [InlineData('z', 25)]
    [InlineData('S', 0, 0)]
    [InlineData('E', 25, null, 0)]
    [Theory]
    internal static void ParseInputCharTest(char line, int expectedHeight, int? expectedDistanceFromStart = null,
        int? expectedDistanceFromEnd = null)
    {
        var actual = line.ParseCharacter();
        actual.Height.Should().Be(expectedHeight);
        actual.DistanceFromStart.Should().Be(expectedDistanceFromStart);
        actual.DistanceFromEnd.Should().Be(expectedDistanceFromEnd);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().AsGridArray();
        things.Rows().Should().HaveCount(5);
        things.Columns().Should().HaveCount(8);
    }

    [Fact]
    internal static void Thing2Test()
    {
        var a = Input.ExampleInput
            .ParseThings()
            //.AsGridArray()
            .Select((thing, x, y) => new Thing2(x, y, thing.Height, thing.DistanceFromStart, thing.DistanceFromEnd));
        a.ToList();
        a.First().ToList();
        var q=a.Select(row => row.ToList()).ToList();
        q.AsGridArray();
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 31;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 383;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
