using GACHSLApi.Common;
using GACHSLApi.DTOs;

namespace GACHSLApi.Interfaces
{
    public interface INoticeService
    {
        Task<ApiResponse<List<NoticeDto>>> GetAllAsync();

        Task<ApiResponse<NoticeDto>> GetByIdAsync(int id);

        Task<ApiResponse<object>> CreateAsync(CreateNoticeDto dto);

        Task<ApiResponse<object>> UpdateAsync(int id, UpdateNoticeDto dto);

        Task<ApiResponse<object>> DeleteAsync(int id);
    }
}