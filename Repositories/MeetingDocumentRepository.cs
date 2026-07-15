using GACHSLApi.Data;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class MeetingDocumentRepository : IMeetingDocumentRepository
    {
        private readonly AppDbContext _context;

        public MeetingDocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MeetingDocument>> GetByMeetingIdAsync(int meetingId)
        {
            return await _context.MeetingDocuments
                .Where(x => x.MeetingId == meetingId)
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.CreatedOn)
                .ToListAsync();
        }

        public async Task<MeetingDocument?> GetByIdAsync(int id)
        {
            return await _context.MeetingDocuments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(MeetingDocument document)
        {
            await _context.MeetingDocuments.AddAsync(document);
        }

        public async Task DeleteAsync(MeetingDocument document)
        {
            _context.MeetingDocuments.Remove(document);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}