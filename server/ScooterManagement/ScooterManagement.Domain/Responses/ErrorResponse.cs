using System.Net;
using System.Text.Json.Serialization;

namespace ScooterManagement.Domain.Responses;

public class ErrorResponse
{
    [JsonPropertyOrder(0)]
    public string ErrorMessage { get; set; }

    [JsonPropertyOrder(1)]
    public HttpStatusCode Status { get; set; }
}
