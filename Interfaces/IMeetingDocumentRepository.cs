using GACHSLApi.Entities;

namespace GACHSLApi.Interfaces
{
    public interface IMeetingDocumentRepository
    {
        Task<List<MeetingDocument>> GetByMeetingIdAsync(int meetingId);

        Task<MeetingDocument?> GetByIdAsync(int id);

        Task AddAsync(MeetingDocument document);

        Task DeleteAsync(MeetingDocument document);

        Task SaveChangesAsync();
    }
}