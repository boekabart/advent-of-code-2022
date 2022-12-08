using System.Diagnostics;
using shared;

namespace day2;

public enum Move
{
    Rock = 1,
    Paper = 2,
    Scissors = 3,
}

record Round(Move Opponent, Move You);

public enum Result
{
    Opponent = 0,
    Draw = 3,
    You = 6,
}


internal static class D2P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParseRounds()
            .GetTotalScore();

    public static int Score(this Move move) => (int) move;
    public static int Score(this Result result) => (int) result;

    public static Result Result(this Round round) => (round.You, round.Opponent) switch
    {
        (Move.Rock, Move.Rock) => day2.Result.Draw,
        (Move.Rock, Move.Paper) => day2.Result.Opponent,
        (Move.Rock, Move.Scissors) => day2.Result.You,

        (Move.Paper, Move.Rock) => day2.Result.You,
        (Move.Paper, Move.Paper) => day2.Result.Draw,
        (Move.Paper, Move.Scissors) => day2.Result.Opponent,

        (Move.Scissors, Move.Rock) => day2.Result.Opponent,
        (Move.Scissors, Move.Paper) => day2.Result.You,
        (Move.Scissors, Move.Scissors) => day2.Result.Draw,

        _ => throw new UnreachableException()
    };

    public static int Score(this Round round) => round.Result().Score() + round.You.Score();

    public static Round? TryParseRound(string line)
    {
        var chars = line.Trim().Split(' ');
        if (chars.Length != 2) return null;
        var opponent = TryParseOpponentMove(chars[0]);
        var you = TryParseYourMove(chars[1]);
        if (opponent != null && you != null)
            return new Round(opponent.Value, you.Value);
        return null;
    }

    public static Move? TryParseOpponentMove(string c) =>
        c switch {"A" => Move.Rock, "B" => Move.Paper, "C" => Move.Scissors, _ => null};

    private static Move? TryParseYourMove(string c) =>
        c switch {"X" => Move.Rock, "Y" => Move.Paper, "Z" => Move.Scissors, _ => null};

    public static IEnumerable<Round> ParseRounds(this string input) =>
        input.TrimmedLines()
            .Select(TryParseRound)
            .OfType<Round>();

    public static int GetTotalScore(this IEnumerable<Round> rounds) => rounds.Select(Score).Sum();
}