/// <summary>
/// Entry point for accessing the Riot Games API.
/// </summary>

namespace RiftSharp.Routing;

public enum RegionalRoute
{
    Americas,
    Asia,
    Europe,
    Sea
}

public static class RegionalRouteExtensions
{
    public static string ToHost(this RegionalRoute route)
    {
        return route switch
        {
            RegionalRoute.Americas => "americas.api.riotgames.com",
            RegionalRoute.Asia => "asia.api.riotgames.com",
            RegionalRoute.Europe => "europe.api.riotgames.com",
            RegionalRoute.Sea => "sea.api.riotgames.com",
            _ => throw new ArgumentOutOfRangeException(nameof(route), route, null)
        };
    }
}