namespace RiftSharp.Routing;

public enum PlatformRoute
{
    Br1,
    Eune1,
    Euw1,
    Jp1,
    Kr,
    La1,
    La2,
    Na1,
    Oc1,
    Ph2,
    Ru,
    Sg2,
    Th2,
    Tr1,
    Tw2,
    Vn2
}

public static class PlatformRouteExtensions
{
    public static string ToHost(this PlatformRoute route)
    {
        return route switch
        {
            PlatformRoute.Br1 => "br1.api.riotgames.com",
            PlatformRoute.Eune1 => "eun1.api.riotgames.com",
            PlatformRoute.Euw1 => "euw1.api.riotgames.com",
            PlatformRoute.Jp1 => "jp1.api.riotgames.com",
            PlatformRoute.Kr => "kr.api.riotgames.com",
            PlatformRoute.La1 => "la1.api.riotgames.com",
            PlatformRoute.La2 => "la2.api.riotgames.com",
            PlatformRoute.Na1 => "na1.api.riotgames.com",
            PlatformRoute.Oc1 => "oc1.api.riotgames.com",
            PlatformRoute.Ph2 => "ph2.api.riotgames.com",
            PlatformRoute.Ru => "ru.api.riotgames.com",
            PlatformRoute.Sg2 => "sg2.api.riotgames.com",
            PlatformRoute.Th2 => "th2.api.riotgames.com",
            PlatformRoute.Tr1 => "tr1.api.riotgames.com",
            PlatformRoute.Tw2 => "tw2.api.riotgames.com",
            PlatformRoute.Vn2 => "vn2.api.riotgames.com",
            _ => throw new ArgumentOutOfRangeException(nameof(route), route, null)
        };
    }
}