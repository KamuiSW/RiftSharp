using System.Text.Json.Serialization;

namespace RiftSharp.Dtos;

public sealed record RiotAccountDto(

    [property: JsonPropertyName("puuid")]
    string Puuid,

    [property: JsonPropertyName("gameName")]
    string GameName,

    [property: JsonPropertyName("tagLine")]
    string TagLine
    
);