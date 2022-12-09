using FluentAssertions;
using Xunit;

namespace day9;

public static class D9P1Tests
{
    [InlineData("R 2", 2, 1, 0)]
    [InlineData("L 3", 3, -1, 0)]
    [InlineData("U 4", 4, 0, 1)]
    [InlineData("D 5", 5, 0, -1)]
    [InlineData("X Y", null)]
    [InlineData("What", null)]
    [InlineData("", null)]
    [Theory]
    internal static void ParseInputLineTest(string line, int? ec = null, int edx = 0, int edy = 0)
    {
        var actualThing = line.TryParseAsMove();
        if (ec is null)
        {
            actualThing.Should().BeNull();
            return;
        }

        actualThing.Should().NotBeNull();
        actualThing!.Count.Should().Be(ec.Value);
        actualThing.DeltaX.Should().Be(edx);
        actualThing.DeltaY.Should().Be(edy);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseMoves().ToArray();
        things.Should().HaveCount(8);
    }

    [Fact]
    internal static void ExpandTest()
    {
        var things = Input.ExampleInput.ParseMoves().Expand().ToArray();
        things.Should().HaveCount(24);
    }

    [InlineData(0,0,0,0)]
    [InlineData(1, 0, 0, 0)]
    [InlineData(2, 0, 1, 0)]
    [InlineData(-2, 0, -1, 0)]
    [InlineData(2, 1, 1, 1)]
    [InlineData(2, -1, 1, -1)]
    [InlineData(-2, 1, -1, 1)]
    [InlineData(-2, -1, -1, -1)]
    [Theory]
    internal static void CorrectionMoveTest(int dx, int dy, int edx, int edy)
    {
        var actual = new Distance(dx, dy).DetermineCatchupMove();
        actual.DeltaX.Should().Be(edx);
        actual.DeltaY.Should().Be(edy);
    }

    [Fact]
    internal static void ExecutionTest()
    {
        var state = Input.ExampleInput.ParseMoves().Expand().MakeTheMoves();
        state.HeadPositions.Should().HaveCount(25);
        state.TailPositions.Should().HaveCount(25);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 13;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 6642;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
