using System.Net;
using System.Text.Json;

namespace RiftSharp.Http;

internal sealed class RiftHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public RiftHttpClient(string apiKey, HttpClient? httpClient = null)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("API key cannot be empty.", nameof(apiKey));
        }

        _httpClient = httpClient ?? new HttpClient();

        if (!_httpClient.DefaultRequestHeaders.Contains("X-Riot-Token"))
        {
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
        }

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<T> GetAsync<T>(
        string url,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw CreateException(response, body);
        }

        var result = JsonSerializer.Deserialize<T>(body, _jsonOptions);

        if (result is null)
        {
            throw new RiftApiException(
                HttpStatusCode.NoContent,
                "Riot API returned empty or invalid JSON.",
                body
            );
        }

        return result;
    }

    private static RiftApiException CreateException(
        HttpResponseMessage response,
        string? body
    )
    {
        var statusCode = response.StatusCode;

        var message = statusCode switch
        {
            HttpStatusCode.Forbidden =>
                "Riot API returned 403 Forbidden. Check that your API key is valid, not expired, and allowed to access this endpoint.",

            HttpStatusCode.NotFound =>
                "Riot API returned 404 Not Found. The requested resource does not exist, or the route/player ID is wrong.",

            HttpStatusCode.TooManyRequests =>
                BuildRateLimitMessage(response),

            HttpStatusCode.Unauthorized =>
                "Riot API returned 401 Unauthorized. The API key is missing or invalid.",

            _ =>
                $"Riot API request failed with {(int)statusCode} {response.ReasonPhrase}."
        };

        return new RiftApiException(statusCode, message, body);
    }

    private static string BuildRateLimitMessage(HttpResponseMessage response)
    {
        if (response.Headers.TryGetValues("Retry-After", out var values))
        {
            var retryAfter = values.FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(retryAfter))
            {
                return $"Riot API rate limit exceeded. Retry after {retryAfter} seconds.";
            }
        }

        return "Riot API rate limit exceeded.";
    }
}