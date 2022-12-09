using System.Collections.Immutable;
using shared;

namespace day9;

internal record Move(int Count, int DeltaX, int DeltaY);
internal record Distance(int DeltaX, int DeltaY);
internal record MiniMove(int DeltaX, int DeltaY);
internal record Position(int X, int Y);
internal record State(ImmutableList<Position> HeadPositions, ImmutableList<Position> TailPositions);

public static class D9P1
{
    public static int Part1Answer(this string input) =>
        input
            .ParseMoves()
            .Expand()
            .MakeTheMoves()
            .UniqueTailPositions();

    internal static IEnumerable<Move> ParseMoves(this string input) =>
        input
            .NotEmptyTrimmedLines()
            .Select(TryParseAsMove)
            .OfType<Move>();

    internal static Move? TryParseAsMove(this string line)
    {
        var split = line.Split(' ');
        if (split.Length != 2) return null;
        if (!int.TryParse(split[1], out var count))
            return null;
        return split[0] switch
        {
            "D" => new Move(count, 0, -1),
            "U" => new Move(count, 0, 1),
            "L" => new Move(count, -1, 0),
            "R" => new Move(count, 1, 0),
            _ => null
        };
    }

    internal static IEnumerable<MiniMove> Expand(this IEnumerable<Move> moves) => moves.SelectMany(Expand);

    internal static IEnumerable<MiniMove> Expand(this Move move) =>
        Enumerable.Range(0, move.Count).Select(_ => new MiniMove(move.DeltaX, move.DeltaY));

    internal static State MakeTheMoves(this IEnumerable<MiniMove> moves)
    {
        var zeroPos = new Position(0, 0);
        var zeroList = ImmutableList<Position>.Empty.Add(zeroPos);
        var start = new State(zeroList, zeroList);
        return moves.Aggregate(start, (state, move) => state.MakeMove(move));
    }

    internal static State MakeMove(this State state, MiniMove move)
        => state.MoveHead(move).CatchupTail();

    internal static State MoveHead(this State state, MiniMove move)
        => state with {HeadPositions = state.HeadPositions.Move(move)};

    internal static State MoveTail(this State state, MiniMove move)
        => state with {TailPositions = state.TailPositions.Move(move)};

    internal static ImmutableList<Position> Move(this ImmutableList<Position> list, MiniMove move) =>
        list.Add(list.Last().Move(move));

    internal static Position Move(this Position pos, MiniMove move) => new(pos.X + move.DeltaX, pos.Y + move.DeltaY);

    internal static State CatchupTail(this State state)
        => state.MoveTail(state.DetermineCatchupMove());

    internal static MiniMove DetermineCatchupMove(this State state)
        => state.DetermineHeadTailDistance().DetermineCatchupMove();

    internal static MiniMove DetermineCatchupMove(this Distance diff)
        => diff.NotTouching()
            ? new(Math.Sign(diff.DeltaX), Math.Sign(diff.DeltaY))
            : new(Math.Sign(diff.DeltaX - Math.Sign(diff.DeltaX)), Math.Sign(diff.DeltaY - Math.Sign(diff.DeltaY)));

    internal static bool NotTouching(this Distance dist)
        => Math.Abs(dist.DeltaX) > 1 || Math.Abs(dist.DeltaY) > 1;

    internal static Distance DetermineHeadTailDistance(this State state)
        => state.Head().Diff(state.Tail());

    internal static Position Head(this State state)
        => state.HeadPositions.Last();

    internal static Position Tail(this State state)
        => state.TailPositions.Last();

    internal static Distance Diff(this Position head, Position tail) => new(head.X - tail.X, head.Y - tail.Y);

    internal static int UniqueTailPositions(this State state)
        => state.TailPositions.Distinct().Count();
}