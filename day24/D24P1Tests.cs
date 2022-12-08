using FluentAssertions;
using Xunit;

namespace day24;

public static class D24P1Tests
{
    [InlineData("",null)]
    [Theory]
    public static void ParseInputLineTest(string line, Thing? expectedThing)
    {
        var actualThing = line.TryParseAsThing();
        actualThing.Should().Be(expectedThing);
    }

    [Fact]
    public static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(0);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 0;
        var things = Input.ExampleInput.ParseThings();
        var actual = things.GetResult();
        actual.Should().Be(expected);
    }
}
