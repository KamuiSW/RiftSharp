using RiftSharp;
using RiftSharp.Routing;

var apiKey = Environment.GetEnvironmentVariable("RIOT_API_KEY");

if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.WriteLine("Missing riot api key environment variable");
    Console.WriteLine("Set it before running the example");
    return;
}

var client = new RiftClient(apiKey);

try
{
    var account = await client.Account.GetByRiotIdAsync(
        RegionalRoute.Europe,
        "看雨民",
        "Kamui"
    );

    Console.WriteLine($"Game Name: {account.GameName}");
    Console.WriteLine($"Tag Line: {account.TagLine}");
    Console.WriteLine($"PUUID: {account.Puuid}");

    var summoner = await client.Summoner.GetByPuuidAsync(
        PlatformRoute.Euw1,
        account.Puuid
    );

    Console.WriteLine($"Summoner Level: {summoner.SummonerLevel}");
    Console.WriteLine($"Profile Icon ID: {summoner.ProfileIconId}");
}
catch (RiftApiException ex)
{
    Console.WriteLine($"RiftSharp error: {(int)ex.StatusCode} {ex.StatusCode}");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.ResponseBody);
}