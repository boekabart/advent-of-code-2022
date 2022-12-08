using day1;

var calorieList = D1P1.GetCalorieList(Input.RawCalorieList).ToArray();
var outputQ1 = D1P1.GetCaloriesOfElfWithMostCalories(calorieList);
Console.WriteLine($"Day 1, Part 1: Highest Elf carries {outputQ1} Calories");

var outputQ2 = D1P2.GetCaloriesOfElvesWithMostCalories(calorieList, 3);
Console.WriteLine($"Day 1, Part 2: Highest 3 Elves carry {outputQ2} Calories");
