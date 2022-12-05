using day5;

var stacks = Input.RawInput.ParseBoxes().AsStacks();
var program = Input.ExampleInput.ParseProgram();
var newStacks = program.Execute(stacks);
var result = newStacks.TopCrates();
Console.WriteLine($"The top crates after the program are {result}");

