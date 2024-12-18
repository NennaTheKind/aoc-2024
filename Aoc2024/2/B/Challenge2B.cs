using Aoc2024._2.A;
using Microsoft.Extensions.Logging;

namespace Aoc2024._2.B;

public class Challenge2B : IChallenge
{
    private readonly ILogger<Challenge2B> _logger;
    private readonly IInputReader<IReadOnlyList<ReactorRecord>> _inputReader;

    public Challenge2B(ILogger<Challenge2B> logger, IInputReader<IReadOnlyList<ReactorRecord>> inputReader)
    {
        _logger = logger;
        _inputReader = inputReader;
    }

    public string Name { get; } = "2B";
    public async Task RunAsync()
    {
        var records = await _inputReader.ReadInputAsync();
        var safeRecords = 0;
        foreach (var record in records)
        {
            _logger.LogInformation("Record {@Record}", record);
            safeRecords += IsRecordSafe(record) ? 1 : 0;
        }
        
        _logger.LogInformation("{SafeRecords} safe records", safeRecords);
    }
    
    public bool IsRecordSafe(ReactorRecord record)
    {
        const int maxChange = 3;
                
        ChangeType changeType = ChangeType.None;
        for (var i = 0; i < record.Entries.Count - 1; i++)
        {
            var first = record.Entries[i];
            var second = record.Entries[i + 1];
            var change = second - first;
            var absChange = Math.Abs(change);

            if (change > 0 && absChange <= maxChange)
            {
                if (changeType != ChangeType.None && changeType != ChangeType.Increasing)
                {
                    // decreasing to increasing, unsafe
                    return false;
                }

                changeType = ChangeType.Increasing;
            } else if (change < 0 && absChange <= maxChange)
            {
                if (changeType != ChangeType.None && changeType != ChangeType.Decreasing)
                {
                    // increasing to decreasing, unsafe
                    return false;
                }

                changeType = ChangeType.Decreasing;
            }
            else
            {
                // delta too large or too small, unsafe
                return false;
            }
        }
                
        // passed all checks, safe
        return true;
    }
}