using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs.Document
{
    public class CreateDocumentDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IFormFile File { get; set; } = null!;

        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        public int DisplayOrder { get; set; }
    }
}