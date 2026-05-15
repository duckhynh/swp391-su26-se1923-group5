namespace AIStudyHub.Shared.Exceptions;

/// <summary>
/// Base exception for all application-specific exceptions.
/// </summary>
public class AppException : Exception
{
    public int StatusCode { get; }

    public AppException(string message, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
    }
}

/// <summary>
/// 404 – Requested resource was not found.
/// </summary>
public class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base(message, 404) { }

    public NotFoundException(string entityName, object id)
        : base($"{entityName} with ID '{id}' was not found.", 404) { }
}

/// <summary>
/// 401 – Unauthorized access attempt.
/// </summary>
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Unauthorized access.")
        : base(message, 401) { }
}

/// <summary>
/// 403 – Forbidden action.
/// </summary>
public class ForbiddenException : AppException
{
    public ForbiddenException(string message = "You do not have permission to perform this action.")
        : base(message, 403) { }
}

/// <summary>
/// 400 – Validation error.
/// </summary>
public class ValidationException : AppException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(string message)
        : base(message, 400)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.", 400)
    {
        Errors = errors;
    }
}

/// <summary>
/// 409 – Duplicate / conflict resource.
/// </summary>
public class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message, 409) { }
}
