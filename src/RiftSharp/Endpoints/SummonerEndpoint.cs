using RiftSharp.Dtos;
using RiftSharp.Http;
using RiftSharp.Routing;

namespace RiftSharp.Endpoints;

public sealed class SummonerEndpoint
{
    private readonly RiftHttpClient _http;

    internal SummonerEndpoint(RiftHttpClient http)
    {
        _http = http;
    }

    public async Task<SummonerDto> GetByPuuidAsync(
        PlatformRoute route,
        string puuid,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(puuid))
        {
            throw new ArgumentException("PUUID cannot be empty.", nameof(puuid));
        }

        var host = route.ToHost();
        var encodedPuuid = Uri.EscapeDataString(puuid);

        var url =
            $"https://{host}/lol/summoner/v4/summoners/by-puuid/{encodedPuuid}";

        return await _http.GetAsync<SummonerDto>(url, cancellationToken);
    }
}