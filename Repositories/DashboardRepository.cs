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

            var activeMembers = await _context.Members
                .CountAsync(x => x.IsActive);
            Console.WriteLine($"TOTAL MEMBERS : {totalMembers}");
            Console.WriteLine($"ACTIVE MEMBERS : {activeMembers}");
            var totalMeetings = await _context.Meetings.CountAsync();

            var totalDocuments = await _context.Documents
                .CountAsync(x => x.IsActive);

            var totalNotices = await _context.Notices
                .CountAsync(x => x.IsActive);

            var consentYes = await _context.Consents
                .CountAsync(x => x.ConsentStatus == 1);

            var consentNo = await _context.Consents
                .CountAsync(x => x.ConsentStatus == 2);

            var consentPending = await _context.Consents
                .CountAsync(x => x.ConsentStatus == 0);

            decimal consentPercentage = 0;

            if (totalMembers > 0)
            {
                consentPercentage = Math.Round(
                    (decimal)consentYes * 100 / totalMembers,
                    2);
            }
            var database = _context.Database.GetDbConnection().Database;

           
            Console.WriteLine("DATABASE USED : " + database);
            Console.WriteLine("MEMBERS COUNT : " + totalMembers);
            return new DashboardSummaryDto
            {
                TotalMembers = totalMembers,
                ActiveMembers = activeMembers,
                TotalMeetings = totalMeetings,
                TotalDocuments = totalDocuments,
                TotalNotices = totalNotices,
                ConsentYes = consentYes,
                ConsentNo = consentNo,
                ConsentPending = consentPending,
                ConsentPercentage = consentPercentage
            };
        }
    }
}