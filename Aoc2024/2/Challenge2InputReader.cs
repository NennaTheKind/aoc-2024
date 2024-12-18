namespace Aoc2024._2;

public class Challenge2InputReader : IInputReader<IReadOnlyList<ReactorRecord>>
{
    public async Task<IReadOnlyList<ReactorRecord>> ReadInputAsync()
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
}