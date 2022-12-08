using FluentAssertions;
using Xunit;

namespace day2;

public class D2P2Tests
{
    private const string Input = @"
A Y
B X
C Z
";

    [Fact]
    public static void ParsingIsOk()
    {
        var actual = Input.ParsePredictions().ToArray();
        actual.Should().HaveCount(3);
        actual.Skip(0).First().Opponent.Should().Be(Move.Rock);
        actual.Skip(1).First().Opponent.Should().Be(Move.Paper);
        actual.Skip(2).First().Opponent.Should().Be(Move.Scissors);
        actual.Skip(0).First().Result.Should().Be(Result.Draw);
        actual.Skip(1).First().Result.Should().Be(Result.Opponent);
        actual.Skip(2).First().Result.Should().Be(Result.You);
    }

    [InlineData(Result.Draw, Move.Rock)]
    [InlineData(Result.Draw, Move.Paper)]
    [InlineData(Result.Draw, Move.Scissors)]
    [InlineData(Result.You, Move.Rock)]
    [InlineData(Result.You, Move.Paper)]
    [InlineData(Result.You, Move.Scissors)]
    [InlineData(Result.Opponent, Move.Rock)]
    [InlineData(Result.Opponent, Move.Paper)]
    [InlineData(Result.Opponent, Move.Scissors)]
    [Theory]
    public static void HinUndHer( Result result, Move opponentMove)
    {
        new Prediction(opponentMove, result)
            .MapToRound()
            .Result()
            .Should().Be(result);
    }

    [Fact]
    public static void GetTotalScoreIsOk()
    {
        Input.ParsePredictions()
            .MapToRounds()
            .GetTotalScore()
            .Should().Be(12);
    }
}