using FluentAssertions;
using Xunit;

namespace day4;

public static class D4P1Tests
{
    [Fact]
    public static void ParseInputLineTest()
    {
        var line = "1-2,2-3";
        var expectedThing = new RawPairAssignment(new(1, 2), new(2, 3));
        var actualThing = line.TryParseAsPairAssignment();
        actualThing.Should().Be(expectedThing);
    }

    [Fact]
    public static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParsePairAssignments().ToArray();
        things.Should().HaveCount(6);
    }

    [Fact]
    public static void ExpandTest()
    {
        var input = new RawPairAssignment(new(1, 1), new(2, 4));
        var actual = input.Expand();
        actual.FirstElveAssignment.Should().BeEquivalentTo(new[] { 1 });
        actual.SecondElveAssignment.Should().BeEquivalentTo(new[] { 2,3,4 });
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 2;
        var things = Input.ExampleInput.ParsePairAssignments();
        var expandedThings = things.Select(r => r.Expand());

        var actual = expandedThings.GetNumberOfFullyOverlappingPairs();
        actual.Should().Be(expected);
    }
}
