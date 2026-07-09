using RiftSharp.Endpoints;
using RiftSharp.Http;

namespace RiftSharp;

public sealed class RiftClient
{
    public AccountEndpoint Account { get; }
    public SummonerEndpoint Summoner { get; }

    public RiftClient(string apiKey, HttpClient? httpClient = null)
    {
        var riotHttpClient = new RiftHttpClient(apiKey, httpClient);

        Account = new AccountEndpoint(riotHttpClient);
        Summoner = new SummonerEndpoint(riotHttpClient);
    }
}