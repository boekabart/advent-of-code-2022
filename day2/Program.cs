using day2;

var rounds = D2P1.ParseRounds(Input.RawStrategyList);
var totalScore = rounds.GetTotalScore();
Console.WriteLine($"The total score if your follow the part 1 assumed meaning of the strategy guide: {totalScore}");

var predictions = Input.RawStrategyList.ParsePredictions();
var totalScore2 = predictions.MapToRounds().GetTotalScore();
Console.WriteLine($"The total score if your follow the actual (part 2) meaning of the strategy guide: {totalScore2}");
