using FluentAssertions;
using Xunit;

namespace day7;

public static class D7P1Tests
{
    [InlineData("$ cd /", typeof(CdRoot))]
    [InlineData("$ cd ..", typeof(CdUp))]
    [InlineData("$ cd a", typeof(CdThing), "a")]
    [InlineData("", null)]
    [InlineData("$ ls", null)]
    [InlineData("dir a", typeof(DirThing), "a")]
    [InlineData("14848514 b.txt", typeof(FileThing), "b.txt", (long)14848514)]
    [Theory]
    public static void ParseInputLineTest(string line, Type? expectedType, string? expectedPath = null, long? expectedSize = null)
    {
        var actualThing = line.TryParseAsThing();
        if (expectedType is null)
        {
            actualThing.Should().BeNull();
            return;
        }
        actualThing.Should().BeOfType(expectedType);
        actualThing.Name().Should().Be(expectedPath);
        actualThing.Size().Should().Be(expectedSize);
    }

    private static long? Size(this IThing? thing) => thing switch
    {
        FileThing ft => ft.Size,
        _ => null
    };

    private static string? Name(this IThing? thing) => thing switch
    {
        FileThing ft => ft.Name,
        CdThing cd => cd.SubDir,
        DirThing dur => dur.Name,
        _ => null
    };
    
    [Fact]
    public static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(19);
    }
    [Fact]
    public static void ParseInputAndCreateDirectoryTreeTest()
    {
        var tree = Input.ExampleInput.ParseThings().CreateDirectoryTree();
        tree.Name.Should().BeEmpty();
        tree.SubDirectories.Should().HaveCount(2);
        tree.Files.Should().HaveCount(2);
        tree.SubDirectories[0].Name.Should().Be("a");
        tree.SubDirectories[0].SubDirectories[0].Name.Should().Be("e");
    }

    [Fact]
    public static void ParseInputAndExpandAndGetSizesTest()
    {
        var fileSystemEntries = Input.ExampleInput.ParseThings().CreateDirectoryTree();
        var things = fileSystemEntries.GetDirectorySizes().ToArray();
        things.Should().HaveCount(4);
        things[0].Dir.Name.Should().Be("");
        things[1].Dir.Name.Should().Be("a");
        things[2].Dir.Name.Should().Be("e");
        things[3].Dir.Name.Should().Be("d");
        things[0].TotalSize.Should().Be(48381165);
        things[1].TotalSize.Should().Be(94853);
        things[2].TotalSize.Should().Be(584);
        things[3].TotalSize.Should().Be(24933642);
    }

    [Fact]
    public static void AcceptanceTest()
    {
        var expected = 95437;
        var things = Input.ExampleInput.ParseThings();
        var paths = things.CreateDirectoryTree();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetResult();
        actual.Should().Be(expected);
    }

    [Fact]
    public static void DontRepeatYourMistakeTest()
    {
        var notExpected = 1486590; //Wrong answer
        var things = Input.RawInput.ParseThings();
        var paths = things.CreateDirectoryTree();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetResult();
        actual.Should().NotBe(notExpected);
    }

    [Fact]
    public static void RegressionTest()
    {
        var expected = 1583951;
        var things = Input.RawInput.ParseThings();
        var paths = things.CreateDirectoryTree();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetResult();
        actual.Should().Be(expected);
    }
}
