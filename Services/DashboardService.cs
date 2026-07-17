using GACHSLApi.Common;
using GACHSLApi.DTOs.Dashboard;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync()
        {
            var summary = await _dashboardRepository.GetSummaryAsync();

            return new ApiResponse<DashboardSummaryDto>(
                true,
                "Dashboard summary retrieved successfully.",
                summary);
        }
    }
}