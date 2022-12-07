using day3;
using day7;

var things = Input.RawInput.ParseThings();
var paths = things.ExpandPaths();
var sizes = paths.GetDirectorySizes()
    .OrderBy(d => d.Dir.Path)
    .ToList();
var result = sizes.GetResult();
Console.WriteLine($"The sum of directory sizes for each dir that has at most 100000 bytes is {result}");
var result2 = sizes.GetSizeOfSmallestDirNeededToGetEnoughSpace();
Console.WriteLine($"The size of the smallest directory to delete to get enough space is {result2}");
