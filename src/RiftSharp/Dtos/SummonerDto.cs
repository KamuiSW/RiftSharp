using System.Text.Json.Serialization;

namespace RiftSharp.Dtos;

public sealed record SummonerDto(

    [property: JsonPropertyName("id")]
    string Id,

    [property: JsonPropertyName("accountId")]
    string AccountId,

    [property: JsonPropertyName("puuid")]
    string Puuid,

    [property: JsonPropertyName("profileIconId")]
    int ProfileIconId,

    [property: JsonPropertyName("revisionDate")]
    long RevisionDate,

    [property: JsonPropertyName("summonerLevel")]
    long SummonerLevel
    
);