using FluentAssertions;
using Xunit;

namespace day7;

public class D7P1Tests
{
    [InlineData("$ cd /", typeof(CdThing), "/")]
    [InlineData("$ cd ..", typeof(CdThing), "..")]
    [InlineData("", null)]
    [InlineData("$ ls", null)]
    [InlineData("dir a", typeof(DirThing), "a")]
    [InlineData("14848514 b.txt", typeof(FileThing), "b.txt", (long)14848514)]
    [Theory]
    public void ParseInputLineTest(string line, Type? expectedType, string? expectedPath = null, long? expectedSize = null)
    {
        var actualThing = line.TryParseAsThing();
        if (expectedType is null)
        {
            actualThing.Should().BeNull();
            return;
        }
        actualThing.Should().BeOfType(expectedType);
        actualThing!.Path.Should().Be(expectedPath);
        actualThing.Size.Should().Be(expectedSize);
    }

    [InlineData("/", "/", "/")]
    [InlineData("/", "a", "/a/")]
    [InlineData("/b/", "a", "/b/a/")]
    [InlineData("/b/", "..", "/")]
    [InlineData("/b/c/", "..", "/b/")]
    [Theory]
    public void PathCombineTests(string root, string relative, string expected)
    {
        var actual = root.PathCombine(relative);
        actual.Should().Be(expected);
    }

    [Fact]
    public void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(19);
    }
    [Fact]
    public void ParseInputAndExpandTest()
    {
        var things = Input.ExampleInput.ParseThings().ExpandPaths().ToArray();
        things.Should().HaveCount(14);
        things.OfType<DirThing>().Should().AllSatisfy(dir => dir.Path.Should().EndWith("/"));
        things.OfType<FileThing>().Should().AllSatisfy(file => file.Path.Should().NotEndWith("/"));
    }

    [Fact]
    public void ParseInputAndExpandAndGetSizesTest()
    {
        var fileSystemEntries = Input.ExampleInput.ParseThings().ExpandPaths().ToList();
        var things = fileSystemEntries.GetDirectorySizes().ToArray();
        things.Should().HaveCount(4);
        things[0].Dir.Path.Should().Be("/");
        things[0].TotalSize.Should().Be(48381165);
        things[1].Dir.Path.Should().Be("/a/");
        things[1].TotalSize.Should().Be(94853);
    }

    [Fact]
    public void AcceptanceTest()
    {
        var expected = 95437;
        var things = Input.ExampleInput.ParseThings();
        var paths = things.ExpandPaths();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetResult();
        actual.Should().Be(expected);
    }

    [Fact]
    public void NotAcceptanceTest()
    {
        var notExpected = 1486590; //Wrong answer
        var things = Input.RawInput.ParseThings();
        var paths = things.ExpandPaths();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetResult();
        actual.Should().NotBe(notExpected);
    }


    [Fact]
    public void MyAcceptanceTest()
    {
        var expected = 1583951;
        var things = Input.RawInput.ParseThings();
        var paths = things.ExpandPaths();
        var sizes = paths.GetDirectorySizes();
        var actual = sizes.GetResult();
        actual.Should().Be(expected);
    }
}
