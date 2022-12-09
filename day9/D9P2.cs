using System.Collections.Immutable;
using shared;

namespace day9;
internal record State2(ImmutableList<ImmutableList<Position>> Positions);

public static class D9P2
{
    public static object Part2Answer(this string input) =>
        input
            .ParseMoves()
            .Expand()
            .MakeTheMoves(10)
            .UniqueTailPositions();
    internal static State2 MakeTheMoves(this IEnumerable<MiniMove> moves, int len)
    {
        var zeroPos = new Position(0, 0);
        var zeroList = ImmutableList<Position>.Empty.Add(zeroPos);
        var listOfLists = ImmutableList<ImmutableList<Position>>.Empty;
        for( int q = 0; q < len; q++ )
            listOfLists = listOfLists.Add(zeroList);
        var start = new State2(listOfLists);
        return moves.Aggregate(start, (state, move) => state.MakeMove(move));
    }

    internal static State2 MakeMove(this State2 state, MiniMove move)
        => state.MoveHead(move).CatchupTail();

    internal static State2 MoveHead(this State2 state, MiniMove move) =>
        new(Positions: state.Positions.SetItem(0, state.Positions[0].Move(move)));

    internal static State2 CatchupTail(this State2 state)
    {
        var iter = state;
        foreach (var (item, index) in state.Positions.WithIndex().Skip(1))
        {
            var prevPos = iter.Positions[index - 1].Last();
            var myPos = item.Last();
            var move = prevPos.Diff(myPos).DetermineCatchupMove();
            iter = new(iter.Positions.SetItem(index, item.Add(myPos.Move(move))));
        }

        return iter;
    }

    internal static int UniqueTailPositions(this State2 state)
        => state.TailPositions().Distinct().Count();

    internal static IReadOnlyCollection<Position> TailPositions(this State2 state)
        => state.Positions.Last();
}
