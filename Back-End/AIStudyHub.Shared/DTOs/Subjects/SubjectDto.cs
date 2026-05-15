namespace AIStudyHub.Shared.DTOs.Subjects;

public class SubjectDto
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
