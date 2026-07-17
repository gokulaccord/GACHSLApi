using GACHSLApi.DTOs.Consent;
using GACHSLApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GACHSLApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsentController : ControllerBase
    {
        private readonly IConsentService _consentService;

        public ConsentController(IConsentService consentService)
        {
            _consentService = consentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _consentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _consentService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateConsentDto dto)
        {
            var result = await _consentService.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateConsentDto dto)
        {
            var result = await _consentService.UpdateAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _consentService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}   