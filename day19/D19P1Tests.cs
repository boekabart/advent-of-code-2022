using FluentAssertions;
using Xunit;

namespace day19;

public static class D19P1Tests
{
    [InlineData("",null)]
    [Theory]
    internal static void ParseInputLineTest(string line, Thing? expectedThing)
    {
        var actualThing = line.TryParseAsThing();
        actualThing.Should().Be(expectedThing);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(0);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 0;
        var things = Input.ExampleInput.ParseThings();
        var actual = things.GetResult();
        actual.Should().Be(expected);
    }
}
