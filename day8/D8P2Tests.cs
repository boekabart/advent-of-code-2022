using FluentAssertions;
using Xunit;

namespace day8;

public static class D8P2Tests
{
    [Fact]
    public static void TestAllInnerTrees()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = exampleInput.AllInnerTrees().ToArray();
        actual.Should().HaveCount(9);
        actual.Distinct().Should().HaveCount(9);
    }

    [Fact]
    public static void TestTree1Score()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = new Tree(2, 1).GetScenicScore(exampleInput);
        actual.Should().Be(4);
    }

    [Fact]
    public static void TestTree2Score()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = new Tree(2, 3).GetScenicScore(exampleInput);
        actual.Should().Be(8);
    }

    [Fact]
    public static void TestTopTreeScore()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = new Tree(2, 0).GetScenicScore(exampleInput);
        actual.Should().Be(0);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var exampleInput = Input.ExampleInput.ParseTreeHeightGrid();
        var actual = exampleInput.GetBestScenicScore();
        actual.Should().Be(8);
    }
}
