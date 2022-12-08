using FluentAssertions;
using Xunit;

namespace day2;

public static class D2P1Tests
{
    [Fact]
    public static void ParsingIsOk()
    {
        var actual = D2P1.ParseRounds(Input.ExampleInput).ToArray();
        actual.Should().HaveCount(3);
        actual.Skip(0).First().Opponent.Should().Be(Move.Rock);
        actual.Skip(1).First().Opponent.Should().Be(Move.Paper);
        actual.Skip(2).First().Opponent.Should().Be(Move.Scissors);
        actual.Skip(0).First().You.Should().Be(Move.Paper);
        actual.Skip(1).First().You.Should().Be(Move.Rock);
        actual.Skip(2).First().You.Should().Be(Move.Scissors);
    }

    [Fact]
    public static void GetRawScoreIsOk()
    {
        var rounds = D2P1.ParseRounds(Input.ExampleInput).ToArray();
        rounds[0].You.Score().Should().Be(2);
        rounds[0].Result().Score().Should().Be(6);

        rounds[1].You.Score().Should().Be(1);
        rounds[1].Result().Score().Should().Be(0);

        rounds[2].You.Score().Should().Be(3);
        rounds[2].Result().Score().Should().Be(3);
    }

    [Fact]
    public static void GetScoreIsOk()
    {
        var rounds = D2P1.ParseRounds(Input.ExampleInput).ToArray();
        rounds[0].Score().Should().Be(8);
        rounds[1].Score().Should().Be(1);
        rounds[2].Score().Should().Be(6);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        D2P1.ParseRounds(Input.ExampleInput).GetTotalScore().Should().Be(15);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 15523;
        var rounds = D2P1.ParseRounds(Input.RawStrategyList);
        var totalScore = rounds.GetTotalScore();
        totalScore.Should().Be(expected);
    }
}