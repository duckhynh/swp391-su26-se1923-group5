namespace AIStudyHub.Shared.DTOs.Chat;

public class ChatMessageDto
{
    public int MessageId { get; set; }
    public int SessionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string SenderType { get; set; } = string.Empty; // "User" or "AI"
    public DateTime SentAt { get; set; }
}
