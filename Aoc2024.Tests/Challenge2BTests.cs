using Aoc2024._2;
using Aoc2024._2.A;
using Aoc2024._2.B;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Aoc2024.Tests;

public class Challenge2BTests
{
    private readonly Mock<IInputReader<IReadOnlyList<ReactorRecord>>> _inputReader = new();
    private readonly Challenge2B _challenge;

    public Challenge2BTests()
    {
        _challenge = new Challenge2B(NullLogger<Challenge2B>.Instance, _inputReader.Object);
    }

    [Theory]
    [InlineData(7, 6, 4, 2, 1)]
    [InlineData(1, 3, 2, 4, 5)]
    [InlineData(8, 6, 4, 4, 1)]
    [InlineData(1, 3, 6, 7, 9)]
    public void IsRecordSafe_HappyPath(params int[] levels)
    {
        // arrange
        var record = new ReactorRecord
        {
            Entries = levels
        };
        
        // act
        var result = _challenge.IsRecordSafe(record);
        
        // assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1, 2, 7, 8, 9)]
    [InlineData(9, 7, 6, 2, 1)]
    public void IsRecordSafe_UnhappyPath(params int[] levels)
    {
        // arrange
        var record = new ReactorRecord
        {
            Entries = levels
        };
        
        // act
        var result = _challenge.IsRecordSafe(record);
        
        // assert
        result.Should().BeFalse();
    }
}