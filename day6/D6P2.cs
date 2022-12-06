namespace day6;

internal static class D3P2
{
    public static int? CalculateLengthOfPrefixAndMarker14(this string input, int numberOfUniqueCharactersToLookFor) =>
        input
            .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)[0]
            .Trim()
            .CalculateLengthOfPrefixAndMarkerOnTrimmedString(numberOfUniqueCharactersToLookFor);

    private static int? CalculateLengthOfPrefixAndMarkerOnTrimmedString(this string input, int numberOfUniqueCharactersToLookFor)
    {
        if (input.Length < numberOfUniqueCharactersToLookFor)
            return null;

        var list = Enumerable.Range(0, input.Length - numberOfUniqueCharactersToLookFor)
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

    private static bool AreUnique(IEnumerable<char> arg)
    {
        return arg.Distinct().Count() == arg.Count();
    }
}
