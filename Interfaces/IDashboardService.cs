using GACHSLApi.DTOs.Dashboard;

namespace GACHSLApi.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}