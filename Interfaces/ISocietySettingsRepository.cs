using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface ISocietySettingsRepository
    {
        Task<SocietySettings?> GetAsync();

        Task<SocietySettings> UpdateAsync(SocietySettings settings);
    }
}