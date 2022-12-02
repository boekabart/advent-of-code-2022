using FluentAssertions;
using Xunit;

namespace day5;

public class D5P1Tests
{
    [InlineData("",null)]
    [Theory]
    public void ParseInputLineTest(string line, Thing? expectedThing)
    {
        var actualThing = line.TryParseAsThing();
        actualThing.Should().Be(expectedThing);
    }

    [Fact]
    public void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(0);
    }

    [Fact]
    public void AcceptanceTest()
    {
        var expected = 0;
        var things = Input.ExampleInput.ParseThings();
        var actual = things.GetResult();
        actual.Should().Be(expected);
    }
}
