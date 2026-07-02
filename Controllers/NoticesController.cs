using GACHSLApi.DTOs.Notice;
using GACHSLApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GACHSLApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeService _noticeService;

        public NoticesController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        // All logged-in members
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _noticeService.GetAllAsync();
            return Ok(result);
        }

        // All logged-in members
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _noticeService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // Admin only
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateNoticeDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _noticeService.CreateAsync(dto, userId);

            return Ok(new
            {
                message = "Notice created successfully."
            });
        }

        // Admin only
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateNoticeDto dto)
        {
            await _noticeService.UpdateAsync(id, dto);

            return Ok(new
            {
                message = "Notice updated successfully."
            });
        }

        // Admin only
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _noticeService.DeleteAsync(id);

            return Ok(new
            {
                message = "Notice deleted successfully."
            });
        }
    }
}