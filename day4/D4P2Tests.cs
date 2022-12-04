using day3;
using FluentAssertions;
using Xunit;

namespace day4;

public class D4P2Tests
{

    [Fact]
    public void AcceptanceTest()
    {
        var expected = 4;
        var things = Input.ExampleInput.ParsePairAssignments();
        var expandedThings = things.Select(r => r.Expand());

        var actual = expandedThings.GetNumberOfOverlappingPairs();
        actual.Should().Be(expected);
    }
}