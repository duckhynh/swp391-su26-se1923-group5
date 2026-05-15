namespace AIStudyHub.Shared.DTOs.Chat;

public class SendMessageDto
{
    public int SessionId { get; set; }
    public string Content { get; set; } = string.Empty;
}
