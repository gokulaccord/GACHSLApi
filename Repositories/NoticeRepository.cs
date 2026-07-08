using GACHSLApi.Data;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class NoticeRepository : INoticeRepository
    {
        private readonly AppDbContext _context;

        public NoticeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notice>> GetAllAsync()
        {
            return await _context.Notices
                .OrderByDescending(x => x.PublishDate)
                .ToListAsync();
        }

        public async Task<Notice?> GetByIdAsync(int id)
        {
            return await _context.Notices
                .FirstOrDefaultAsync(x => x.NoticeId == id);
        }

        public async Task<Notice> AddAsync(Notice notice)
        {
            await _context.Notices.AddAsync(notice);
            return notice;
        }

        public Task UpdateAsync(Notice notice)
        {
            _context.Notices.Update(notice);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Notice notice)
        {
            _context.Notices.Remove(notice);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}