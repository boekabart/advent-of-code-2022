using FluentAssertions;
using Xunit;

namespace day8;

public static class D8P2Tests
{
    [Fact]
    internal static void TestAllInnerTrees()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = exampleInput.AllInnerTrees().ToArray();
        actual.Should().HaveCount(9);
        actual.Distinct().Should().HaveCount(9);
    }

    [Fact]
    internal static void TestTree1Score()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = new Tree(2, 1).GetScenicScore(exampleInput);
        actual.Should().Be(4);
    }

    [Fact]
    internal static void TestTree2Score()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = new Tree(2, 3).GetScenicScore(exampleInput);
        actual.Should().Be(8);
    }

    [Fact]
    internal static void TestTopTreeScore()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = new Tree(2, 0).GetScenicScore(exampleInput);
        actual.Should().Be(0);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 8;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 480000;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}
