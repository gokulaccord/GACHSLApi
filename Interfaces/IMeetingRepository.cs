using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface IMeetingRepository
    {
        Task<List<Meeting>> GetAllAsync();

        Task<Meeting?> GetByIdAsync(int id);

        Task AddAsync(Meeting meeting);

        Task UpdateAsync(Meeting meeting);

        Task DeleteAsync(Meeting meeting);

        Task SaveChangesAsync();
    }
}