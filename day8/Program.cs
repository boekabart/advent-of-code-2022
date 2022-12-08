using day8;

var things = Input.RawInput.ParseThings();
var result = things.GetResult();
Console.WriteLine($"The number of trees visible from any direction is {result}");
var bestScore = things.GetBestScenicScore();
Console.WriteLine($"The best scenic score is {bestScore}");
Console.WriteLine($"The best scenic tree is {things.GetBestScenicScoreTree()}");
