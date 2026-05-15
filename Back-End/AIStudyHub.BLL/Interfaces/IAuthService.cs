using AIStudyHub.Shared.DTOs.Auth;

namespace AIStudyHub.BLL.Interfaces;

/// <summary>
/// Authentication service contract.
/// </summary>
public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(int userId);
}
