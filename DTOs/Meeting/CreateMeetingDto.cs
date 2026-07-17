using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs.Meeting
{
    public class CreateMeetingDto
    {
        [Required]
        public string MeetingTitle { get; set; } = string.Empty;

        [Required]
        public string MeetingType { get; set; } = string.Empty;

        [Required]
        public DateTime MeetingDate { get; set; }

        [Required]
        public TimeSpan MeetingTime { get; set; }

        [Required]
        public string Venue { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Status { get; set; } = "Scheduled";

        public bool IsActive { get; set; } = true;
        public string? Minutes { get; set; }
    }
}