﻿using FluentAssertions;
using Xunit;

namespace day4;

public static class D4P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 4;
        var things = Input.ExampleInput.ParsePairAssignments();
        var expandedThings = things.Select(r => r.Expand());

        var actual = expandedThings.GetNumberOfOverlappingPairs();
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 806;
        var things = Input.RawInput.ParsePairAssignments();
        var expandedThings = things.Select(r => r.Expand());

        var actual = expandedThings.GetNumberOfOverlappingPairs();
        actual.Should().Be(expected);
    }
}