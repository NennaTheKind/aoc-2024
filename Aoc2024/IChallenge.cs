namespace Aoc2024;

public interface IChallenge
{
    public string Name { get; }
    Task RunAsync();
}