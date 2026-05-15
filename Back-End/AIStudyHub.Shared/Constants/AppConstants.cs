namespace AIStudyHub.Shared.Constants;

/// <summary>
/// Application-wide constant values.
/// </summary>
public static class AppConstants
{
    // ── API ──────────────────────────────────────────────
    public const string ApiVersion = "v1";
    public const string ApiBaseRoute = $"api/{ApiVersion}";

    // ── Pagination ──────────────────────────────────────
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 10;
    public const int MaxPageSize = 50;

    // ── JWT config keys ─────────────────────────────────
    public const string JwtSectionKey = "Jwt";
    public const string JwtIssuerKey = "Jwt:Issuer";
    public const string JwtAudienceKey = "Jwt:Audience";
    public const string JwtSecretKey = "Jwt:SecretKey";
    public const string JwtExpirationMinutesKey = "Jwt:ExpirationMinutes";

    // ── File upload ─────────────────────────────────────
    public const long MaxFileSizeBytes = 50 * 1024 * 1024; // 50 MB
    public static readonly string[] AllowedFileExtensions =
        { ".pdf", ".docx", ".pptx", ".txt", ".md", ".xlsx" };
}
