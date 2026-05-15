namespace AIStudyHub.Shared.Responses;

/// <summary>
/// Standard API response wrapper for consistent JSON structure.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Request successful.")
        => new() { Success = true, Message = message, Data = data };

    public static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
        => new() { Success = false, Message = message, Errors = errors };
}

/// <summary>
/// Non-generic version for operations without a data payload.
/// </summary>
public class ApiResponse : ApiResponse<object>
{
    public static ApiResponse Ok(string message = "Request successful.")
        => new() { Success = true, Message = message };

    public static ApiResponse Fail(string message, List<string>? errors = null)
        => new() { Success = false, Message = message, Errors = errors };
}
