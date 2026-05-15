using AIStudyHub.Shared.DTOs.Documents;
using AIStudyHub.Shared.Responses;

namespace AIStudyHub.BLL.Interfaces;

public interface IDocumentService
{
    Task<DocumentDto> GetByIdAsync(int documentId);
    Task<PaginatedResponse<DocumentDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<PaginatedResponse<DocumentDto>> GetBySubjectAsync(int subjectId, int pageNumber, int pageSize);
    Task<PaginatedResponse<DocumentDto>> SearchAsync(string keyword, int pageNumber, int pageSize);
    Task<DocumentDto> CreateAsync(CreateDocumentDto dto, int userId);
    Task<DocumentDto> UpdateAsync(int documentId, CreateDocumentDto dto, int userId);
    Task DeleteAsync(int documentId, int userId);
}
