using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface INoticeRepository
    {
        Task<List<Notice>> GetAllAsync();
        Task<Notice?> GetByIdAsync(int id);
        Task AddAsync(Notice notice);
        Task UpdateAsync(Notice notice);
        Task DeleteAsync(Notice notice);
        Task SaveChangesAsync();
    }
}