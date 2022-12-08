using FluentAssertions;
using Xunit;

namespace day7;

public static class D7P2Tests
{
    [Fact]
    internal static void GetFreeDiskSpaceTest()
    {
        var fileSystemEntries = Input.ExampleInput.ParseThings().CreateDirectoryTree();
        var sizes = fileSystemEntries.GetDirectorySizes();
        var actual = sizes.GetFreeDiskSpace();
        actual.Should().Be(21618835);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 24933642;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 214171;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}