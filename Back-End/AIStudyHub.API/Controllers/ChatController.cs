using AIStudyHub.BLL.Interfaces;
using AIStudyHub.Shared.DTOs.Chat;
using AIStudyHub.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIStudyHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("sessions")]
    public async Task<IActionResult> CreateSession([FromQuery] string? title = null)
    {
        var userId = 0; // TODO: Extract from JWT
        var result = await _chatService.CreateSessionAsync(userId, title);
        return CreatedAtAction(nameof(GetSessionMessages), new { sessionId = result.SessionId },
            ApiResponse<ChatSessionDto>.SuccessResponse(result, "Session created."));
    }

    [HttpGet("sessions")]
    public async Task<IActionResult> GetSessions()
    {
        var userId = 0; // TODO: Extract from JWT
        var result = await _chatService.GetUserSessionsAsync(userId);
        return Ok(ApiResponse<IEnumerable<ChatSessionDto>>.SuccessResponse(result));
    }

    [HttpGet("sessions/{sessionId}/messages")]
    public async Task<IActionResult> GetSessionMessages(int sessionId)
    {
        var userId = 0; // TODO: Extract from JWT
        var result = await _chatService.GetSessionMessagesAsync(sessionId, userId);
        return Ok(ApiResponse<IEnumerable<ChatMessageDto>>.SuccessResponse(result));
    }

    [HttpPost("messages")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
    {
        var userId = 0; // TODO: Extract from JWT
        var result = await _chatService.SendMessageAsync(dto, userId);
        return Ok(ApiResponse<ChatMessageDto>.SuccessResponse(result, "Message sent."));
    }

    [HttpDelete("sessions/{sessionId}")]
    public async Task<IActionResult> DeleteSession(int sessionId)
    {
        var userId = 0; // TODO: Extract from JWT
        await _chatService.DeleteSessionAsync(sessionId, userId);
        return Ok(ApiResponse.Ok("Session deleted."));
    }
}
