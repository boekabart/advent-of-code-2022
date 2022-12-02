﻿using System.Diagnostics;

namespace day17;

public record Thing(bool Data);

internal static class D17P1
{
    public static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .Split(new[] {'\r'})
            .Select(TryParseAsThing)
            .OfType<Thing>();

    public static Thing? TryParseAsThing(this string line)
    {
        return null;
    }

    public static int GetResult(this IEnumerable<Thing> things) => things.Select(AsResult).Sum();
    public static int AsResult(this Thing thing) => 0;
}