using FluentAssertions;
using Xunit;

namespace day2;

public class D2P1Tests
{
    private const string Input = @"
A Y
B X
C Z
";

    [Fact]
    void ParsingIsOk()
    {
        var actual = D2P1.ParseRounds(Input);
        actual.Should().HaveCount(3);
        actual.Skip(0).First().Opponent.Should().Be(RPS.Rock);
        actual.Skip(1).First().Opponent.Should().Be(RPS.Paper);
        actual.Skip(2).First().Opponent.Should().Be(RPS.Scissors);
        actual.Skip(0).First().You.Should().Be(RPS.Paper);
        actual.Skip(1).First().You.Should().Be(RPS.Rock);
        actual.Skip(2).First().You.Should().Be(RPS.Scissors);
    }

    [Fact]
    void GetRawScoreIsOK()
    {
        var rounds = D2P1.ParseRounds(Input).ToArray();
        rounds[0].You.Score().Should().Be(2);
        rounds[0].Result().Score().Should().Be(6);

        rounds[1].You.Score().Should().Be(1);
        rounds[1].Result().Score().Should().Be(0);

        rounds[2].You.Score().Should().Be(3);
        rounds[2].Result().Score().Should().Be(3);
    }

    [Fact]
    void GetScoreIsOK()
    {
        var rounds = D2P1.ParseRounds(Input).ToArray();
        rounds[0].Score().Should().Be(8);
        rounds[1].Score().Should().Be(1);
        rounds[2].Score().Should().Be(6);
    }

    [Fact]
    void GetTotalScoreIsOK()
    {
        D2P1.ParseRounds(Input).GetTotalScore().Should().Be(15);
    }
}