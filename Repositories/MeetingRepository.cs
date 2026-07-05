using GACHSLApi.Data;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly AppDbContext _context;

        public MeetingRepository(AppDbContext context)
        {
            _context = context;
        }
            
        public async Task<List<Meeting>> GetAllAsync()
        {
            return await _context.Meetings
                .OrderByDescending(x => x.MeetingDate)
                .ToListAsync();
        }

        public async Task<Meeting?> GetByIdAsync(int id)
        {
            return await _context.Meetings
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Meeting> CreateAsync(Meeting meeting)
        {
            _context.Meetings.Add(meeting);

            await _context.SaveChangesAsync();

            return meeting;
        }

       

      
        public async Task AddAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
        }

        public Task UpdateAsync(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Meeting meeting)
        {
            _context.Meetings.Remove(meeting);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}