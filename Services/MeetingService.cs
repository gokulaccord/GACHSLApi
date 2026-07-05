using AutoMapper;
using GACHSLApi.Common;
using GACHSLApi.DTOs.Meeting;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public MeetingService(
            IMeetingRepository meetingRepository,
            IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<MeetingDto>>> GetAllAsync()
        {
            var meetings = await _meetingRepository.GetAllAsync();

            var result = _mapper.Map<List<MeetingDto>>(meetings);

            return new ApiResponse<List<MeetingDto>>(
                true,
                "Success",
                result);
        }

        public async Task<ApiResponse<MeetingDto>> GetByIdAsync(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);

            if (meeting == null)
            {
                return new ApiResponse<MeetingDto>(
                    false,
                    "Meeting not found",
                    null);
            }

            var result = _mapper.Map<MeetingDto>(meeting);

            return new ApiResponse<MeetingDto>(
                true,
                "Success",
                result);
        }

        public async Task<ApiResponse<object>> CreateAsync(CreateMeetingDto dto)
        {
            var meeting = _mapper.Map<Meeting>(dto);

            meeting.CreatedOn = DateTime.UtcNow;

            await _meetingRepository.AddAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting created successfully",
                null);
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, UpdateMeetingDto dto)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);

            if (meeting == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Meeting not found",
                    null);
            }

            _mapper.Map(dto, meeting);

            meeting.UpdatedOn = DateTime.UtcNow;

            await _meetingRepository.UpdateAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting updated successfully",
                null);
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);

            if (meeting == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Meeting not found",
                    null);
            }

            await _meetingRepository.DeleteAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting deleted successfully",
                null);
        }
    }
}