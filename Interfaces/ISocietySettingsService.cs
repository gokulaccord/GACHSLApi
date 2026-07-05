using GACHSLApi.Common;
using GACHSLApi.DTOs.SocietySettings;

namespace GACHSLApi.Interfaces
{
    public interface ISocietySettingsService
    {
        Task<ApiResponse<SocietySettingsDto>> GetAsync();

        Task<ApiResponse<SocietySettingsDto>> UpdateAsync(UpdateSocietySettingsDto dto);
    }
}