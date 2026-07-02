using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs.Document
{
    public class UpdateDocumentDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string GoogleDriveFileId { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}