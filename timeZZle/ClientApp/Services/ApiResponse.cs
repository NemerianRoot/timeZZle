using System.Net;
using System.Text.Json;

namespace timeZZle.ClientApp.Services;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; private init; }
    public string? Content { get; private init; }

    public static implicit operator ApiResponse(HttpResponseMessage httpResponseMessage)
    {
        return new ApiResponse
        {
            StatusCode = httpResponseMessage.StatusCode,
            Content = httpResponseMessage.Content?.ReadAsStringAsync().Result
        };
    }
}

public class ApiResponse<TContent>
{
    private static readonly JsonSerializerOptions? JsonSerializerOptions = new()
    {
        WriteIndented = false,
        IgnoreReadOnlyProperties = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null,
        IncludeFields = true
    };

    public HttpStatusCode StatusCode { get; private set; }
    public TContent? Content { get; private set; }

    public static implicit operator ApiResponse<TContent?>(ApiResponse apiResponse)
    {
        var stringContent = apiResponse.Content!;
        
        return new ApiResponse<TContent?>
        {
            StatusCode = apiResponse.StatusCode,
            Content = JsonSerializer.Deserialize<TContent>(stringContent, JsonSerializerOptions)
        };
    }
}