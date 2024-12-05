using Microsoft.Extensions.Logging;

namespace Aoc2024._2.A;

public class Challenge2A : IChallenge
{
    private readonly ILogger<Challenge2A> _logger;

    public Challenge2A(ILogger<Challenge2A> logger)
    {
        _logger = logger;
    }

    public string Name { get; } = "2A";
    public async Task RunAsync()
    {
        var records = await ReadInput();
        var safeRecords = 0;
        foreach (var record in records)
        {
            _logger.LogInformation("Record {@Record}", record);
            safeRecords += record.IsSafe ? 1 : 0;
        }
        
        _logger.LogInformation("{SafeRecords} safe records", safeRecords);
    }
    
    private async Task<List<ReactorRecord>> ReadInput()
    {
        using var fileIn = File.OpenRead("2/A/input.txt");
        using var reader = new StreamReader(fileIn);
        var records = new List<ReactorRecord>();
        while(true)
        {
            var line = await reader.ReadLineAsync();
            if (line is null || line.Length == 0)
            {
                break;
            }
            
            var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var entries = new List<int>();
            foreach (var part in parts)
            {
                var partInt = int.Parse(part);
                entries.Add(partInt);
            }
            
            records.Add(new ReactorRecord
            {
                Entries = entries
            });
        }
        
        return records;
    }

    public class ReactorRecord
    {
        public required IReadOnlyList<int> Entries { get; init; }

        public bool IsSafe
        {
            get
            {
                const int maxChange = 3;
                
                ChangeType changeType = ChangeType.None;
                for (var i = 0; i < Entries.Count - 1; i++)
                {
                    var first = Entries[i];
                    var second = Entries[i + 1];
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

        public enum ChangeType
        {
            None = 0,
            Increasing,
            Decreasing,
        }
    }
}