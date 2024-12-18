namespace Aoc2024;

public interface IInputReader<T>
{
    public Task<T> ReadInputAsync();
}