using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Subjects;
using AIStudyHub.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIStudyHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _subjectService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<SubjectDto>>.SuccessResponse(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _subjectService.GetByIdAsync(id);
        return Ok(ApiResponse<SubjectDto>.SuccessResponse(result));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Lecturer")]
    public async Task<IActionResult> Create([FromBody] SubjectDto dto)
    {
        var result = await _subjectService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.SubjectId },
            ApiResponse<SubjectDto>.SuccessResponse(result, "Subject created."));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Lecturer")]
    public async Task<IActionResult> Update(int id, [FromBody] SubjectDto dto)
    {
        var result = await _subjectService.UpdateAsync(id, dto);
        return Ok(ApiResponse<SubjectDto>.SuccessResponse(result, "Subject updated."));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _subjectService.DeleteAsync(id);
        return Ok(ApiResponse.Ok("Subject deleted."));
    }
}
