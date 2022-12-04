using day3;
using day4;

var things = Input.RawInput.ParsePairAssignments();
var expandedThings = things.Select(r => r.Expand());

var actual = expandedThings.GetNumberOfFullyOverlappingPairs();
Console.WriteLine($"The number of pairs with fully overlapping assignments is {actual}");
var actual2 = expandedThings.GetNumberOfOverlappingPairs();
Console.WriteLine($"The number of pairs with overlapping assignments is {actual2}");
