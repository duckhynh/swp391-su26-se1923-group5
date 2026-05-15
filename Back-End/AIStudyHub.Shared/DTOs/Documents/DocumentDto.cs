namespace AIStudyHub.Shared.DTOs.Documents;

public class DocumentDto
{
    public int DocumentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public int UploadedByUserId { get; set; }
    public string UploadedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<string> Tags { get; set; } = new();
}
