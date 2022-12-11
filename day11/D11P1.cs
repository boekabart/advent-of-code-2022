using System.Collections.Immutable;
using shared;

namespace day11;

internal record Monkey(int Index, ImmutableQueue<long> Items, Func<long,long> Operation, Func<long,bool> Test, int TrueMonkey, int FalseMonkey, long Modulo);

internal record Game(ImmutableArray<Monkey> Monkeys, ImmutableArray<int> InspectionCounts, long WorryDivisor, long Modulo);

public static class D11P1
{
    public static long Part1Answer(this string input) =>
        input
            .ParseMonkeys()
            .ToList()
            .CreateGame()
            .PlayRounds(20)
            .GetLevelOfMonkeyBusiness();

    internal static IEnumerable<Monkey> ParseMonkeys(this string input) =>
        input
            .TrimmedLines()
            .Scan(default(Monkey), ParseInputLine)
            .OfType<Monkey>();

    private static (Monkey? RetVal, Monkey? WipMonkey) ParseInputLine(Monkey? wipMonkey, string line, int _)
    {
        if (string.IsNullOrEmpty(line))
            return (wipMonkey, null);
        var split = line.Split(' ');
        if (split[0].Equals("Monkey")) return StartNewMonkey(wipMonkey, split[1]);
        if (wipMonkey is null) return (null, wipMonkey);
        return (split[0], split[1]) switch
        {
            ("Starting", _) => AddStartingItems(wipMonkey, split[2..]),
            ("Operation:", _) => AddOperation(wipMonkey, split[1..]),
            ("Test:", _) => AddTest(wipMonkey, split[1..]),
            ("If", "true:") => AddTrueMonkey(wipMonkey, split[1..]),
            ("If", "false:") => AddFalseMonkey(wipMonkey, split[1..]),
            _ => throw new InvalidOperationException(split[0])
        };
    }

    private static (Monkey? RetVal, Monkey WipMonkey) AddStartingItems(Monkey wipMonkey, string[] rest) =>
        (null, wipMonkey with
        {
            Items = rest
                .Select(withComma => withComma.Replace(",", ""))
                .Select(long.Parse)
                .Aggregate(ImmutableQueue<long>.Empty, (queue, item) => queue.Enqueue(item))
        });

    private static (Monkey? RetVal, Monkey WipMonkey) AddOperation(Monkey wipMonkey, string[] rest) =>
        (rest[3], rest[4]) switch
        {
            ("*", "old") => (null, wipMonkey with {Operation = old => old * old}),
            ("+", "old") => (null, wipMonkey with {Operation = old => old + old}),
            ("*", _) => (null, wipMonkey with {Operation = old => old * long.Parse(rest[4])}),
            ("+", _) => (null, wipMonkey with {Operation = old => old + long.Parse(rest[4])}),
            _ => throw new InvalidOperationException()
        };

    private static (Monkey? RetVal, Monkey WipMonkey) AddTest(Monkey wipMonkey, string[] rest)
        => (null, wipMonkey with
        {
            Test = worry => worry % long.Parse(rest[2]) == 0,
            Modulo = long.Parse(rest[2])
        });

    private static (Monkey? RetVal, Monkey WipMonkey) AddTrueMonkey(Monkey wipMonkey, string[] rest)
        => (null, wipMonkey with {TrueMonkey = int.Parse(rest[4])});

    private static (Monkey? RetVal, Monkey WipMonkey) AddFalseMonkey(Monkey wipMonkey, string[] rest)
        => (null, wipMonkey with {FalseMonkey = int.Parse(rest[4])});

    private static (Monkey? RetVal, Monkey WipMonkey) StartNewMonkey(Monkey? wipMonkey, string numberColon)
    {
        var monkeyNo = int.Parse(numberColon[..^1]);
        return (
            wipMonkey,
            new Monkey(monkeyNo, ImmutableQueue<long>.Empty, _ => 0, _ => false, -1, -1, 0)
        );
    }

    internal static Game CreateGame(this ICollection<Monkey> monkeys, int worryDivisor = 3) =>
        new(monkeys.ToImmutableArray(), monkeys.Select(_ => 0).ToImmutableArray(), worryDivisor, monkeys.Select(m => m.Modulo).Multiplied());

    internal static Game PlayRounds(this Game game, int count)
        => Enumerable.Range(0, count).Aggregate(game, PlayRound);

    internal static Game PlayRound(this Game game, int roundNumber)
        => game.Monkeys.Select(m => m.Index).Aggregate(game, PlayTurn);

    private static Game PlayTurn(Game game, int monkeyIndex) =>
        game.Monkeys[monkeyIndex].Items
            .Select(item => (Item: item, MonkeyIndex: monkeyIndex))
            .Aggregate(game.UpdateMonkey(game.Monkeys[monkeyIndex] with {Items = ImmutableQueue<long>.Empty}),
                (gm, itemPair) => HandleItem(itemPair.Item, gm.Monkeys[itemPair.MonkeyIndex], gm));

    private static Game HandleItem(long item, Monkey monkey, Game game)
    {
        var newItem = (monkey.Operation(item) / game.WorryDivisor) % game.Modulo;
        var targetMonkey = game.Monkeys[monkey.Test(newItem) ? monkey.TrueMonkey : monkey.FalseMonkey];
        return game
            .IncreaseInspectionCount(monkey)
            .UpdateMonkey(targetMonkey with {Items = targetMonkey.Items.Enqueue(newItem)});
    }

    private static Game UpdateMonkey(this Game game, Monkey monkey)
        => game with
        {
            Monkeys = game.Monkeys.SetItem(monkey.Index, monkey)
        };

    private static Game IncreaseInspectionCount(this Game game, Monkey monkey)
        => game with
        {
            InspectionCounts = game.InspectionCounts.SetItem(monkey.Index, game.InspectionCounts[monkey.Index] + 1)
        };

    internal static long GetLevelOfMonkeyBusiness(this Game game)
        => game
            .InspectionCounts
            .OrderByDescending(_ => _)
            .Take(2)
            .Multiplied();

    internal static long Multiplied(this IEnumerable<int> numbers)
        => numbers.Aggregate((long)1, (prev, num) => prev * num);

    internal static long Multiplied(this IEnumerable<long> numbers)
        => numbers.Aggregate((long)1, (prev, num) => prev * num);
}