using FluentAssertions;
using Xunit;

namespace day1;

public class D1P2Tests
{
    [Fact]
    void CountingIsOkFor1()
    {
        var input = new int?[] { 1000, 2000, null, 6000, 1000, null, 4000, 2000, 100 };
        var actual = D1P2.GetCaloriesOfHighestElves(input,1);
        actual.Should().Be(7000);
    }

    [Fact]
    void CountingIsOkFor3()
    {
        var input = new int?[] { 1000, 2000, null, 6000, 1000, null, 4000, 2000, 100 };
        var actual = D1P2.GetCaloriesOfHighestElves(input, 3);
        actual.Should().Be(16100);
    }
}