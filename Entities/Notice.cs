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

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int CreatedBy { get; set; }   // UserId of Admin
    }
}