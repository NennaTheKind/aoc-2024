using Aoc2024._2;
using Aoc2024._2.A;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Aoc2024.Tests;

public class Challenge2ATests
{
    private readonly Mock<IInputReader<IReadOnlyList<ReactorRecord>>> _inputReader = new();
    private readonly Challenge2A _challenge2A;

    public Challenge2ATests()
    {
        _challenge2A = new Challenge2A(NullLogger<Challenge2A>.Instance, _inputReader.Object);
    }

    [Theory]
    [InlineData(7, 6, 4, 2, 1)]
    [InlineData(1, 3, 6, 7, 9)]
    public void IsRecordSafe_HappyPath(params int[] levels)
    {
        // arrange
        var record = new ReactorRecord
        {
            Entries = levels
        };
        
        // act
        var result = _challenge2A.IsRecordSafe(record);
        
        // assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1, 2, 7, 8, 9)]
    [InlineData(9, 7, 6, 2, 1)]
    [InlineData(1, 3, 2, 4, 5)]
    [InlineData(8, 6, 4, 4, 1)]
    public void IsRecordSafe_UnhappyPath(params int[] levels)
    {
        // arrange
        var record = new ReactorRecord
        {
            Entries = levels
        };
        
        // act
        var result = _challenge2A.IsRecordSafe(record);
        
        // assert
        result.Should().BeFalse();
    }
}