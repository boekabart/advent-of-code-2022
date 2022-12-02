using FluentAssertions;
using Xunit;

namespace day1;

public class D1P1Tests
{
    [Fact]
    void ParsingIsOk()
    {
        var input = @"
1000
2000

3000
";
        var actual = D1P1.GetCalorieList(input);
        actual.Should().HaveCount(6);
        actual.Skip(1).First().Should().Be(1000);
        actual.Skip(3).First().Should().BeNull();
    }

    [Fact]
    void CountingIsOk()
    {
        var input = new int?[] {1000, 2000, null, 6000, 1000, null, 4000, 2000, 100};
        var actual = D1P1.GetCaloriesOfHighestElve(input);
        actual.Should().Be(7000);
    }
}