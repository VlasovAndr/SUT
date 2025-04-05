using System.Text.Json.Serialization;

namespace ProductAPI.Errors;

public class ApiResponse
{
    public ApiResponse(int statusCode, string? message = null)
    {
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    [JsonPropertyName("result")]
    public object? Result { get; set; }
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; } = true;
    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        if (statusCode >= 400 && statusCode <= 600)
        {
            IsSuccess = false;

            return statusCode switch
            {
                400 => "Bad request. The server could not understand your request.",
                401 => "Unauthorized. You must authenticate yourself to access this resource.",
                404 => "Not found. The requested resource could not be found on the server.",
                500 => "Internal Server Error. Something went wrong on the server side. Please try again later.",
                _ => "Unexpected error occurred. Please contact support."
            };
        }

        return string.Empty;
    }
}
