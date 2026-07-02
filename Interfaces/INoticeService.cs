using GACHSLApi.DTOs.Notice;

namespace GACHSLApi.Interfaces
{
    public interface INoticeService
    {
        Task<List<NoticeDto>> GetAllAsync();

        Task<NoticeDto?> GetByIdAsync(int id);

        Task CreateAsync(CreateNoticeDto dto, int createdBy);

        Task UpdateAsync(int id, UpdateNoticeDto dto);

        Task DeleteAsync(int id);
    }
}