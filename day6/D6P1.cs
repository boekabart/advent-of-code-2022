using shared;

namespace day6;

internal static class D6P1
{
    public static int Part1Answer(this string input) =>
        input.CalculateLengthOfPrefixAndMarker()!.Value;

    public static int? CalculateLengthOfPrefixAndMarker(this string input) =>
        input.CalculateLengthOfPrefixAndMarker(4);

    public static int? CalculateLengthOfPrefixAndMarker(this string input, int numberOfUniqueCharactersToLookFor) =>
        input
            .NotEmptyTrimmedLines()
            .First()
            .CalculateLengthOfPrefixAndMarkerOnTrimmedString(numberOfUniqueCharactersToLookFor);

    public static int? CalculateLengthOfPrefixAndMarkerOnTrimmedString(this string input,
        int numberOfUniqueCharactersToLookFor)
    {
        if (input.Length < numberOfUniqueCharactersToLookFor)
            return null;

        var list = Enumerable.Range(0, 1 + input.Length - numberOfUniqueCharactersToLookFor)
            .Select(i => input.Skip(i).Take(numberOfUniqueCharactersToLookFor))
            .Select(arg => arg.AreDistinct())
            .ToList();

        if (!list.Any(unique => unique))
            return null;

        var countOfNonUnique = list
            .TakeWhile(unique => unique == false)
            .Count();

        return countOfNonUnique + numberOfUniqueCharactersToLookFor;
    }
}