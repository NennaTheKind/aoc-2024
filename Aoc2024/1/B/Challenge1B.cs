namespace Aoc2024._1.B;

using Microsoft.Extensions.Logging;

public class Challenge1B : IChallenge
{
    private readonly ILogger<Challenge1B> _logger;

    public Challenge1B(ILogger<Challenge1B> logger)
    {
        _logger = logger;
    }

    public string Name => "1B";

    public async Task RunAsync()
    {
        var (left, right) = await ReadInput();
        long total = 0;
        foreach (var number in left)
        {
            var countInRight = right.Count(r => r == number);
            long numberToAdd = number * countInRight;
            _logger.LogInformation("{Number} occurs {Count} times, adding {NumberToAdd} to the total", number, countInRight, numberToAdd);
            total += numberToAdd;
        }
        _logger.LogInformation("Total = {Total}", total);
    }

    private async Task<(List<int> left, List<int> right)> ReadInput()
    {
        using var fileIn = File.OpenRead("1/B/input.txt");
        using var reader = new StreamReader(fileIn);
        var leftList = new List<int>();
        var rightList = new List<int>();
        while(true)
        {
            var line = await reader.ReadLineAsync();
            if (line is null || line.Length == 0)
            {
                break;
            }
            
            var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var left = int.Parse(parts[0]);
            var right = int.Parse(parts[1]);
            leftList.Add(left);
            rightList.Add(right);
        }
        
        return (leftList, rightList);
    }
}