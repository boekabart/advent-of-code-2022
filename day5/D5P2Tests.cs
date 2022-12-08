﻿using FluentAssertions;
using Xunit;

namespace day5;

public class D5P2Tests
{
    [Fact]
    public static void AcceptanceTest()
    {
        var expected = "MCD";
        var stacks = Input.ExampleInput.ParseBoxes().AsStacks();
        var program = Input.ExampleInput.ParseProgram();
        var newStacks = program.Execute9001(stacks);
        var actual = newStacks.TopCrates();
        actual.Should().Be(expected);
    }
}