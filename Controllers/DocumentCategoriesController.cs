using GACHSLApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _context.DocumentCategories
                .Where(x => x.IsActive)
                .OrderBy(x => x.CategoryName)
                .ToListAsync();

            return Ok(categories);
        }
    }
}