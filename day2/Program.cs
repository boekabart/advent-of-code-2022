using day2;

var rounds = D2P1.ParseRounds(Input.RawCalorieList);
var totalScore = rounds.GetTotalScore();
Console.WriteLine($"The total score if your follow the strategy guide: {totalScore}");
