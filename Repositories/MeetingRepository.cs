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
                .ThenByDescending(x => x.MeetingTime)
                .ToListAsync();
        }

        public async Task<Meeting?> GetByIdAsync(int id)
        {
            return await _context.Meetings
                .FirstOrDefaultAsync(x => x.MeetingId == id);
        }

        public async Task AddAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
        }

        public async Task DeleteAsync(Meeting meeting)
        {
            _context.Meetings.Remove(meeting);
        }
        public async Task<bool> ExistsAsync(
    DateTime meetingDate,
    TimeSpan meetingTime,
    string venue)
        {
            return await _context.Meetings.AnyAsync(x =>
                x.MeetingDate.Date == meetingDate.Date &&
                x.MeetingTime == meetingTime &&
                x.Venue == venue &&
                x.IsActive);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}