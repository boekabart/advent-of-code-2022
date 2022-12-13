using FluentAssertions;
using shared;
using Xunit;

namespace day13;

public static class D13P1Tests
{
    [Fact]
    internal static void ParseInputLineTest()
    {
        "[]".TryParseAsThing().Things.Should().BeEquivalentTo(Array.Empty<Thing>());
        "[9]".TryParseAsThing().Things.Should().BeEquivalentTo(new Thing[] {new(9)});
        "[7,7,7]".TryParseAsThing().Things.Should().BeEquivalentTo(new Thing[] {new(7), new(7) , new(7) });
        "9".TryParseAsThing().Should().BeEquivalentTo(new Thing(9));
        "0".TryParseAsThing().Should().BeEquivalentTo(new Thing(0));
        "[[1],4]".TryParseAsThing().Render().Should().Be("[[1],4]");
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(16);
    }

    [Fact]
    internal static void RoundtripTest()
    {
        foreach (var line in Input.ExampleInput
                     .NotEmptyTrimmedLines())
            line.TryParseAsThing().Render().Should().Be(line);
        foreach (var line in Input.RawInput
                     .NotEmptyTrimmedLines()) 
            line.TryParseAsThing().Render().Should().Be(line);
    }

    [Fact]
    internal static void CompareTest()
    {
        "9".TryParseAsThing().CompareTo("9".TryParseAsThing()).Should().Be(0);
        "[9]".TryParseAsThing().CompareTo("[9]".TryParseAsThing()).Should().Be(0);
        "[1,1,3,1,1]".TryParseAsThing().CompareTo("[1,1,5,1,1]".TryParseAsThing()).Should().Be(-1);
        "[[1],[2,3,4]]".TryParseAsThing().CompareTo("[[1],4]".TryParseAsThing()).Should().Be(-1);
        "[9]".TryParseAsThing().CompareTo("[[8,7,6]]".TryParseAsThing()).Should().Be(1);
        "[[4,4],4,4]".TryParseAsThing().CompareTo("[[4,4],4,4,4]".TryParseAsThing()).Should().Be(-1);
        "[7,7,7,7]".TryParseAsThing().CompareTo("[7,7,7]".TryParseAsThing()).Should().Be(1);
        "[]".TryParseAsThing().CompareTo("[3]".TryParseAsThing()).Should().Be(-1);
        "[[[]]]".TryParseAsThing().CompareTo("[[]]".TryParseAsThing()).Should().Be(1);
        "[1,[2,[3,[4,[5,6,7]]]],8,9]".TryParseAsThing().CompareTo("[1,[2,[3,[4,[5,6,0]]]],8,9]".TryParseAsThing()).Should().Be(1);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 13;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 6369;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
