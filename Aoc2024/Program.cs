// See https://aka.ms/new-console-template for more information


using Aoc2024;
using Aoc2024._1.A;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(collection =>
    {
        collection.AddTransient<ChallengeFactory>();
        collection.AddTransient<IChallenge, Challenge1A>();
    })
    .ConfigureLogging(builder => builder.AddConsole())
    .Build();

await host.StartAsync();

string? currentChallenge;
if (args.Length < 1)
{
    Console.WriteLine("Select a program:");
    foreach (var service in host.Services.GetServices<IChallenge>())
    {
        Console.WriteLine(service.Name);
    }

    currentChallenge = Console.In.ReadLine();
    if (currentChallenge == null)
    {
        throw new ApplicationException("Please enter a valid program");
    }
} else if (args.Length == 1)
{
    currentChallenge = args[0];
}
else
{
    throw new ArgumentException("Invalid number of arguments");
}

var challengeFactory = host.Services.GetRequiredService<ChallengeFactory>();
var challenge = challengeFactory.GetChallenge(currentChallenge);
await challenge.RunAsync();


await host.StopAsync();