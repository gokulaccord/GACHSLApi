using System.ComponentModel.DataAnnotations;
using GACHSLApi.Enums;

namespace GACHSLApi.DTOs.Meeting
{
    public class CreateMeetingDto
    {
        [Required]
        [MaxLength(200)]
        public string MeetingTitle { get; set; } = string.Empty;

        [Required]
        public DateTime MeetingDate { get; set; }

        [Required]
        public TimeSpan MeetingTime { get; set; }

        [MaxLength(200)]
        public string Venue { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Agenda { get; set; } = string.Empty;

        [MaxLength(3000)]
        public string? Description { get; set; }

        [Required]
        public MeetingType MeetingType { get; set; }

        public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;
    }
}