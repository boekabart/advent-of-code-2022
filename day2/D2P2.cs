using System.Diagnostics;

namespace day2;

internal record Prediction(Move Opponent, Result Result);

internal static class D2P2
{
    public static int Part2Answer(this string input) =>
        input
            .ParsePredictions()
            .MapToRounds()
            .GetTotalScore();

    public static IEnumerable<Prediction> ParsePredictions(this string input) =>
        input.Split('\n').Select(TryParsePrediction).OfType<Prediction>();

    public static Prediction? TryParsePrediction(string line)
    {
        var chars = line.Trim().Split(' ');
        if (chars.Length != 2) return null;
        var opponent = D2P1.TryParseOpponentMove(chars[0]);
        var result = TryParseResult(chars[1]);
        if (opponent != null && result != null)
            return new Prediction(opponent.Value, result.Value);
        return null;
    }

    private static Result? TryParseResult(string c) =>
        c switch { "X" => Result.Opponent, "Y" => Result.Draw, "Z" => Result.You, _ => null };

    public static IEnumerable<Round> MapToRounds(this IEnumerable<Prediction> predictions) => predictions.Select(MapToRound);

    public static Round MapToRound(this Prediction prediction) => (prediction.Result, prediction.Opponent) switch
    {
        (Result.Draw, _) => new Round(prediction.Opponent, prediction.Opponent),
        (Result.Opponent, Move.Rock) => new Round(prediction.Opponent, Move.Scissors),
        (Result.Opponent, Move.Paper) => new Round(prediction.Opponent, Move.Rock),
        (Result.Opponent, Move.Scissors) => new Round(prediction.Opponent, Move.Paper),
        (Result.You, Move.Rock) => new Round(prediction.Opponent, Move.Paper),
        (Result.You, Move.Paper) => new Round(prediction.Opponent, Move.Scissors),
        (Result.You, Move.Scissors) => new Round(prediction.Opponent, Move.Rock),

        _ => throw new UnreachableException()
    };

}