using Microsoft.Extensions.Logging;

namespace Aoc2024._1.A;

public class Challenge1A : IChallenge
{
    private readonly ILogger<Challenge1A> _logger;

    public Challenge1A(ILogger<Challenge1A> logger)
    {
        _logger = logger;
    }

    public string Name => "1A";

    public async Task RunAsync()
    {
        var (left, right) = await ReadInput();
        left.Sort();
        right.Sort();
        long total = 0;
        foreach (var pair in left.Zip(right))
        {
            var distance = Math.Abs(pair.First - pair.Second);
            _logger.LogInformation("{Left} - {Right} = {Distance}", pair.First, pair.Second, distance);
            total += distance;
        }
        _logger.LogInformation("Total = {Total}", total);
    }

    private async Task<(List<int> left, List<int> right)> ReadInput()
    {
        using var fileIn = File.OpenRead("1/A/input.txt");
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