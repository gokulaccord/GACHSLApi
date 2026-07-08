namespace GACHSLApi.DTOs.Meeting
{
    public class MeetingDto
    {
        public int MeetingId { get; set; }

        public string MeetingTitle { get; set; } = string.Empty;

        public string MeetingType { get; set; } = string.Empty;

        public DateTime MeetingDate { get; set; }

        public TimeSpan MeetingTime { get; set; }

        public string Venue { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Status { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}