using GACHSLApi.Data;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class SocietySettingsRepository : ISocietySettingsRepository
    {
        private readonly AppDbContext _context;

        public SocietySettingsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SocietySettings?> GetAsync()
        {
            return await _context.SocietySettings
                                 .FirstOrDefaultAsync();
        }

        public async Task<SocietySettings> UpdateAsync(SocietySettings settings)
        {
            settings.UpdatedOn = DateTime.UtcNow;

            _context.SocietySettings.Update(settings);

            await _context.SaveChangesAsync();

            return settings;
        }
    }
}