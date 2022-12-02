using System.Diagnostics;

namespace day2;

enum RPS
{
    INVALID = 0,
    Rock = 1,
    Paper = 2,
    Scissors = 3,
}

record Round(RPS Opponent, RPS You);

enum Result
{
    Opponent = 0,
    Draw = 3,
    You = 6,
}


internal static class D2P1
{
    public static int Score(this RPS rps) => (int) rps;
    public static int Score(this Result result) => (int) result;

    public static Result Result(this Round round) => (round.You, round.Opponent) switch
    {
        (RPS.Rock, RPS.Rock) => day2.Result.Draw,
        (RPS.Rock, RPS.Paper) => day2.Result.Opponent,
        (RPS.Rock, RPS.Scissors) => day2.Result.You,

        (RPS.Paper, RPS.Rock) => day2.Result.You,
        (RPS.Paper, RPS.Paper) => day2.Result.Draw,
        (RPS.Paper, RPS.Scissors) => day2.Result.Opponent,

        (RPS.Scissors, RPS.Rock) => day2.Result.Opponent,
        (RPS.Scissors, RPS.Paper) => day2.Result.You,
        (RPS.Scissors, RPS.Scissors) => day2.Result.Draw,

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

    private static RPS? TryParseOpponentMove(string c) =>
        c switch {"A" => RPS.Rock, "B" => RPS.Paper, "C" => RPS.Scissors, _ => null};

    private static RPS? TryParseYourMove(string c) =>
        c switch {"X" => RPS.Rock, "Y" => RPS.Paper, "Z" => RPS.Scissors, _ => null};

    public static IEnumerable<Round> ParseRounds(string input) =>
        input.Split(new[] {'\r'}).Select(TryParseRound).OfType<Round>();

    public static int GetTotalScore(this IEnumerable<Round> rounds) => rounds.Select(Score).Sum();

}