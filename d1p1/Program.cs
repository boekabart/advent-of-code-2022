using d1p1;

var calorieList = D1P1.GetCalorieList(Input.RawCalorieList);
var outputQ1 = D1P1.GetCaloriesOfHighestElve(calorieList);
Console.WriteLine($"Day 1, Part 1: Highest Elve carries {outputQ1} Calories");
