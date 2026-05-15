using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Auth;
using AIStudyHub.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIStudyHub.API.Controllers;

/// <summary>
/// Authentication endpoints – login, register, refresh token, logout.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// POST api/auth/login
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successful."));
    }

    /// <summary>
    /// POST api/auth/register
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var result = await _authService.RegisterAsync(request);
        return CreatedAtAction(nameof(Register), ApiResponse<AuthResponseDto>.SuccessResponse(result, "Registration successful."));
    }

    /// <summary>
    /// POST api/auth/refresh-token
    /// </summary>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var result = await _authService.RefreshTokenAsync(refreshToken);
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Token refreshed."));
    }

    /// <summary>
    /// POST api/auth/logout
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        // TODO: Extract userId from JWT claims after implementing auth
        var userId = 0; // Placeholder
        await _authService.LogoutAsync(userId);
        return Ok(ApiResponse.Ok("Logged out successfully."));
    }
}
