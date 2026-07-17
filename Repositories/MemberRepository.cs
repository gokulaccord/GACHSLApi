using GACHSLApi.Data;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
        }

        public async Task<List<Member>> GetAvailableMembersAsync()
        {
            var consentMemberIds = await _context.Consents
                .Select(c => c.MemberId)
                .ToListAsync();

            return await _context.Members
                .AsNoTracking()
                .Where(m => m.IsActive &&
                            !consentMemberIds.Contains(m.MemberId))
                .OrderBy(m => m.FullName)
                .ToListAsync();
        }
        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
        }

        public async Task DeleteAsync(Member member)
        {
            _context.Members.Remove(member);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Member>> GetAvailableMembersAsync(int? consentId = null)
        {
            var consentMemberIds = await _context.Consents
                .Select(c => c.MemberId)
                .ToListAsync();

            // If editing, allow the current member to remain in the list
            if (consentId.HasValue)
            {
                var currentMemberId = await _context.Consents
                    .Where(c => c.ConsentId == consentId.Value)
                    .Select(c => c.MemberId)
                    .FirstOrDefaultAsync();

                consentMemberIds.Remove(currentMemberId);
            }

            return await _context.Members
                .AsNoTracking()
                .Where(m => m.IsActive &&
                            !consentMemberIds.Contains(m.MemberId))
                .OrderBy(m => m.FullName)
                .ToListAsync();
        }
    }
}