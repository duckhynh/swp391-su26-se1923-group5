namespace AIStudyHub.Shared.DTOs.Chat;

public class ChatSessionDto
{
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastMessageAt { get; set; }
}
