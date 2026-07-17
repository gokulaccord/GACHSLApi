using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllAsync();

        Task<Member?> GetByIdAsync(int id);

        Task<List<Member>> GetAvailableMembersAsync(int? consentId = null);

        Task AddAsync(Member member);

        Task UpdateAsync(Member member);

        Task DeleteAsync(Member member);

        Task SaveChangesAsync();
    }
}
