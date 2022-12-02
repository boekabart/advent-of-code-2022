using day1;

var calorieList = D1P1.GetCalorieList(Input.RawCalorieList);
var outputQ1 = D1P1.GetCaloriesOfHighestElve(calorieList);
Console.WriteLine($"Day 1, Part 1: Highest Elve carries {outputQ1} Calories");

var outputQ2 = D1P2.GetCaloriesOfHighestElves(calorieList, 3);
Console.WriteLine($"Day 1, Part 2: Highest 3 Elves carry {outputQ2} Calories");
