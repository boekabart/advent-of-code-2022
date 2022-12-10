using FluentAssertions;
using shared;
using Xunit;

namespace day10;

public class D10P2Tests
{
    [Fact]
    internal static void RenderPixelTest()
    {
        var line = new[] { 1, 1, 16, 16, 5, 5, 11, 11, 8, 8, 13, 13, 12, 12, 4, 4, 17, 17, 21, 21, 21 };
        var l = line.Select(a => new CpuRegisters(a)).ToArray();

        new CpuRegisters(13).RenderPixel(11).Should().Be('·');
        new CpuRegisters(13).RenderPixel(12).Should().Be('█');
        new CpuRegisters(13).RenderPixel(13).Should().Be('█');
        new CpuRegisters(13).RenderPixel(14).Should().Be('█');
        new CpuRegisters(13).RenderPixel(15).Should().Be('·');
    }

    [Fact]
    internal static void RenderLineTest()
    {
        var line = new[] {1, 1, 16, 16, 5, 5, 11, 11, 8, 8, 13, 13, 12, 12, 4, 4, 17, 17, 21, 21, 21}
            .Select(a => new CpuRegisters(a)).ToArray();

        var actual = line.RenderLine();
        actual.Should().Be("██··██··██··██··██··█");
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expectedText = @"
██··██··██··██··██··██··██··██··██··██··
███···███···███···███···███···███···███·
████····████····████····████····████····
█████·····█████·····█████·····█████·····
██████······██████······██████······████
███████·······███████·······███████·····
";
        var expected = expectedText.NotEmptyTrimmedLines();
        Input.ExampleInput
            .Part2Answer()
            .Should().BeEquivalentTo(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expectedText = @"
████·███····██·███··███··█··█··██··█··█·
█····█··█····█·█··█·█··█·█·█··█··█·█··█·
███··█··█····█·███··█··█·██···█··█·████·
█····███·····█·█··█·███··█·█··████·█··█·
█····█····█··█·█··█·█·█··█·█··█··█·█··█·
████·█·····██··███··█··█·█··█·█··█·█··█·
";
        var expected = expectedText.NotEmptyTrimmedLines();
        Input.RawInput
            .Part2Answer()
            .Should().BeEquivalentTo(expected);
    }
}