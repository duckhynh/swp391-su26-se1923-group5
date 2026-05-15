using AIStudyHub.Shared.DTOs.Chat;

namespace AIStudyHub.BLL.Interfaces;

public interface IChatService
{
    Task<ChatSessionDto> CreateSessionAsync(int userId, string? title = null);
    Task<IEnumerable<ChatSessionDto>> GetUserSessionsAsync(int userId);
    Task<IEnumerable<ChatMessageDto>> GetSessionMessagesAsync(int sessionId, int userId);
    Task<ChatMessageDto> SendMessageAsync(SendMessageDto dto, int userId);
    Task DeleteSessionAsync(int sessionId, int userId);
}
