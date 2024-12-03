namespace Aoc2024;

public class ChallengeFactory
{
    private readonly Dictionary<string, IChallenge> _challenges;

    public ChallengeFactory(IEnumerable<IChallenge> challenges)
    {
        _challenges = challenges.ToDictionary(x => x.Name);
    }

    public IChallenge GetChallenge(string name)
    {
        if (_challenges.TryGetValue(name, out var challenge))
        {
            return challenge;
        }
        
        throw new KeyNotFoundException();
    }
}