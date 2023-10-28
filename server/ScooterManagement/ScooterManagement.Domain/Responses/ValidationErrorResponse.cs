using System.Text.Json.Serialization;

namespace ScooterManagement.Domain.Responses;

public class ValidationErrorResponse : ErrorResponse
{
    [JsonPropertyOrder(2)]
    public Dictionary<string, string> Errors { get; set; }
}
