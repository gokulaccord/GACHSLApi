using GACHSLApi.DTOs.Document;
using GACHSLApi.Interfaces;
using GACHSLApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GACHSLApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/documents
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] DocumentQueryDto query)
        {
            var result = await _documentService.GetAllAsync(query);
            return Ok(result);
        }

        // GET: api/documents/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _documentService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //// POST: api/documents
        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Create([FromForm] CreateDocumentDto dto)
        //{
        //    int createdBy = int.Parse(
        //        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        //    var result = await _documentService.CreateAsync(dto, createdBy);

        //    if (!result.Success)
        //        return BadRequest(result);

        //    return Ok(result);
        //}


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateDocumentDto dto)
        {
            try
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

                var result = await _documentService.CreateAsync(dto, createdBy);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }
        // PUT: api/documents/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDocumentDto dto)
        {
            var result = await _documentService.UpdateAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/documents/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _documentService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpGet("download/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(int id)
        {
            var document = await _documentService.GetByIdAsync(id);

            if (!document.Success || document.Data == null)
                return NotFound();

            var stream = await _documentService.DownloadFileAsync(id);

            return File(
                stream,
                document.Data.MimeType!,
                document.Data.FileName);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetLookup()
        {
            var documents = await _documentService.GetLookupAsync();

            return Ok(documents);
        }
    }
}