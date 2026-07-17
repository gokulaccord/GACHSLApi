using GACHSLApi.Data;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class ConsentRepository : IConsentRepository
    {
        private readonly AppDbContext _context;

        public ConsentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Consent>> GetAllAsync()
        {
            return await _context.Consents
                .Include(x => x.Member)
                .Include(x => x.Document)
                .OrderBy(x => x.Member!.FlatNumber)
                .ToListAsync();
        }

        public async Task<Consent?> GetByIdAsync(int id)
        {
            return await _context.Consents
                .Include(x => x.Member)
                .Include(x => x.Document)
                .FirstOrDefaultAsync(x => x.ConsentId == id);
        }

        public async Task AddAsync(Consent consent)
        {
            await _context.Consents.AddAsync(consent);
        }

        public Task UpdateAsync(Consent consent)
        {
            _context.Consents.Update(consent);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Consent consent)
        {
            _context.Consents.Remove(consent);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsByMemberIdAsync(int memberId)
        {
            return await _context.Consents
                .AnyAsync(x => x.MemberId == memberId);
        }
    }
}