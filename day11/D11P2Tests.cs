using FluentAssertions;
using Xunit;

namespace day11;

public class D11P2Tests
{
    [Fact]
    internal static void TestRounds()
    {
        var game = Input.ExampleInput
            .ParseMonkeys()
            .ToList()
            .CreateGame(1);

        var gameAfter1Round = game.PlayRounds(1);
        gameAfter1Round.InspectionCounts[0].Should().Be(2);
        gameAfter1Round.InspectionCounts[1].Should().Be(4);
        gameAfter1Round.InspectionCounts[2].Should().Be(3);
        gameAfter1Round.InspectionCounts[3].Should().Be(6);

        var gameAfter20Rounds = game.PlayRounds(20);
        gameAfter20Rounds.InspectionCounts[0].Should().Be(99);
        gameAfter20Rounds.InspectionCounts[1].Should().Be(97);
        gameAfter20Rounds.InspectionCounts[2].Should().Be(8);
        gameAfter20Rounds.InspectionCounts[3].Should().Be(103);

        var gameAfter1000Rounds = game.PlayRounds(1000);
        gameAfter1000Rounds.InspectionCounts[0].Should().Be(5204);
        gameAfter1000Rounds.InspectionCounts[1].Should().Be(4792);
        gameAfter1000Rounds.InspectionCounts[2].Should().Be(199);
        gameAfter1000Rounds.InspectionCounts[3].Should().Be(5192);

        var gameAfter10000Rounds = game.PlayRounds(10000);
        gameAfter10000Rounds.InspectionCounts[0].Should().Be(52166);
        gameAfter10000Rounds.InspectionCounts[1].Should().Be(47830);
        gameAfter10000Rounds.InspectionCounts[2].Should().Be(1938);
        gameAfter10000Rounds.InspectionCounts[3].Should().Be(52013);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 2713310158;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 11614682178L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}