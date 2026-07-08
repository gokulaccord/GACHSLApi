using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.Entities
{
    public class Notice
    {
        [Key]
        public int NoticeId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string? Category { get; set; }

        [MaxLength(20)]
        public string? Priority { get; set; }

        [MaxLength(500)]
        public string? AttachmentUrl { get; set; }

        public bool IsPublished { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }

        public int CreatedBy { get; set; }

        // Optional (recommended for future use)
        public int? UpdatedBy { get; set; }
    }
}