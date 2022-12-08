using FluentAssertions;
using Xunit;

namespace day3;

public static class D3P1Tests
{
    [Fact]
    internal static void ParseInputLineTest()
    {
        var line = "ABCdef";
        var actualThing = line.TryParseAsBackpack();
        actualThing.Should().NotBeNull();
        actualThing!.FirstCompartment.Should().BeEquivalentTo(new[] { 'A', 'B', 'C' });
        actualThing.SecondCompartment.Should().BeEquivalentTo(new[] { 'd', 'e', 'f' });
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseBackpacks().ToArray();
        things.Should().HaveCount(6);
    }

    [InlineData('a', 1)]
    [InlineData('m', 13)]
    [InlineData('B', 28)]
    [InlineData('Z', 52)]
    [Theory]
    internal static void TestPriority(char theChar, int expectedPriority)
    {
        theChar.Priority().Should().Be(expectedPriority);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 157;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 7763;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
