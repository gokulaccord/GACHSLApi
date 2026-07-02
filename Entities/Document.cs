using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GACHSLApi.Entities
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string GoogleDriveFileId { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public DocumentCategory? Category { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}