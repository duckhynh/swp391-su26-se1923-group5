using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Auth;

namespace AIStudyHub.BLL.Services;

/// <summary>
/// Authentication service placeholder.
/// TODO: Implement after database scaffolding.
/// </summary>
public class AuthService : IAuthService
{
    // TODO: Inject IUserRepository, IConfiguration

    public Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        // TODO: 1. Validate credentials  2. Verify password hash
        //       3. Generate JWT token     4. Return AuthResponseDto
        throw new NotImplementedException("LoginAsync – implement after DB scaffolding.");
    }

    public Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // TODO: 1. Validate input         2. Check duplicate email
        //       3. Hash password           4. Create user
        //       5. Generate JWT            6. Return AuthResponseDto
        throw new NotImplementedException("RegisterAsync – implement after DB scaffolding.");
    }

    public Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
    {
        // TODO: Validate refresh token, generate new JWT
        throw new NotImplementedException("RefreshTokenAsync – implement after DB scaffolding.");
    }

    public Task LogoutAsync(int userId)
    {
        // TODO: Invalidate refresh token
        throw new NotImplementedException("LogoutAsync – implement after DB scaffolding.");
    }
}
