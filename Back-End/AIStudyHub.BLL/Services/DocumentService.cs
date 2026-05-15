using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Documents;
using AIStudyHub.Shared.Responses;

namespace AIStudyHub.BLL.Services;

public class DocumentService : IDocumentService
{
    // TODO: Inject IDocumentRepository, ISubjectRepository

    public Task<DocumentDto> GetByIdAsync(int documentId) => throw new NotImplementedException();
    public Task<PaginatedResponse<DocumentDto>> GetAllAsync(int pageNumber, int pageSize) => throw new NotImplementedException();
    public Task<PaginatedResponse<DocumentDto>> GetBySubjectAsync(int subjectId, int pageNumber, int pageSize) => throw new NotImplementedException();
    public Task<PaginatedResponse<DocumentDto>> SearchAsync(string keyword, int pageNumber, int pageSize) => throw new NotImplementedException();
    public Task<DocumentDto> CreateAsync(CreateDocumentDto dto, int userId) => throw new NotImplementedException();
    public Task<DocumentDto> UpdateAsync(int documentId, CreateDocumentDto dto, int userId) => throw new NotImplementedException();
    public Task DeleteAsync(int documentId, int userId) => throw new NotImplementedException();
}
