using AIStudyHub.Shared.DTOs.Subjects;

namespace AIStudyHub.BLL.Interfaces;

public interface ISubjectService
{
    Task<SubjectDto> GetByIdAsync(int subjectId);
    Task<IEnumerable<SubjectDto>> GetAllAsync();
    Task<SubjectDto> CreateAsync(SubjectDto dto);
    Task<SubjectDto> UpdateAsync(int subjectId, SubjectDto dto);
    Task DeleteAsync(int subjectId);
}
