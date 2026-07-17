using GACHSLApi.Common;
using GACHSLApi.DTOs.Meeting;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<ApiResponse<List<MeetingDto>>> GetAllAsync()
        {
            var meetings = await _meetingRepository.GetAllAsync();

            var result = meetings.Select(m => new MeetingDto
            {
                MeetingId = m.MeetingId,
                MeetingTitle = m.MeetingTitle,
                MeetingType = m.MeetingType,
                MeetingDate = m.MeetingDate,
                MeetingTime = m.MeetingTime,
                Minutes= m.Minutes,
                Venue = m.Venue,
                Description = m.Description,
                Status = m.Status,
                IsActive = m.IsActive,
                CreatedOn = m.CreatedOn
            }).ToList();

            return new ApiResponse<List<MeetingDto>>(true, "Success", result);
        }

        public async Task<ApiResponse<MeetingDto>> GetByIdAsync(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);

            if (meeting == null)
                return new ApiResponse<MeetingDto>(false, "Meeting not found", null);

            var dto = new MeetingDto
            {
                MeetingId = meeting.MeetingId,
                MeetingTitle = meeting.MeetingTitle,
                MeetingType = meeting.MeetingType,
                MeetingDate = meeting.MeetingDate,
                MeetingTime = meeting.MeetingTime,
                Venue = meeting.Venue,
                Description = meeting.Description,
                Minutes = meeting.Minutes,
                Status = meeting.Status,
                IsActive = meeting.IsActive,
                CreatedOn = meeting.CreatedOn
            };

            return new ApiResponse<MeetingDto>(true, "Success", dto);
        }

        public async Task<ApiResponse<object>> CreateAsync(CreateMeetingDto dto)
        {
            if (dto.MeetingDate.Date < DateTime.Today)
            {
                return new ApiResponse<object>(
                    false,
                    "Meeting date cannot be in the past.",
                    null);
            }

            bool exists = await _meetingRepository.ExistsAsync(
                dto.MeetingDate,
                dto.MeetingTime,
                dto.Venue);

            if (exists)
            {
                return new ApiResponse<object>(
                    false,
                    "Another meeting is already scheduled at the same venue, date and time.",
                    null);
            }

            var meeting = new Meeting
            {
                MeetingTitle = dto.MeetingTitle,
                MeetingType = dto.MeetingType,
                MeetingDate = dto.MeetingDate,
                MeetingTime = dto.MeetingTime,
                Venue = dto.Venue,
                Description = dto.Description,
                Minutes = dto.Minutes,
                Status = dto.Status,
                IsActive = dto.IsActive,
                CreatedOn = DateTime.UtcNow
            };

            await _meetingRepository.AddAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting scheduled successfully.",
                null);
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, UpdateMeetingDto dto)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);

            if (meeting == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Meeting not found.",
                    null);
            }

            meeting.MeetingTitle = dto.MeetingTitle;
            meeting.MeetingType = dto.MeetingType;
            meeting.MeetingDate = dto.MeetingDate;
            meeting.MeetingTime = dto.MeetingTime;
            meeting.Venue = dto.Venue;
            meeting.Description = dto.Description;
            meeting.Minutes= dto.Minutes;
            meeting.Status = dto.Status;
            meeting.IsActive = dto.IsActive;
            meeting.UpdatedOn = DateTime.UtcNow;

            await _meetingRepository.UpdateAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting updated successfully.",
                null);
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);

            if (meeting == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Meeting not found.",
                    null);
            }

            await _meetingRepository.DeleteAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting deleted successfully.",
                null);
        }
    }
}