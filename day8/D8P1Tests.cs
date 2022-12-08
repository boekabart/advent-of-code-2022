using FluentAssertions;
using Xunit;

namespace day8;

public class D8P1Tests
{
    [InlineData("30373", new[]{3,0,3,7,3})]
    [InlineData("", null)]
    [Theory]
    public void ParseInputLineTest(string line, int[]? expectedThing)
    {
        var actualThing = line.TryParseAsThing();
        actualThing.Should().BeEquivalentTo(expectedThing);
    }

    [Fact]
    public void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(5);
        things.Should().AllSatisfy(row => row.Length.Should().Be(5));
    }

    [Fact]
    public void AcceptanceTest()
    {
        var expected = 21;
        var things = Input.ExampleInput.ParseThings();
        var actual = things.GetResult();
        actual.Should().Be(expected);
    }
}
