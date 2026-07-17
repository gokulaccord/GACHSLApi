using GACHSLApi.Common;
using GACHSLApi.DTOs.Dashboard;

namespace GACHSLApi.Interfaces
{
    public interface IDashboardService
    {
        Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync();
    }
}