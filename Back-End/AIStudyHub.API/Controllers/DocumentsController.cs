using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Documents;
using AIStudyHub.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIStudyHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _documentService.GetAllAsync(pageNumber, pageSize);
        return Ok(ApiResponse<PaginatedResponse<DocumentDto>>.SuccessResponse(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _documentService.GetByIdAsync(id);
        return Ok(ApiResponse<DocumentDto>.SuccessResponse(result));
    }

    [HttpGet("subject/{subjectId}")]
    public async Task<IActionResult> GetBySubject(int subjectId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _documentService.GetBySubjectAsync(subjectId, pageNumber, pageSize);
        return Ok(ApiResponse<PaginatedResponse<DocumentDto>>.SuccessResponse(result));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _documentService.SearchAsync(keyword, pageNumber, pageSize);
        return Ok(ApiResponse<PaginatedResponse<DocumentDto>>.SuccessResponse(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDocumentDto dto)
    {
        // TODO: Extract userId from JWT claims
        var userId = 0;
        var result = await _documentService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = result.DocumentId },
            ApiResponse<DocumentDto>.SuccessResponse(result, "Document created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateDocumentDto dto)
    {
        var userId = 0; // TODO: Extract from JWT
        var result = await _documentService.UpdateAsync(id, dto, userId);
        return Ok(ApiResponse<DocumentDto>.SuccessResponse(result, "Document updated."));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = 0; // TODO: Extract from JWT
        await _documentService.DeleteAsync(id, userId);
        return Ok(ApiResponse.Ok("Document deleted."));
    }
}
