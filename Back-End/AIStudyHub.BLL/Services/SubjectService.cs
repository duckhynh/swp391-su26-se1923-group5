using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Subjects;

namespace AIStudyHub.BLL.Services;

public class SubjectService : ISubjectService
{
    // TODO: Inject ISubjectRepository

    public Task<SubjectDto> GetByIdAsync(int subjectId) => throw new NotImplementedException();
    public Task<IEnumerable<SubjectDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<SubjectDto> CreateAsync(SubjectDto dto) => throw new NotImplementedException();
    public Task<SubjectDto> UpdateAsync(int subjectId, SubjectDto dto) => throw new NotImplementedException();
    public Task DeleteAsync(int subjectId) => throw new NotImplementedException();
}
