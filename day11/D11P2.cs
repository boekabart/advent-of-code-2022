namespace day11;

public static class D11P2
{
    public static long Part2Answer(this string input) =>
        input
            .ParseMonkeys()
            .ToList()
            .CreateGame(1)
            .PlayRounds(10000)
            .GetLevelOfMonkeyBusiness();
}
