using day3;

var things = Input.RawInput.ParseBackpacks();
var result = things.GetResult();
Console.WriteLine($"The summed priority of duplicate items in the backpacks is {result}");
var groups = things.MakeGroups();
var actual = groups.SumOfBadgePriorities();
Console.WriteLine($"The summed priority of group badge items is {actual}");
