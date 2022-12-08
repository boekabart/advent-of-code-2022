using day1;

var calorieList = Input.RawCalorieList.GetCalorieList().ToArray();
var outputQ1 = calorieList.GetCaloriesOfElfWithMostCalories();
Console.WriteLine($"Day 1, Part 1: Highest Elf carries {outputQ1} Calories");

var outputQ2 = calorieList.GetCaloriesOfElvesWithMostCalories(3);
Console.WriteLine($"Day 1, Part 2: Highest 3 Elves carry {outputQ2} Calories");
