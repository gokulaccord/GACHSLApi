using GACHSLApi.Common;
using GACHSLApi.DTOs.Meeting;

namespace GACHSLApi.Interfaces
{
    public interface IMeetingService
    {
        Task<ApiResponse<List<MeetingDto>>> GetAllAsync();

        Task<ApiResponse<MeetingDto>> GetByIdAsync(int id);

        Task<ApiResponse<object>> CreateAsync(CreateMeetingDto dto);

        Task<ApiResponse<object>> UpdateAsync(int id, UpdateMeetingDto dto);

        Task<ApiResponse<object>> DeleteAsync(int id);
    }
}