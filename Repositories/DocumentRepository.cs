using GACHSLApi.Data;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await _context.Documents
                .Include(d => d.Category)
                .OrderBy(d => d.DisplayOrder)
                .ThenByDescending(d => d.PublishDate)
                .ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents
                .Include(d => d.Category)
                .FirstOrDefaultAsync(d => d.DocumentId == id);
        }

        public async Task AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
        }

        public Task UpdateAsync(Document document)
        {
            _context.Documents.Update(document);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Document document)
        {
            _context.Documents.Remove(document);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Document>> GetLookupAsync()
        {
            return await _context.Documents
                .AsNoTracking()
                .OrderBy(d => d.Title)
                .ToListAsync();
        }
    }
}