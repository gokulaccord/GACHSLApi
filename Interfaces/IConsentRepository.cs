using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface IConsentRepository
    {
        Task<List<Consent>> GetAllAsync();

        Task<Consent?> GetByIdAsync(int id);

        Task AddAsync(Consent consent);

        Task UpdateAsync(Consent consent);

        Task DeleteAsync(Consent consent);

        Task SaveChangesAsync();
        Task<bool> ExistsByMemberIdAsync(int memberId);
    }
}