using System.Net;

namespace RiftSharp;

public sealed class RiftApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string? ResponseBody { get; }

    public RiftApiException(
        HttpStatusCode statusCode,
        string message,
        string? responseBody = null
    ) : base(message)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}