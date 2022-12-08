using FluentAssertions;
using Xunit;

namespace day3;

public class D3P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 70;
        var things = Input.ExampleInput.ParseBackpacks();
        var groups = things.MakeGroups();
        var actual = groups.SumOfBadgePriorities();
        actual.Should().Be(expected);
    }
}