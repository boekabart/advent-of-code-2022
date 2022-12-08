using FluentAssertions;
using Xunit;

namespace day2;

public static class D2P1Tests
{
    [Fact]
    internal static void ParsingIsOk()
    {
        var actual = Input.ExampleInput.ParseRounds().ToArray();
        actual.Should().HaveCount(3);
        actual.Skip(0).First().Opponent.Should().Be(Move.Rock);
        actual.Skip(1).First().Opponent.Should().Be(Move.Paper);
        actual.Skip(2).First().Opponent.Should().Be(Move.Scissors);
        actual.Skip(0).First().You.Should().Be(Move.Paper);
        actual.Skip(1).First().You.Should().Be(Move.Rock);
        actual.Skip(2).First().You.Should().Be(Move.Scissors);
    }

    [Fact]
    internal static void GetRawScoreIsOk()
    {
        var rounds = Input.ExampleInput.ParseRounds().ToArray();
        rounds[0].You.Score().Should().Be(2);
        rounds[0].Result().Score().Should().Be(6);

        rounds[1].You.Score().Should().Be(1);
        rounds[1].Result().Score().Should().Be(0);

        rounds[2].You.Score().Should().Be(3);
        rounds[2].Result().Score().Should().Be(3);
    }

    [Fact]
    internal static void GetScoreIsOk()
    {
        var rounds = Input.ExampleInput.ParseRounds().ToArray();
        rounds[0].Score().Should().Be(8);
        rounds[1].Score().Should().Be(1);
        rounds[2].Score().Should().Be(6);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 15;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 15523;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}