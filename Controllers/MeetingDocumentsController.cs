using GACHSLApi.DTOs.MeetingDocument;
using GACHSLApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GACHSLApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingDocumentsController : ControllerBase
    {
        private readonly IMeetingDocumentService _meetingDocumentService;

        public MeetingDocumentsController(
            IMeetingDocumentService meetingDocumentService)
        {
            _meetingDocumentService = meetingDocumentService;
        }

        // GET: api/MeetingDocuments/meeting/1
        [HttpGet("meeting/{meetingId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMeeting(int meetingId)
        {
            var result = await _meetingDocumentService.GetByMeetingIdAsync(meetingId);
            return Ok(result);
        }

        // POST: api/MeetingDocuments
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateMeetingDocumentDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "NameIdentifier claim not found."
                });
            }

            int createdBy = int.Parse(userIdClaim.Value);

            var result = await _meetingDocumentService.CreateAsync(dto, createdBy);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/MeetingDocuments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _meetingDocumentService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}