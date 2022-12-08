using shared;

namespace day6;

internal static class D3P2
{
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
            .Select(AreUnique)
            .ToList();

        if (!list.Any(unique => unique))
            return null;

        var countOfNonUnique = list
            .TakeWhile(unique => unique == false)
            .Count();

        return countOfNonUnique + numberOfUniqueCharactersToLookFor;
    }

    private static bool AreUnique(IEnumerable<char> arg) => arg.AreDistinct();
}
