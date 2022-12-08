using day8;

var things = Input.RawInput.ParseThings();
var result = things.GetResult();
Console.WriteLine($"The number of trees visible from any direction is {result}");
