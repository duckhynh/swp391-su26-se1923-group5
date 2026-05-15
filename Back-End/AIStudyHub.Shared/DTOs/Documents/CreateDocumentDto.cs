namespace AIStudyHub.Shared.DTOs.Documents;

public class CreateDocumentDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SubjectId { get; set; }
    public List<string> Tags { get; set; } = new();
    // TODO: File upload (IFormFile) handled at controller level
}
