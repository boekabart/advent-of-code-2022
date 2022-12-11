using FluentAssertions;
using Xunit;

namespace day11;

public static class D11P1Tests
{
    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseMonkeys().ToArray();
        things.Should().HaveCount(4);

        var monkey0 = things[0];
        monkey0.Items.Should().HaveCount(2);
        monkey0.Items.Peek().Should().Be(79);
        monkey0.Operation(1).Should().Be(19);
        monkey0.Modulo.Should().Be(23);
        monkey0.Test(46).Should().BeTrue();
        monkey0.Test(47).Should().BeFalse();
        monkey0.TrueMonkey.Should().Be(2);
        monkey0.FalseMonkey.Should().Be(3);

        things[1].Operation(1).Should().Be(7);
        things[2].Operation(4).Should().Be(16);
        things[2].Operation(5).Should().Be(25);

        var monkey3 = things[3];
        monkey3.Items.Should().HaveCount(1);
        monkey3.Items.Peek().Should().Be(74);
        monkey3.Operation(1).Should().Be(4);
        monkey3.Modulo.Should().Be(17);
        monkey3.Test(51).Should().BeTrue();
        monkey3.Test(47).Should().BeFalse();
        monkey3.TrueMonkey.Should().Be(0);
        monkey3.FalseMonkey.Should().Be(1);
    }

    [Fact]
    internal static void ParseInputSmokeTest()
    {
        var things = Input.RawInput.ParseMonkeys().ToArray();
        things.Should().HaveCount(8);

        foreach (var monkey in things)
        {
            monkey.Operation(1);
            monkey.Test(46);
        }
    }

    [Fact]
    internal static void TestRounds()
    {
        var gameAfter1Round = Input.ExampleInput
            .ParseMonkeys()
            .ToList()
            .CreateGame()
            .PlayRounds(1);

        gameAfter1Round.Monkeys[0].Items.Should().BeEquivalentTo(new[] {20, 23, 27, 26});
        gameAfter1Round.Monkeys[1].Items.Should().HaveCount(6);
        gameAfter1Round.Monkeys[2].Items.Should().HaveCount(0);
        gameAfter1Round.Monkeys[3].Items.Should().HaveCount(0);

        var gameAfter2Rounds = gameAfter1Round.PlayRounds(1);
        gameAfter2Rounds.Monkeys[0].Items.Should().BeEquivalentTo(new[] { 695, 10,71,135,350 });
        gameAfter2Rounds.Monkeys[1].Items.Should().HaveCount(5);
        gameAfter2Rounds.Monkeys[2].Items.Should().HaveCount(0);
        gameAfter2Rounds.Monkeys[3].Items.Should().HaveCount(0);

        var gameAfter20Rounds = gameAfter2Rounds.PlayRounds(18);
        gameAfter20Rounds.Monkeys[0].Items.Should().BeEquivalentTo(new[] { 10,12,14,26,34 });
        gameAfter20Rounds.Monkeys[1].Items.Should().HaveCount(5);
        gameAfter20Rounds.Monkeys[2].Items.Should().HaveCount(0);
        gameAfter20Rounds.Monkeys[3].Items.Should().HaveCount(0);

        gameAfter20Rounds.InspectionCounts[0].Should().Be(101);
        gameAfter20Rounds.InspectionCounts[1].Should().Be(95);
        gameAfter20Rounds.InspectionCounts[2].Should().Be(7);
        gameAfter20Rounds.InspectionCounts[3].Should().Be(105);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 10605;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 50172;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
