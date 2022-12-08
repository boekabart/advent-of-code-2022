using day6;

var lengthOfPrefix = Input.RawInput.CalculateLengthOfPrefixAndMarker();
Console.WriteLine($"On day 6, the number of characters to process before msg is {lengthOfPrefix}");
var lengthOf14Prefix = Input.RawInput.CalculateLengthOfPrefixAndMarker(14);
Console.WriteLine($"On day 6, the number of characters to process before the real msg is {lengthOf14Prefix}");
