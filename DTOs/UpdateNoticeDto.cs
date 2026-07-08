using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs
{
    public class UpdateNoticeDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime PublishDate { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }

        [MaxLength(20)]
        public string? Priority { get; set; }

        [MaxLength(500)]
        public string? AttachmentUrl { get; set; }

        public bool IsPublished { get; set; }

        public bool IsActive { get; set; }
    }
}