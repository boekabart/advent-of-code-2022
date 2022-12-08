using FluentAssertions;
using Xunit;

namespace day7;

public static class D7P2Tests
{
    [Fact]
    public static void GetFreeDiskSpaceTest()
    {
        var fileSystemEntries = Input.ExampleInput.ParseThings().CreateDirectoryTree();
        var sizes = fileSystemEntries.GetDirectorySizes().ToArray();
        var actual = sizes.GetFreeDiskSpace();
        actual.Should().Be(21618835);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 24933642;
        var things = Input.ExampleInput.ParseThings();
        var paths = things.CreateDirectoryTree();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetSizeOfSmallestDirNeededToGetEnoughSpace();
        actual.Should().Be(expected);
    }
}