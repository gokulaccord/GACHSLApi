using GACHSLApi.DTOs.SocietySettings;

namespace GACHSLApi.Interfaces
{
    public interface ISocietySettingsService
    {
        Task<SocietySettingsDto?> GetAsync();

        Task<SocietySettingsDto?> UpdateAsync(UpdateSocietySettingsDto dto);
    }
}