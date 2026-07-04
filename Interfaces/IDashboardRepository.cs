using GACHSLApi.DTOs.Dashboard;

namespace GACHSLApi.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}