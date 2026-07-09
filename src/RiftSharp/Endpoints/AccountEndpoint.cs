using RiftSharp.Dtos;
using RiftSharp.Http;
using RiftSharp.Routing;

namespace RiftSharp.Endpoints;

public sealed class AccountEndpoint
{
    private readonly RiftHttpClient _http;

    internal AccountEndpoint(RiftHttpClient http)
    {
        _http = http;
    }

    public async Task<RiotAccountDto> GetByRiotIdAsync(
        RegionalRoute route,
        string gameName,
        string tagLine,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(gameName))
        {
            throw new ArgumentException("Game name cannot be empty.", nameof(gameName));
        }

        if (string.IsNullOrWhiteSpace(tagLine))
        {
            throw new ArgumentException("Tag line cannot be empty.", nameof(tagLine));
        }

        var host = route.ToHost();
        var encodedGameName = Uri.EscapeDataString(gameName);
        var encodedTagLine = Uri.EscapeDataString(tagLine);

        var url =
            $"https://{host}/riot/account/v1/accounts/by-riot-id/{encodedGameName}/{encodedTagLine}";

        return await _http.GetAsync<RiotAccountDto>(url, cancellationToken);
    }

    public async Task<RiotAccountDto> GetByPuuidAsync(
        RegionalRoute route,
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
            $"https://{host}/riot/account/v1/accounts/by-puuid/{encodedPuuid}";

        return await _http.GetAsync<RiotAccountDto>(url, cancellationToken);
    }
}