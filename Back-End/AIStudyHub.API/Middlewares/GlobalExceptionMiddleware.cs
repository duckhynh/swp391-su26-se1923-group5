using System.Net;
using System.Text.Json;
using AIStudyHub.Shared.Exceptions;
using AIStudyHub.Shared.Responses;

namespace AIStudyHub.API.Middlewares;

/// <summary>
/// Global exception handling middleware.
/// Catches all unhandled exceptions and returns a consistent ApiResponse JSON.
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message, errors) = exception switch
        {
            ValidationException vex => (vex.StatusCode, vex.Message,
                vex.Errors.SelectMany(e => e.Value).ToList()),
            AppException appEx => (appEx.StatusCode, appEx.Message, (List<string>?)null),
            _ => ((int)HttpStatusCode.InternalServerError,
                "An unexpected error occurred.", (List<string>?)null)
        };

        context.Response.StatusCode = statusCode;

        var response = ApiResponse.Fail(message, errors);
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
