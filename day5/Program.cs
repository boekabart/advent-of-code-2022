using day5;

var stacks = Input.RawInput.ParseBoxes().AsStacks();
var program = Input.RawInput.ParseProgram();
var newStacks = program.Execute(stacks);
var result = newStacks.TopCrates();
Console.WriteLine($"The top crates after the program are {result}");

stacks = Input.RawInput.ParseBoxes().AsStacks();
newStacks = program.Execute9001(stacks);
result = newStacks.TopCrates();
Console.WriteLine($"The top crates after the program executes on the 9001 unit are {result}");
