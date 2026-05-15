using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Chat;

namespace AIStudyHub.BLL.Services;

public class ChatService : IChatService
{
    // TODO: Inject IChatSessionRepository, IConfiguration (OpenAI key)

    public Task<ChatSessionDto> CreateSessionAsync(int userId, string? title = null) => throw new NotImplementedException();
    public Task<IEnumerable<ChatSessionDto>> GetUserSessionsAsync(int userId) => throw new NotImplementedException();
    public Task<IEnumerable<ChatMessageDto>> GetSessionMessagesAsync(int sessionId, int userId) => throw new NotImplementedException();
    public Task<ChatMessageDto> SendMessageAsync(SendMessageDto dto, int userId) => throw new NotImplementedException();
    public Task DeleteSessionAsync(int sessionId, int userId) => throw new NotImplementedException();
}
