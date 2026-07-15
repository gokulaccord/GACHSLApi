using GACHSLApi.Common;
using GACHSLApi.DTOs.MeetingDocument;

namespace GACHSLApi.Interfaces
{
    public interface IMeetingDocumentService
    {
        Task<ApiResponse<List<MeetingDocumentDto>>> GetByMeetingIdAsync(int meetingId);

        Task<ApiResponse<object>> CreateAsync(
            CreateMeetingDocumentDto dto,
            int createdBy);

        Task<ApiResponse<object>> DeleteAsync(int id);
    }
}