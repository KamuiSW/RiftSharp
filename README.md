# RiftSharp
An updated C# wrapper of RiotAPI
works with newer Riot ID and PUUID

**note that this project is mostly me learning, so part of the code is either written by AI or was assisted by AI. Cheers <3*

**current features**
 - PUUID player searching
 - Account v1 sup
 - Summoner v4 sup
 - Match v5 sup
 - Platform and regional routing
 - Basic error messages

**planned features**
 - League v4 sup
 - Champ mastery v4 sup
 - Better rate limit handler
 - NuGet package release
 - More examples and documentation

Installation
RiftSharp is not published to NuGet yet :c
For now, clone the repository and reference the project directly:
```
git clone https://github.com/KamuiSW/riftsharp.git
```
Then add the library project reference to your own application:
```
dotnet add reference path/to/RiftSharp/src/RiftSharp/RiftSharp.csproj
```

## How to use

To use the Riot Games API, you need an API key from the Riot Developer Portal.

Example using an environment variable:
```
using RiftSharp;
using RiftSharp.Routing;

var apiKey = Environment.GetEnvironmentVariable("RIOT_API_KEY");

if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.WriteLine("Missing Riot API key.");
    return;
}

var client = new RiftClient(apiKey);
```

## Account API

To get a Riot account by Riot ID:

```
using RiftSharp;
using RiftSharp.Routing;

var client = new RiftClient("YOUR_API_KEY");

try
{
    var account = await client.Account.GetByRiotIdAsync(
        RegionalRoute.Europe,
        "SomePlayer",
        "EUW"
    );

    Console.WriteLine(account.GameName);
    Console.WriteLine(account.TagLine);
    Console.WriteLine(account.Puuid);
}
catch (RiftApiException ex)
{
    Console.WriteLine($"RiftSharp error: {(int)ex.StatusCode} {ex.StatusCode}");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.ResponseBody);
}
```

To get a Riot account by PUUID:

```
try
{
    var account = await client.Account.GetByPuuidAsync(
        RegionalRoute.Europe,
        "PLAYER_PUUID"
    );

    Console.WriteLine(account.GameName);
    Console.WriteLine(account.TagLine);
}
catch (RiftApiException ex)
{
    Console.WriteLine($"RiftSharp error: {(int)ex.StatusCode} {ex.StatusCode}");
    Console.WriteLine(ex.Message);
}
```

## Summoner API

To get League of Legends summoner information from a PUUID:

```
try
{
    var summoner = await client.Summoner.GetByPuuidAsync(
        PlatformRoute.Euw1,
        "PLAYER_PUUID"
    );

    Console.WriteLine(summoner.Id);
    Console.WriteLine(summoner.SummonerLevel);
    Console.WriteLine(summoner.ProfileIconId);
}
catch (RiftApiException ex)
{
    Console.WriteLine($"RiftSharp error: {(int)ex.StatusCode} {ex.StatusCode}");
    Console.WriteLine(ex.Message);
}
```
```
var account = await client.Account.GetByRiotIdAsync(
    RegionalRoute.Europe,
    "SomePlayer",
    "EUW"
);

var summoner = await client.Summoner.GetByPuuidAsync(
    PlatformRoute.Euw1,
    account.Puuid
);

Console.WriteLine($"Summoner level: {summoner.SummonerLevel}");
```

## Match API

To get recent match ID's for a player:

```
try
{
    var matchIds = await client.Match.GetMatchIdsByPuuidAsync(
        RegionalRoute.Europe,
        "PLAYER_PUUID",
        start: 0,
        count: 10
    );

    foreach (var matchId in matchIds)
    {
        Console.WriteLine(matchId);
    }
}
catch (RiftApiException ex)
{
    Console.WriteLine($"RiftSharp error: {(int)ex.StatusCode} {ex.StatusCode}");
    Console.WriteLine(ex.Message);
}
```

To get details for a specific match:

```
try
{
    var match = await client.Match.GetMatchAsync(
        RegionalRoute.Europe,
        "EUW1_1234567890"
    );

    Console.WriteLine(match.Metadata.MatchId);
    Console.WriteLine(match.Info.GameMode);
    Console.WriteLine(match.Info.GameDuration);
}
catch (RiftApiException ex)
{
    Console.WriteLine($"RiftSharp error: {(int)ex.StatusCode} {ex.StatusCode}");
    Console.WriteLine(ex.Message);
}
```

## Routing

RiftSharp separates Riot API routing into two route types:

**Regional Routes**
Used by APIs such as Account V1 and Match V5

```
RegionalRoute.Americas
RegionalRoute.Asia
RegionalRoute.Europe
RegionalRoute.Sea
```

**Platform Routes**
Used by platform specific League of Legends APIs such as Summoner V4

```
PlatformRoute.Euw1
PlatformRoute.Na1
PlatformRoute.Kr
PlatformRoute.Jp1
PlatformRoute.Eune1
PlatformRoute.Br1
PlatformRoute.Tr1
```

Example:

```
var account = await client.Account.GetByRiotIdAsync(
    RegionalRoute.Europe,
    "SomePlayer",
    "EUW"
);

var summoner = await client.Summoner.GetByPuuidAsync(
    PlatformRoute.Euw1,
    account.Puuid
);
```

## Error Handling

RiftSharp throws `RiftApiException` when the Riot API returns an error response.

```
try
{
    var account = await client.Account.GetByRiotIdAsync(
        RegionalRoute.Europe,
        "SomePlayer",
        "EUW"
    );
}
catch (RiftApiException ex)
{
    Console.WriteLine(ex.StatusCode);
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.ResponseBody);
}
```

Possible errors:

-   `401 Unauthorized`  
    The API key is missing or invalid
-   `403 Forbidden`  
    The API key may be expired or invalid
-   `404 Not Found`  
    The player or match could not be found
-   `429 Too Many Requests`  
    The Riot API rate limit has been exceeded

## Contributing
Contributions are welcome <3

## Disclaimer
RiftSharp is not endorsed by Riot Games and does not reflect the views or opinions of Riot Games or anyone officially involved in producing or managing Riot Games properties.

Riot Games, League of Legends, and all associated properties are trademarks or registered trademarks of Riot Games, Inc.