using GACHSLApi.DTOs.SocietySettings;
using GACHSLApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GACHSLApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SocietySettingsController : ControllerBase
    {
        private readonly ISocietySettingsService _service;

        public SocietySettingsController(ISocietySettingsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<SocietySettingsDto>> Get()
        {
            var settings = await _service.GetAsync();

            if (settings == null)
                return NotFound();

            return Ok(settings);
        }

        [HttpPut]
        public async Task<ActionResult<SocietySettingsDto>> Update(UpdateSocietySettingsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
    }
}