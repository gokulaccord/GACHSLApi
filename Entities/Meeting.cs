using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.Entities
{
    public class Meeting
    {
        [Key]
        public int MeetingId { get; set; }

        [Required]
        [MaxLength(200)]
        public string MeetingTitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string MeetingType { get; set; } = string.Empty;

        [Required]
        public DateTime MeetingDate { get; set; }

        [Required]
        public TimeSpan MeetingTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Venue { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Scheduled";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }
    }
}