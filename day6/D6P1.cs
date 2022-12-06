namespace day6;

internal static class D6P1
{
    public static int? CalculateLengthOfPrefixAndMarker(this string input) =>
        input
            .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)[0]
            .Trim()
            .CalculateLengthOfPrefixAndMarkerOnTrimmedString();

    private static int? CalculateLengthOfPrefixAndMarkerOnTrimmedString(this string input)
    {
        var list = input.Zip(input.Skip(1), input.Skip(2).Zip(input.Skip(3)))
            .Select(AreUnique2)
            .ToList();

        if (!list.Any(unique => unique))
            return null;

        var countOfNonUnique = list
            .TakeWhile(unique => unique == false)
            .Count();

        return countOfNonUnique + 4;
    }

    private static bool AreUnique2((char First, char Second, (char First, char Second) Third) valueTuple)
    {
        if (AreUnique(valueTuple.First, valueTuple.Second, valueTuple.Third)) return true;
        return false;
    }

    private static bool AreUnique(char a, char b, (char c, char d) cd) => a!=b && a!=cd.c && a!=cd.d && b != cd.c && b != cd.d && cd.c != cd.d;
}