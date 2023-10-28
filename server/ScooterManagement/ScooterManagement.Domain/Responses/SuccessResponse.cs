using System.Net;
using System.Text.Json.Serialization;

namespace ScooterManagement.Domain.Responses;

public class SuccessResponse<T>
{
    [JsonPropertyOrder(0)]
    public string Message { get; set; }

    [JsonPropertyOrder(1)]
    public T Payload { get; set; }

    [JsonPropertyOrder(2)]
    public HttpStatusCode Status { get; set; }
}
