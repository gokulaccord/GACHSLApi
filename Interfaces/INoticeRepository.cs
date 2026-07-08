using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface INoticeRepository
    {
        Task<IEnumerable<Notice>> GetAllAsync();
        Task<Notice?> GetByIdAsync(int id);
        Task<Notice> AddAsync(Notice notice);
        Task UpdateAsync(Notice notice);
        Task DeleteAsync(Notice notice);
        Task SaveChangesAsync();
    }
}