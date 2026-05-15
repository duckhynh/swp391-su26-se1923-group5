using Xunit;

namespace AIStudyHub.Tests.Services;

/// <summary>
/// Unit tests for AuthService.
/// TODO: Implement after database scaffolding and service implementation.
/// </summary>
public class AuthServiceTests
{
    [Fact]
    public void LoginAsync_WithValidCredentials_ShouldReturnToken()
    {
        // TODO: Arrange – Mock IUserRepository
        // TODO: Act – Call AuthService.LoginAsync
        // TODO: Assert – Verify token is returned
        Assert.True(true, "Placeholder test – implement after scaffolding.");
    }

    [Fact]
    public void RegisterAsync_WithDuplicateEmail_ShouldThrowConflictException()
    {
        // TODO: Implement
        Assert.True(true, "Placeholder test – implement after scaffolding.");
    }
}
