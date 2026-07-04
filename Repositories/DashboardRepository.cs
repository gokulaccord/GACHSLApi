using GACHSLApi.Data;
using GACHSLApi.DTOs.Dashboard;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GACHSLApi.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var totalMembers = await _context.Members.CountAsync();

            var owners = await _context.Members
                .CountAsync(m => m.IsOwner);

            var tenants = await _context.Members
                .CountAsync(m => !m.IsOwner);

            var activeMembers = await _context.Members
                .CountAsync(m => m.IsActive);

            var inactiveMembers = await _context.Members
                .CountAsync(m => !m.IsActive);

            return new DashboardSummaryDto
            {
                TotalMembers = totalMembers,
                Owners = owners,
                Tenants = tenants,
                ActiveMembers = activeMembers,
                InactiveMembers = inactiveMembers,

                // These will be replaced once the Flats module is created
                TotalFlats = totalMembers,
                OccupiedFlats = totalMembers,
                VacantFlats = 0
            };
        }
    }
}