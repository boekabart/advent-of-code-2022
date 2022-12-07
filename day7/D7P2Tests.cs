using day3;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace day7;

public class D7P2Tests
{
    [Fact]
    public void GetFreeDiskSpaceTest()
    {
        var fileSystemEntries = Input.ExampleInput.ParseThings().ExpandPaths().ToList();
        var sizes = fileSystemEntries.GetDirectorySizes().ToArray();
        var actual = sizes.GetFreeDiskSpace();
        actual.Should().Be(21618835);
    }

    [Fact]
    public void AcceptanceTest()
    {
        var expected = 24933642;
        var things = Input.ExampleInput.ParseThings();
        var paths = things.ExpandPaths();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetSizeOfSmallestDirNeededToGetEnoughSpace();
        actual.Should().Be(expected);
    }
}