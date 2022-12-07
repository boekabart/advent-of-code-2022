using day7;

var things = Input.RawInput.ParseThings();
var paths = things.ExpandPaths();
var sizes = paths.GetDirectorySizes()
    .OrderBy(d => d.Dir.Path)
    .ToList();
var sizes2 = sizes.Where(d => d.TotalSize <= 100000).ToList();
var result = sizes.GetResult();
Console.WriteLine($"The sum of directory sizes for each dir that has at most 100000 bytes is {result}");
